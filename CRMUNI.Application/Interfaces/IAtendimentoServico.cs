using CRMUNI.Application.DTOs.Atendimentos;

namespace CRMUNI.Application.Interfaces;

public interface IAtendimentoServico
{
    /// <summary>
    /// Atendimentos da empresa do solicitante com etapa, funil e atendente.
    /// Usuario (admin) vê todos; funcionário vê só os seus.
    /// </summary>
    Task<AtendimentosDto> Listar(string idUsuario);

    /// <summary>Move o atendimento para outra etapa da mesma empresa.</summary>
    Task AlterarEtapa(string idUsuario, int idAtendimento, int idEtapa);
}
