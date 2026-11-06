# contexto_chat

## Visão geral

- **Objetivo** – documentar a camada de chat em tempo real (`CRMUNI.UI.Chat`) e o cliente de testes (`CRMUNI.UI.Chat.Cliente`).
- **Escopo** – conversa entre **cliente** (anônimo, identificado por empresa + celular) e **funcionário** (autenticado por JWT) via SignalR.
- **Persistência** – contato, atendimento e **mensagens** (`tb_mensagens`, via `IMensagemServico`) são gravados no banco. Só as conexões ficam em memória.

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
| `CRMUNI.UI.Chat/Program.cs` | DI mínima: Infra (DbContext/repos), JWT, Serilog, `IAtendimentoChatServico`, `IMensagemServico`, `GerenciadorAtendimentos` (singleton), SignalR + `FiltroExcecaoHub` |
| `CRMUNI.UI.Chat/Hubs/ChatHub.cs` | Métodos invocados pelos clientes |
| `CRMUNI.UI.Chat/Hubs/IChatCliente.cs` | Eventos enviados pelo servidor (hub tipado) |
| `CRMUNI.UI.Chat/Atendimentos/GerenciadorAtendimentos.cs` | Estado em memória: aguardando setor e cliente conectado por atendimento (`SessaoChat`) |
| `CRMUNI.UI.Chat/Contratos/` | DTOs trafegados no websocket |
| `CRMUNI.UI.Chat/Extensao/FiltroExcecaoHub.cs` | Converte exceções de domínio/aplicação em `HubException` |
| `CRMUNI.UI.Chat/Extensao/AuxiliarAutenticacao.cs` | `GetId()` (NameIdentifier) e `GetEmail()` do token |
| `CRMUNI.Application/Services/AtendimentoChatServico.cs` | Regras de negócio: identificar contato, abrir atendimento |
| `CRMUNI.Application/Services/MensagemServico.cs` | `GravaMensagem(idAtendimento, remetente, texto)` e `RetornaMensagens(idAtendimento)` → `MensagemDto` com o remetente |
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
  |<-- AtendimentoEmAndamento      | (se existir) vincula a conexão ao atendimento
  |    (nº + histórico)            |   RetornaMensagens -----------------> tb_mensagens
  |<-- SelecionarSetor (menu)      | (se não existir) guarda dados em "aguardando setor"
  | SelecionarSetor(1|2|3)
  |------------------------------->| Abrir ----------------------------> cria Contato/Atendimento
  |<-- AtendimentoAberto (nº)      | vincula a conexão ao atendimento
Funcionário (JWT) — vinculado direto, sem aceitar
  |<-- AtendimentoIniciado         | Clients.User(idFuncionario) assim que o cliente é vinculado
  | ListarAtendimentos / RetornarMensagens(idAtendimento)
Ambos
  | EnviarMensagem(idAtendimento, texto) -> GravaMensagem -> ReceberMensagem (cliente + funcionário)
  | EncerrarAtendimento(idAtendimento)   -> AtendimentoEncerrado
```

### Métodos do hub

| Método | Quem | Retorno | Observações |
|--------|------|---------|-------------|
| `SolicitarAtendimento(Guid idEmpresa, string telefone, string nome)` | Cliente | `RespostaContatoDto` | Nome exige nome e sobrenome; celular 13 dígitos. Com atendimento em andamento já devolve o histórico em `Mensagens` |
| `SelecionarSetor(int setor)` | Cliente | `RespostaContatoDto` | 1-Comercial, 2-Financeiro, 3-Suporte (`ESetor` = setor - 1). Em erro, o cliente pode tentar outro setor |
| `ListarAtendimentos()` | Funcionário `[Authorize]` | `List<AtendimentoDto>` | Atendimentos em andamento dele (banco), com `ClienteConectado` |
| `RetornarMensagens(int idAtendimento)` | Funcionário `[Authorize]` | `List<MensagemDto>` | Só atendimento em andamento dele (cliente online ou não) |
| `EnviarMensagem(int idAtendimento, string texto)` | Ambos | – | Grava (VO `Conteudo`, 1–1000) e entrega aos dois lados. Funcionário pode enviar com o cliente offline: o cliente vê no histórico ao voltar |
| `EncerrarAtendimento(int idAtendimento)` | Ambos | – | Desvincula a conexão do cliente (erro se ele já estiver offline); não altera a situação no banco |

### Eventos (`IChatCliente`)

| Evento | Destino |
|--------|---------|
| `ReceberMensagem(MensagemDto)` | Conexão do cliente + `Clients.User(idFuncionario)` |
| `AtendimentoIniciado(SessaoDto)` | `Clients.User(idFuncionario)` |
| `AtendimentoEncerrado(int idAtendimento, string motivo)` | Cliente e funcionário (desconexão do cliente avisa o funcionário) |

`MensagemDto(Id, IdAtendimento, Remetente, Tipo, Texto, DataHora)` – `Tipo` (`ERemetente`): `Cliente` ou `Funcionario`; `Remetente` = nome.

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
| **Nome do funcionário no chat** | Nome do funcionário do atendimento (vem de `Identificar`/`Abrir`) | Já está na consulta |
| **Remetente da mensagem** | `Mensagem.DoCliente` preenche `Contato`; `Mensagem.DoFuncionario` preenche `Funcionario` (o outro fica nulo) | Colunas já eram nullable; sem migration |
| **Sem aceite** | Ao vincular o cliente, o funcionário do atendimento recebe `AtendimentoIniciado` direto | Funcionário sempre atrelado ao atendimento |
| **Erros** | `FiltroExcecaoHub`: `BaseValidacao`, `ExceptionDomain`, `NaoEncontrado`, `NaoPermitido`, `NaoAutorizado`, `Conflito` → `HubException(mensagem)`; demais → log + "Erro interno do servidor." | Cliente recebe mensagem legível sem vazar detalhes |
| **Vários atendimentos por funcionário** | Toda chamada (`EnviarMensagem`, `EncerrarAtendimento`) recebe o `idAtendimento`. No console: `/atendimentos` lista as sessões, `/trocar <nº>` muda a ativa, mensagens de outras sessões aparecem com `[nº X - outro atendimento]` | Funcionário alterna entre clientes sem desconectar ninguém |
| **Erros detalhados** | `EnableDetailedErrors` apenas em Development | Diagnóstico local |

---

## 5. Limitações conhecidas

- Conexões (aguardando setor, cliente vinculado) ficam **em memória**: perdido ao reiniciar e **não escala** para mais de uma instância (exigiria backplane, ex.: Redis).
- Nenhum fluxo altera o atendimento para `Finalizado` (nem `EncerrarAtendimento`): contato que já abriu um atendimento sempre volta como "em andamento".
- Funcionário é identificado pelo `NameIdentifier` numérico do token; login de Usuário (administrador, id Guid) não vê atendimentos.
- Um atendimento aceita uma conexão de cliente por vez (segunda conexão recebe erro).

---

## 6. Como testar

1. `docker compose up db` (ou SQL Server local) e subir a API (aplica migrations).
2. `dotnet run --project CRMUNI.UI.Chat`.
3. Terminal A: `dotnet run --project CRMUNI.UI.Chat.Cliente` → `1` (Cliente) → id da empresa, celular, nome → setor.
4. Terminal B: mesmo comando → `2` (Funcionário) → login do funcionário vinculado → recebe o cliente sem aceitar; mensagens, `/atendimentos`, `/trocar <nº>` (mostra o histórico), `/encerrar` (encerra o ativo).
5. Conferir `tb_contatos`, `tb_atendimentos` e `tb_mensagens`; reconectar o cliente mostra o histórico.

> Ao rodar o Chat fora do `dotnet run --project`, executar a partir da pasta `CRMUNI.UI.Chat` (content root) para carregar o `appsettings.json`.
