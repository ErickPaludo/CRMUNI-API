# Convenção de Códigos de Erro — CRMUNI

> Este documento descreve o padrão de códigos de erro utilizado nas validações do sistema CRMUNI.

---

## Formato do Código

Os códigos de erro seguem o formato padrão `n.n.n` (exemplo: `0.0.0`), composto por três níveis de identificação:

| Posição | Significado | Exemplo |
|---------|-------------|---------|
| 1º valor | Identificador da Entidade | `0` = Empresa |
| 2º valor | Identificador do Objeto de Valor | `0` = E-mail |
| 3º valor | Código do Erro específico | `0` = Valor nulo |

> Exemplo: `0.1.3` → Entidade `0` (Geral/Compartilhado) · Objeto de Valor `1` (Nome) · Erro `3` (máximo de caracteres excedido).

---

## Convenção dos Dígitos Finais (Erros Genéricos)

Os dígitos finais de `0` a `10` são reservados para erros genéricos e recorrentes de validação:

| Código Final | Significado |
|-------------|-------------|
| `0` | Valor nulo |
| `1` | Valor obrigatório |
| `2` | Quantidade mínima de caracteres não atingida |
| `3` | Quantidade máxima de caracteres excedida |
| `4` | Formato inválido (texto ou número) |
| `5` | Reservado para futuras padronizações |
| `6` | Enumerador inválido |
| `7` a `10` | Reservados para futuras padronizações |

> Códigos `11+` são destinados a regras de negócio específicas de cada objeto de valor/entidade.

---

## Sumário de Entidades e Objetos de Valor

### Entidades

| Entidade | Código |
|----------|--------|
| Empresa | `0` → `0.0.x` |
| Setor | `1` → `1.0.x` |
| Funcionário | `2` → `2.0.x` |
| Contato | `3` → `3.0.x` |

### Objetos de Valor (Compartilhados / Gerais)

| Objeto de Valor | Código |
|-----------------|--------|
| E-mail | `0.0.x` |
| Nome | `0.1.x` |
| Telefone / Celular | `0.2.x` |
| Documento (CNPJ/CPF) | `0.3.x` |
| Descrição | `0.4.x` |
| Senha | `0.5.x` |

---

## Catálogo Detalhado de Códigos de Erro

### 1. Entidades

#### 1.1 Empresa (Código 0.0.x)

> `CRMUNI.DOMAIN/Validacoes/Entidades/Empresas/EmpresaMensagens.cs`

| Código | Constante | Mensagem | Descrição para QA |
|--------|-----------|----------|-------------------|
| `0.0.0` | `PropriedadeNula(propriedade)` | "{propriedade} não pode ser nulla." | Ocorre quando a instância ou dados essenciais da entidade Empresa são enviados como nulos na requisição. |

#### 1.2 Setor (Código 1.0.x)

> `CRMUNI.DOMAIN/Validacoes/Entidades/Setores/SetorMensagens.cs`

| Código | Constante | Mensagem | Descrição para QA |
|--------|-----------|----------|-------------------|
| `1.0.0` | `PropriedadeNula(propriedade)` | "{propriedade} não pode ser nulla." | Ocorre quando a instância ou dados essenciais da entidade Setor são enviados como nulos na requisição. |

#### 1.3 Funcionário (Código 2.0.x)

> `CRMUNI.DOMAIN/Validacoes/Entidades/Funcionarios/FuncionarioMensagens.cs`

| Código | Constante | Mensagem | Descrição para QA |
|--------|-----------|----------|-------------------|
| `2.0.0` | `PropriedadeNula(propriedade)` | "{propriedade} não pode ser nulla." | Ocorre quando a instância ou dados essenciais da entidade Funcionário são enviados como nulos na requisição. |

#### 1.4 Contato (Código 3.0.x)

> `CRMUNI.DOMAIN/Validacoes/Entidades/Contatos/ContatoMensagens.cs`

| Código | Constante | Mensagem | Descrição para QA |
|--------|-----------|----------|-------------------|
| `3.0.0` | `PropriedadeNula(propriedade)` | "{propriedade} não pode ser nulla." | Ocorre quando qualquer propriedade obrigatória da entidade Contato (`Nome`, `Celular` ou `Email`) é enviada como nula na requisição. |
| `3.0.6` | `SituacaoInvalida` | "Situação inválida." | Disparado quando o valor informado para `EContatoSituacao` não corresponde a nenhum valor definido no enumerador (`Ativo`, `Inativo`). |

---

### 2. Objetos de Valor

#### 2.1 E-mail (Código 0.0.x)

> `CRMUNI.DOMAIN/Validacoes/ObjetosValor/Emails/EmailMensagens.cs`

| Código | Constante | Mensagem | Descrição para QA |
|--------|-----------|----------|-------------------|
| `0.0.0` | `EmailNulo` | "Email nullo." | Disparado quando o valor do e-mail é passado como nulo para o objeto de valor. |
| `0.0.1` | `EmailObrigatorio` | "O email deve ser informado." | Disparado quando o e-mail é obrigatório no fluxo atual, porém foi enviado vazio/em branco. |
| `0.0.2` | `EmailCaracteresMinimo` | "Email deve conter no mínimo 6 caracteres." | Validação de tamanho mínimo. Dispara se o e-mail tiver menos de 6 caracteres. |
| `0.0.3` | `EmailCaracteresMaximo` | "Email deve conter no maximo 256 caracteres." | Validação de limite máximo de caracteres permitido para o e-mail (excede 256). |
| `0.0.4` | `EmailInvalido` | "O email informado está em um formato inválido." | Disparado quando a estrutura do e-mail não atende ao padrão esperado de endereço (falha na regex do e-mail). |

#### 2.2 Nome (Código 0.1.x)

> `CRMUNI.DOMAIN/Validacoes/ObjetosValor/Nomes/NomeMensagens.cs`

| Código | Constante | Mensagem | Descrição para QA |
|--------|-----------|----------|-------------------|
| `0.1.0` | `NomeNulo` | "Nome não pode ser nullo" | Disparado se o atributo nome for enviado nulo. |
| `0.1.1` | `NomeObrigatorio` | "Nome deve ser informado." | Disparado quando o campo nome for obrigatório e estiver em branco. |
| `0.1.3` | `PrimeiroNomeCaracteresMaximo` | "Primeiro nome deve possuir no máximo {max} caracteres." | O primeiro nome excede a quantidade de caracteres configurada para {max}. |
| `0.1.4` | `PrimeiroNomeCaracteresMinimo` / `NomeInvalido` | "Primeiro nome deve possuir no mínimo {min} caracteres." ou "Nome inválido." | O primeiro nome tem menos caracteres que o limite mínimo {min} ou possui caracteres não permitidos. |
| `0.1.5` | `SegundoNomeObrigatorio` | "Segundo nome deve ser informado." | Disparado quando o sobrenome/segundo nome é exigido na validação mas foi enviado em branco. |
| `0.1.11` | `SegundoNomeCaracteresMaximo` | "Segundo nome deve possuir no máximo {max} caracteres." | O segundo nome ultrapassa o número máximo de caracteres parametrizado ({max}). |
| `0.1.12` | `SegundoNomeCaracteresMinimo` | "Segundo nome deve possuir no mínimo {min} caracteres." | O segundo nome não atinge a quantidade mínima de caracteres parametrizada ({min}). |

#### 2.3 Telefone / Celular (Código 0.2.x)

> `CRMUNI.DOMAIN/Validacoes/ObjetosValor/Telefones/TelefoneMensagens.cs`

| Código | Constante | Mensagem | Descrição para QA |
|--------|-----------|----------|-------------------|
| `0.2.0` | `TelefoneNulo` | "Telefone/Celular não pode ser nullo" | Disparado quando o objeto ou valor de telefone/celular é enviado como nulo. |
| `0.2.1` | `TelefoneObrigatorio` | "Telefone/Celular deve ser informado." | Ocorrerá se o campo de telefone for obrigatório e o usuário tentar salvar vazio. |
| `0.2.3` | `TelefoneCaracteresObrigatorios` | "Telefone/Celular deve possuir no máximo {max} dígitos." | Excede a quantidade permitida de dígitos para números telefônicos ({max}). |
| `0.2.4` | `TelefoneInvalido` | "Telefone/Celular informado está inválido." | Dispara quando o número do telefone/celular informado contém caracteres inválidos ou falha na regra de formato. |

#### 2.4 Documento — CNPJ/CPF (Código 0.3.x)

> `CRMUNI.DOMAIN/Validacoes/ObjetosValor/Documentos/DocumentoMensagens.cs`

| Código | Constante | Mensagem | Descrição para QA |
|--------|-----------|----------|-------------------|
| `0.3.0` | `DocumentonNulo` | "Documento não pode ser nullo" | Disparado quando o valor do documento for fornecido como nulo na requisição. |
| `0.3.1` | `DocumentoObrigatorio` | "Documento deve ser informado." | Ocorre quando o CPF ou CNPJ for obrigatório para o cadastro e não for informado. |
| `0.3.3` | `DocumentoCaracteresObrigatorios` | "Documento deve possuir no máximo {max} dígitos." | O documento informado possui mais dígitos que o limite permitido ({max}). |
| `0.3.4` | `DocumentoInvalido` | "Documento informado está inválido." | Falha nos dígitos verificadores do CPF/CNPJ ou formato inválido. |

#### 2.5 Descrição (Código 0.4.x)

> `CRMUNI.DOMAIN/Validacoes/ObjetosValor/Descricoes/DescricaoMensagens.cs`

| Código | Constante | Mensagem | Descrição para QA |
|--------|-----------|----------|-------------------|
| `0.4.0` | `DescricaoNula` | "Descrição não pode ser nullo" | Disparado quando o objeto ou valor do campo de descrição for nulo. |
| `0.4.1` | `DescricaoObrigatoria` | "Descrição deve ser informado." | Disparado quando o preenchimento da descrição for obrigatório e for enviado vazio. |
| `0.4.2` | `DescricaoMinimo` | "Descrição deve conter no mínimo {min} caracteres." | O texto informado no campo descrição não possui a quantidade mínima de caracteres ({min}). |
| `0.4.3` | `DescricaoMaximo` | "Descrição deve possuir no máximo {max} dígitos." | O texto informado excede o limite máximo de caracteres/dígitos parametrizado ({max}). |

#### 2.6 Senha (Código 0.5.x)

> `CRMUNI.DOMAIN/Validacoes/ObjetosValor/Funcionarios/Senhas/SenhaMensagens.cs`

| Código | Constante | Mensagem | Descrição para QA |
|--------|-----------|----------|-------------------|
| `0.5.0` | `SenhaNula` | "Senha não pode ser nula" | Disparado quando o objeto Senha passado para atualização for nulo. |
| `0.5.1` | `SenhaObrigatoria` | "Senha deve ser informada." | Disparado quando o salt ou hash for enviado vazio, nulo ou apenas com espaços em branco. |
| `0.5.11` | `SenhasIdenticas` | "As senhas são identicas." | Disparado quando a nova senha informada for idêntica à senha atual. |

> **Nota:** Códigos `0.5.0` até `0.5.10` são reservados para códigos "COMUNS" (Nula/Obrigatória). Códigos `0.5.11+` destinam-se a regras de negócio específicas.
