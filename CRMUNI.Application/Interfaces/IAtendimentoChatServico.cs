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

    /// <summary>Atendimentos em andamento de todos os funcionarios da empresa (visao do administrador).</summary>
    Task<List<AtendimentoFuncionarioDto>> ListarDaEmpresa(Guid idEmpresa);

    /// <summary>Empresa do Usuario (administrador); null se o id nao e de um Usuario.</summary>
    Task<Guid?> EmpresaDoAdministrador(Guid idUsuario);

    Task<Guid> EmpresaDoAtendimento(int idAtendimento);

    /// <summary>Finaliza o atendimento do funcionario. Definitivo: nao pode ser reaberto.</summary>
    Task Finalizar(int idAtendimento, int idFuncionario);

    /// <summary>Passa o atendimento a um funcionario escolhido da mesma empresa.</summary>
    Task<AtendimentoAbertoDto> TransferirParaFuncionario(int idAtendimento, int idFuncionarioOrigem,
        int idFuncionarioDestino);

    /// <summary>Passa o atendimento ao funcionario do setor com menos atendimentos. Setor: 1, 2 ou 3.</summary>
    Task<AtendimentoAbertoDto> TransferirParaSetor(int idAtendimento, int idFuncionarioOrigem, int setor);
}
