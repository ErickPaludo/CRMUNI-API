namespace CRMUNI.Application.DTOs.Cadastro;

public record EmpresaUsuarioDTO(
    EmpresaDTO Empresa,
    UsuarioDTO Usuario
);
public record EmpresaDTO(
    string Nome,
    string Cnpj,
    string Email,
    string Telefone
);

public record UsuarioDTO(
    string PrimeiroNome,
    string SegundoNome,
    string Email,
    string Senha
);