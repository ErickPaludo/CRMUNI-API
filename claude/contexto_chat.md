# contexto_chat

## Visão geral

- **Objetivo** – documentar a camada de chat em tempo real (`CRMUNI.UI.Chat`) e o cliente de testes (`CRMUNI.UI.Chat.Cliente`).
- **Escopo** – conversa entre **cliente** (anônimo, identificado por empresa + celular) e **funcionário** (autenticado por JWT) via SignalR.
- **Persistência** – contato e atendimento são gravados no banco; **mensagens do chat não são persistidas** (ficam só em memória durante a sessão).

---

## 1. Projetos

| Projeto | Tipo | Porta (dev) | Responsabilidade |
|--------|------|-------------|------------------|
| `CRMUNI.UI.Chat` | ASP.NET 10 (Web) | `http://localhost:5090` | Hub SignalR em `/hubs/chat` |
| `CRMUNI.UI.Chat.Cliente` | Console | – | Cliente de testes (modo Cliente ou Funcionário) |
| `CRMUNI.UI.Api` | ASP.NET 10 (Web) | `http://localhost:5078` | Login do funcionário (`POST /CRMUNI/Autenticacao`) e **aplicação das migrations** no startup |

- O Chat **não** roda `Migrate()`; depende da API ter aplicado as migrations.
- `appsettings.json` do Chat repete `ConnectionStrings`, `TokenJWT` e `Auth` da API — a chave JWT precisa ser a mesma para validar o token emitido pela API.

---

## 2. Arquivos principais

| Arquivo | Papel |
|---------|-------|
| `CRMUNI.UI.Chat/Program.cs` | DI mínima: Infra (DbContext/repos), JWT, Serilog, `IAtendimentoChatServico`, `GerenciadorAtendimentos` (singleton), SignalR + `FiltroExcecaoHub` |
| `CRMUNI.UI.Chat/Hubs/ChatHub.cs` | Métodos invocados pelos clientes |
| `CRMUNI.UI.Chat/Hubs/IChatCliente.cs` | Eventos enviados pelo servidor (hub tipado) |
| `CRMUNI.UI.Chat/Atendimentos/GerenciadorAtendimentos.cs` | Estado em memória: aguardando setor, fila por funcionário, sessões |
| `CRMUNI.UI.Chat/Contratos/` | DTOs trafegados no websocket |
| `CRMUNI.UI.Chat/Extensao/FiltroExcecaoHub.cs` | Converte exceções de domínio/aplicação em `HubException` |
| `CRMUNI.UI.Chat/Extensao/AuxiliarAutenticacao.cs` | `GetId()` (NameIdentifier) e `GetEmail()` do token |
| `CRMUNI.Application/Services/AtendimentoChatServico.cs` | Regras de negócio: identificar contato, abrir atendimento |
| `CRMUNI.Infra.IoC/InjecaoAutenticacaoJWT.cs` | `OnMessageReceived`: lê `access_token` da query string em rotas `/hubs` (WebSocket não envia header) |

> Ao injetar um novo serviço no `ChatHub`, **registrá-lo também em `CRMUNI.UI.Chat/Program.cs`**. O Chat não chama `ConfigurarInjecaoServicos` (exigiria toda a cadeia de autenticação da API). Serviço não registrado = hub não instancia = conexão derrubada com "Connection closed with an error".

---

## 3. Fluxo

```
Cliente                          ChatHub                               Banco
  | SolicitarAtendimento(idEmpresa, telefone, nome)
  |------------------------------->| valida NomeContato/Celular (sem banco)
  |                                | Identificar -----------------------> contato por empresa+celular
  |                                |                                     atendimento Situacao != Finalizado
  |<-- AtendimentoEmAndamento (nº) | (se existir) enfileira p/ funcionário do atendimento
  |<-- SelecionarSetor (menu)      | (se não existir) guarda dados em "aguardando setor"
  | SelecionarSetor(1|2|3)
  |------------------------------->| Abrir ----------------------------> cria Contato (se não existe)
  |                                |                                     funcionário do setor c/ menos atendimentos
  |                                |                                     etapa Inicial do funil Curioso
  |<-- AtendimentoAberto (nº)      |                                     cria Atendimento + Commit
  |                                | NovaSolicitacao -> Clients.User(idFuncionario)
Funcionário (JWT)
  | ListarFila / AceitarAtendimento(idSolicitacao)  -> só as solicitações dele
  |<-- AtendimentoIniciado (cliente e funcionário, grupo "sessao-{id}")
Ambos
  | EnviarMensagem(idSessao, texto)  -> ReceberMensagem (grupo da sessão)
  | EncerrarAtendimento(idSessao)    -> AtendimentoEncerrado
```

### Métodos do hub

| Método | Quem | Retorno | Observações |
|--------|------|---------|-------------|
| `SolicitarAtendimento(Guid idEmpresa, string telefone, string nome)` | Cliente | `RespostaContatoDto` | Nome exige nome e sobrenome; celular 13 dígitos (DDI+DDD+número) |
| `SelecionarSetor(int setor)` | Cliente | `RespostaContatoDto` | 1-Comercial, 2-Financeiro, 3-Suporte (`ESetor` = setor - 1). Em erro, o cliente pode tentar outro setor |
| `ListarFila()` | Funcionário `[Authorize]` | `List<SolicitacaoDto>` | Apenas solicitações do funcionário logado |
| `AceitarAtendimento(Guid idSolicitacao)` | Funcionário `[Authorize]` | `SessaoDto` | Só o funcionário responsável pode aceitar; pode ter **várias sessões simultâneas** |
| `EnviarMensagem(Guid idSessao, string texto)` | Ambos | – | Texto validado pelo VO `Conteudo` (1–1000) |
| `EncerrarAtendimento(Guid idSessao)` | Ambos | – | Encerra só a sessão em memória |

### Eventos (`IChatCliente`)

| Evento | Destino |
|--------|---------|
| `ReceberMensagem(MensagemChatDto)` | Grupo da sessão |
| `AtendimentoIniciado(SessaoDto)` | Grupo da sessão (mesmo DTO para os dois lados) |
| `AtendimentoEncerrado(Guid idSessao, string motivo)` | Grupo da sessão |
| `NovaSolicitacao(SolicitacaoDto)` | `Clients.User(idFuncionario)` |
| `SolicitacaoRemovida(Guid)` | `Clients.User(idFuncionario)` (cliente desconectou da fila) |

`RespostaContatoDto.Tipo` (`ETipoResposta`): `AtendimentoEmAndamento`, `SelecionarSetor`, `AtendimentoAberto`.

---

## 4. Regras e decisões

| Regra | Como foi feito | Motivo |
|-------|----------------|--------|
| **Atendimento em andamento** | `Situacao != Finalizado` (Aguardando ou EmAndamento), o mais recente | Definição do negócio |
| **Distribuição** | Funcionários do setor/empresa ordenados por nº de atendimentos não finalizados, desempate por `Id` | Balancear carga |
| **Etapa inicial** | `EEtapa.Inicial` do funil `EFunil.Curioso` da empresa | Funis/etapas padrão criados no cadastro da empresa (`EtapaServico.Cadastra`) |
| **Duplicidade** | `Abrir` devolve o atendimento em andamento se já existir | Evita dois atendimentos por chamadas repetidas |
| **Nome do contato** | `NomeContato.Create(nomeCompleto)` – 1ª palavra = primeiro nome, resto = segundo | VO exige os dois nomes |
| **Email do contato** | Opcional (`Contato.Email?`) | Chat só coleta nome e celular |
| **Roteamento** | `Clients.User(id)`; o `IUserIdProvider` padrão usa a claim `NameIdentifier` (= id do funcionário) | Dispensa mapear conexões manualmente |
| **Nome do funcionário no chat** | E-mail do token (`ClaimTypes.Email`) | Sem consulta extra ao banco |
| **Erros** | `FiltroExcecaoHub`: `BaseValidacao`, `ExceptionDomain`, `NaoEncontrado`, `NaoPermitido`, `NaoAutorizado`, `Conflito` → `HubException(mensagem)`; demais → log + "Erro interno do servidor." | Cliente recebe mensagem legível sem vazar detalhes |
| **Vários atendimentos por funcionário** | Servidor não limita sessões por conexão; toda chamada (`EnviarMensagem`, `EncerrarAtendimento`) recebe o `idSessao`. No console: `/atendimentos` lista as sessões, `/trocar <nº>` muda a ativa, mensagens de outras sessões aparecem com `[nº X - outro atendimento]` | Funcionário alterna entre clientes sem desconectar ninguém |
| **Erros detalhados** | `EnableDetailedErrors` apenas em Development | Diagnóstico local |

---

## 5. Limitações conhecidas

- Estado do chat (fila, aguardando setor, sessões) é **em memória**: perdido ao reiniciar e **não escala** para mais de uma instância (exigiria backplane, ex.: Redis).
- Nenhum fluxo altera o atendimento para `Finalizado` (nem `EncerrarAtendimento`): contato que já abriu um atendimento sempre volta como "em andamento".
- Se o funcionário responsável estiver offline, a solicitação fica na fila dele até ele conectar e chamar `ListarFila`.

---

## 6. Como testar

1. `docker compose up db` (ou SQL Server local) e subir a API (aplica migrations).
2. `dotnet run --project CRMUNI.UI.Chat`.
3. Terminal A: `dotnet run --project CRMUNI.UI.Chat.Cliente` → `1` (Cliente) → id da empresa, celular, nome → setor.
4. Terminal B: mesmo comando → `2` (Funcionário) → login do funcionário vinculado → `/fila`, `/aceitar <n>`, mensagens, `/atendimentos`, `/trocar <nº>`, `/encerrar` (encerra o ativo).
5. Conferir `tb_contatos` e `tb_atendimentos`.

> Ao rodar o Chat fora do `dotnet run --project`, executar a partir da pasta `CRMUNI.UI.Chat` (content root) para carregar o `appsettings.json`.
