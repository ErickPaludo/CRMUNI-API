using CRMUNI.Application.DTOs.Atendimentos;

namespace CRMUNI.Application.Interfaces;

public interface IAtendimentoChatServico
{
    /// <summary>
    /// Procura o contato da empresa pelo celular e, se houver, o atendimento em andamento.
    /// </summary>
    Task<IdentificacaoContatoDto> Identificar(Guid idEmpresa, string telefone);

    /// <summary>
    /// Cria o contato (se nao existir) e um atendimento com o funcionario do setor com menos atendimentos.
    /// Setor: 1 - Comercial, 2 - Financeiro, 3 - Suporte.
    /// </summary>
    Task<AtendimentoAbertoDto> Abrir(Guid idEmpresa, string telefone, string nome, int setor);

    /// <summary>
    /// Atendimentos em andamento (Situacao != Finalizado) do funcionario, com ou sem cliente conectado.
    /// </summary>
    Task<List<AtendimentoFuncionarioDto>> ListarDoFuncionario(int idFuncionario);
}
