namespace CRMUNI.Application.DTOs.Atendimentos;

/// <summary>Atendimento em andamento de um funcionario.</summary>
public record AtendimentoFuncionarioDto(int IdAtendimento, string NomeCliente, string TelefoneCliente, DateTime DataCriacao);
