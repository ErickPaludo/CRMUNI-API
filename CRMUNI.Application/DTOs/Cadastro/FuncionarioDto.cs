namespace CRMUNI.Application.DTOs.Cadastro;

public record FuncionarioDto(Guid IdSetor, string PrimeiroNome,string SegundoNome,string Email,string Senha,string ConfirmacaoSenha);
public record FuncionarioRetornoDto(int Id,Guid IdSetor, string PrimeiroNome,string SegundoNome,string Email);