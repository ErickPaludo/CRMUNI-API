using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRMUNI.Application.DTOs.Autenticacao;

public record AutenticacaoDto(string Email, string Senha);
