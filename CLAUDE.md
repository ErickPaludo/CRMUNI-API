# CRMUNI API - Guia do Projeto

## Comandos de Build e Testes
- **Compilar Solução**: `dotnet build CRMUNI.sln`
- **Executar Testes**: `dotnet test`
- **Executar API**: `dotnet run --project CRMUNI.UI.Api/CRMUNI.UI.Api.csproj`

## Arquitetura e Camadas

O projeto segue os princípios de Arquitetura Limpa (Clean Architecture) / Domain-Driven Design (DDD), dividido nas seguintes camadas:

1. **CRMUNI.DOMAIN**
   - **Entidades (`Entidades/`)**: Objetos de negócio centrais com identidade única (ex: `Funcionario`, `Usuario`, `Empresa`, `Atendimento`, `Contato`).
   - **Objetos de Valor (`ObjetosValor/`)**: Conceitos de domínio imutáveis definidos por seus atributos (ex: `Cpf`, `Cnpj`, `Email`, `Senha`, `Nome`, `Telefone`).
   - **Repositórios (`Repositorios/`)**: Interfaces que definem os contratos de acesso a dados.
   - **Exceções (`Execoes/`)**: Exceções específicas de validação e regras de negócio do domínio.

2. **CRMUNI.Application**
   - **Serviços (`Services/`)**: Orquestram a lógica de domínio e casos de uso (ex: `FuncionarioServico`, `UsuarioServico`, `EmpresaServico`).
   - **Interfaces (`Interfaces/`)**: Contratos para os serviços de aplicação.
   - **DTOs (`DTOs/`)**: Objetos de transferência de dados para entrada e saída (ex: filtros, modelos de retorno, DTOs de autenticação).
   - **Exceções (`Excecoes/`)**: Tratamento de erro em nível de aplicação (NotFound, Unauthorized, Conflict).

3. **CRMUNI.Infra.Data**
   - Implementa as interfaces de repositório e gerencia a persistência com Entity Framework Core, contextos de banco de dados e mapeamentos de entidades.

4. **CRMUNI.Infra.Security**
   - Gerencia autenticação, tokens JWT e operações de segurança criptográfica (ex: hash de senha via `Senha`).

5. **CRMUNI.Infra.IoC**
   - Configuração de Inversão de Controle / Injeção de Dependência, conectando serviços, repositórios e frameworks externos (como Swagger).

6. **CRMUNI.UI.Api**
   - **Controladores (`Controllers/`)**: Camada de apresentação expondo endpoints REST (ex: `FuncionarioController`, `UsuarioController`).
   - Ponto de entrada (`Program.cs`) configurando middleware, roteamento e serviços.

7. **CRMUNI.UnitTests.Domain**
   - Testes unitários cobrindo entidades de domínio, objetos de valor e regras de negócio.
