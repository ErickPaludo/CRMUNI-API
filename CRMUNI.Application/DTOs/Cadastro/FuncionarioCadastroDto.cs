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
    SetorDto Setor);

public record FuncionarioDTO(FuncionarioRetornoDto Funcionario);

public record FuncionariosDto(
    List<FuncionarioDTO> Funcionarios
);