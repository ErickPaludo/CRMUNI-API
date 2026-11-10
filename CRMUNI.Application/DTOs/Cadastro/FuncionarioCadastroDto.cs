namespace CRMUNI.Application.DTOs.Cadastro;

public record FuncionarioCadastroDto(
    Guid IdSetor,
    string PrimeiroNome,
    string SegundoNome,
    string Email,
    string Senha,
    string ConfirmacaoSenha);

public record FuncionarioRetornoDto(
    int Id,
    string PrimeiroNome,
    string SegundoNome,
    string NomeCompleto,
    string Email,
    int Atendimentos,
    SetorDto Setor,
    EmpresaRetornoDto Empresa);

public record FuncionarioDTO(FuncionarioRetornoDto Funcionario);

public record FuncionariosDto(
    List<FuncionarioDTO> Funcionarios
);

public record FuncionarioAtualizarDto(
    Guid? IdSetor,
    string? PrimeiroNome,
    string? SegundoNome,
    string? Email,
    FuncionarioAtuSenhaDto? Senha);

public record FuncionarioAtuSenhaDto(
    string Senha,
    string ConfirmacaoSenha);