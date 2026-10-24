namespace CRMUNI.Application.DTOs.Cadastro;

public record EmpresaUsuarioDto(
    EmpresaDto Empresa,
    UsuarioDto Usuario
);
public record EmpresaDto(
    string Nome,
    string Cnpj,
    string Email,
    string Telefone
);

public record UsuarioDto(
    string PrimeiroNome,
    string SegundoNome,
    string Email,
    string Senha,
    string ConfirmacaoSenha
);

public record EmpresaRetornoDto(
    Guid Id,
    string Nome,
    string Cnpj,
    string Email,
    string Telefone
);
public record UsuarioRetornoDto(
    Guid Id,
    Guid EmpresaId,
    string PrimeiroNome,
    string SegundoNome,
    string NomeCompleto,
    string Email
);