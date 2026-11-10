using System.Linq.Expressions;
using CRMUNI.Application.DTOs.Atendimentos;
using CRMUNI.Application.Excecoes;
using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Entidades.Atendimentos;
using CRMUNI.Domain.Entidades.Contatos;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.Etapas;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Entidades.Funis;
using CRMUNI.Domain.Entidades.Setores;
using CRMUNI.Domain.Execoes;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.ObjetosValor.Telefones;
using CRMUNI.Domain.Servicos;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Application.Services;

public sealed class AtendimentoChatServico : IAtendimentoChatServico
{
    private readonly IUnityOfWork _unityOfWork;

    public AtendimentoChatServico(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task<IdentificacaoContatoDto> Identificar(Guid idEmpresa, string telefone)
    {
        await ObterEmpresa(idEmpresa);
        Celular celular = Celular.Create(telefone);

        Contato? contato = await ObterContato(idEmpresa, celular);
        if (contato is null)
            return new IdentificacaoContatoDto(null, null, null, null, null);

        Atendimento? atendimento = await ObterAtendimentoEmAndamento(contato.Id);

        return new IdentificacaoContatoDto(
            contato.Id,
            atendimento?.Id,
            atendimento?.Funcionario.Id,
            atendimento?.Funcionario.Nome.Completo,
            contato.Nome.Completo);
    }

    public async Task<AtendimentoAbertoDto> Abrir(Guid idEmpresa, string telefone, string nome, int setor)
    {
        ESetor tipoSetor = ConverteSetor(setor);
        Empresa empresa = await ObterEmpresa(idEmpresa);
        Celular celular = Celular.Create(telefone);

        Contato? contato = await ObterContato(idEmpresa, celular);

        if (contato is not null)
        {
            // Protecao contra chamadas repetidas: nao abre um segundo atendimento em andamento.
            Atendimento? emAndamento = await ObterAtendimentoEmAndamento(contato.Id);
            if (emAndamento is not null)
                return MapeamentoDto(emAndamento.Id, emAndamento.Funcionario);
        }

        Funcionario funcionario = await ObterFuncionarioComMenosAtendimentos(idEmpresa, tipoSetor);
        Etapa etapa = await ObterEtapaInicial(idEmpresa);

        if (contato is null)
        {
            contato = Contato.Create(empresa, NomeContato.Create(nome), celular, EOrigem.Outros);
            await _unityOfWork.ContatoRepositorio.Insere(contato);
        }

        Atendimento atendimento = Atendimento.Create(contato, funcionario, etapa);
        await _unityOfWork.AtendimentoRepositorio.Insere(atendimento);
        await _unityOfWork.Commit();

        return MapeamentoDto(atendimento.Id, funcionario);
    }

    public Task<List<AtendimentoFuncionarioDto>> ListarDoFuncionario(int idFuncionario)
        => ListarEmAndamento(x => x.Funcionario.Id == idFuncionario);

    public Task<List<AtendimentoFuncionarioDto>> ListarDaEmpresa(Guid idEmpresa)
        => ListarEmAndamento(x => x.Funcionario.Empresa.Id == idEmpresa);

    public async Task<Guid?> EmpresaDoAdministrador(Guid idUsuario)
        => (await _unityOfWork.UsuarioRepositorio.BuscarObjetoUnico(x => x.Id == idUsuario))?.Empresa.Id;

    public async Task<Guid> EmpresaDoAtendimento(int idAtendimento)
        => await _unityOfWork.AtendimentoRepositorio.Retorno()
               .Where(x => x.Id == idAtendimento)
               .Select(x => (Guid?)x.Funcionario.Empresa.Id)
               .FirstOrDefaultAsync()
           ?? throw new NaoEncontradoExecao("Atendimento não encontrado.");

    private async Task<List<AtendimentoFuncionarioDto>> ListarEmAndamento(Expression<Func<Atendimento, bool>> filtro)
    {
        List<Atendimento> atendimentos = await _unityOfWork.AtendimentoRepositorio.Retorno()
            .Where(filtro)
            .Where(x => x.Situacao != EAtendimentoSituacao.Finalizado)
            .OrderBy(x => x.DthrCriacao)
            .ToListAsync();

        return atendimentos
            .Select(x => new AtendimentoFuncionarioDto(
                x.Id, x.Contato!.Nome.Completo, x.Contato.Celular.Numero,
                DateTime.SpecifyKind(x.DthrCriacao, DateTimeKind.Utc), x.Funcionario.Nome.Completo,
                x.Funcionario.Setor.Nome.Completo))
            .ToList();
    }

    public async Task Finalizar(int idAtendimento, int idFuncionario)
    {
        Atendimento atendimento = await ObterDoFuncionario(idAtendimento, idFuncionario);
        atendimento.Finalizar();
        _unityOfWork.AtendimentoRepositorio.Atualiza(atendimento);
        await _unityOfWork.Commit();
    }

    public async Task<AtendimentoAbertoDto> TransferirParaFuncionario(int idAtendimento, int idFuncionarioOrigem,
        int idFuncionarioDestino)
    {
        Atendimento atendimento = await ObterDoFuncionario(idAtendimento, idFuncionarioOrigem);

        Funcionario destino = await _unityOfWork.FuncionarioRepositorio.BuscarObjetoUnico(x
                                  => x.Id == idFuncionarioDestino && x.Empresa.Id == atendimento.Funcionario.Empresa.Id)
                              ?? throw new NaoEncontradoExecao("Funcionário de destino não encontrado.");

        return await Transferir(atendimento, destino);
    }

    public async Task<AtendimentoAbertoDto> TransferirParaSetor(int idAtendimento, int idFuncionarioOrigem, int setor)
    {
        ESetor tipoSetor = ConverteSetor(setor);
        Atendimento atendimento = await ObterDoFuncionario(idAtendimento, idFuncionarioOrigem);

        Funcionario destino =
            await ObterFuncionarioComMenosAtendimentos(atendimento.Funcionario.Empresa.Id, tipoSetor);

        return await Transferir(atendimento, destino);
    }

    private async Task<AtendimentoAbertoDto> Transferir(Atendimento atendimento, Funcionario destino)
    {
        atendimento.TransferirFuncionario(destino);
        _unityOfWork.AtendimentoRepositorio.Atualiza(atendimento);
        await _unityOfWork.Commit();
        return MapeamentoDto(atendimento.Id, destino);
    }

    private async Task<Atendimento> ObterDoFuncionario(int idAtendimento, int idFuncionario)
        => await _unityOfWork.AtendimentoRepositorio.Retorno()
               .FirstOrDefaultAsync(x => x.Id == idAtendimento && x.Funcionario.Id == idFuncionario
                                         && x.Situacao != EAtendimentoSituacao.Finalizado)
           ?? throw new NaoEncontradoExecao("Atendimento não encontrado.");

    private static ESetor ConverteSetor(int setor)
    {
        // Menu exibido ao cliente comeca em 1; o enum ESetor comeca em 0.
        ESetor tipo = (ESetor)(setor - 1);
        if (!Enum.IsDefined(tipo))
            throw new ExceptionDomain("Setor inválido. Opções: 1 - Comercial, 2 - Financeiro, 3 - Suporte.");

        return tipo;
    }

    private async Task<Empresa> ObterEmpresa(Guid idEmpresa)
        => await _unityOfWork.EmpresaRepositorio.BuscarObjetoUnico(x => x.Id == idEmpresa)
           ?? throw new NaoEncontradoExecao("Empresa não encontrada.");

    private async Task<Contato?> ObterContato(Guid idEmpresa, Celular celular)
        => await _unityOfWork.ContatoRepositorio.BuscarObjetoUnico(x
            => x.Empresa.Id == idEmpresa && x.Celular.Numero == celular.Numero);

    private async Task<Atendimento?> ObterAtendimentoEmAndamento(int idContato)
        => await _unityOfWork.AtendimentoRepositorio.Retorno()
            .Where(x => x.Contato!.Id == idContato && x.Situacao != EAtendimentoSituacao.Finalizado)
            .OrderByDescending(x => x.DthrCriacao)
            .FirstOrDefaultAsync();

    private async Task<Funcionario> ObterFuncionarioComMenosAtendimentos(Guid idEmpresa, ESetor setor)
        => await _unityOfWork.FuncionarioRepositorio.Retorno()
               .Where(x => x.Setor.Empresa.Id == idEmpresa && x.Setor.Tipo == setor)
               .OrderBy(x => x.Atendimentos.Count(a => a.Situacao != EAtendimentoSituacao.Finalizado))
               .ThenBy(x => x.Id)
               .FirstOrDefaultAsync()
           ?? throw new NaoEncontradoExecao($"Nenhum funcionário disponível no setor {setor}.");

    private async Task<Etapa> ObterEtapaInicial(Guid idEmpresa)
        => await _unityOfWork.EtapaRepositorio.Retorno()
               .Where(x => x.Funil.Empresa.Id == idEmpresa
                           && x.Funil.Tipo == EFunil.Curioso
                           && x.Tipo == EEtapa.Inicial)
               .FirstOrDefaultAsync()
           ?? throw new NaoEncontradoExecao("Etapa inicial do funil Curioso não cadastrada para a empresa.");

    private static AtendimentoAbertoDto MapeamentoDto(int idAtendimento, Funcionario funcionario)
        => new(idAtendimento, funcionario.Id, funcionario.Nome.Completo);
}
