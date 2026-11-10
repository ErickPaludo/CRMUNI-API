using CRMUNI.Application.DTOs.Atendimentos;
using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.Excecoes;
using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Entidades.Atendimentos;
using CRMUNI.Domain.Entidades.Etapas;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Entidades.Usuarios;
using CRMUNI.Domain.Servicos;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Application.Services;

public sealed class AtendimentoServico : IAtendimentoServico
{
    private readonly IUnityOfWork _unityOfWork;

    public AtendimentoServico(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task<AtendimentosDto> Listar(string idUsuario)
    {
        IQueryable<Atendimento> query = _unityOfWork.AtendimentoRepositorio.Retorno();

        Usuario? usuario =
            await _unityOfWork.UsuarioRepositorio.BuscarObjetoUnico(x => x.Id.ToString() == idUsuario);

        if (usuario is not null)
        {
            query = query.Where(a => a.Funcionario.Empresa.Id == usuario.Empresa.Id);
        }
        else
        {
            Funcionario funcionario =
                await _unityOfWork.FuncionarioRepositorio.BuscarObjetoUnico(x => x.Id.ToString() == idUsuario)
                ?? throw new NaoEncontradoExecao("Usuário solicitante não encontrado.");

            query = query.Where(a => a.Funcionario.Empresa.Id == funcionario.Empresa.Id
                                     && a.Funcionario.Id == funcionario.Id);
        }

        List<Atendimento> atendimentos = await query.OrderByDescending(a => a.DthrCriacao).ToListAsync();

        return new AtendimentosDto(atendimentos.Select(MapeamentoDto).ToList());
    }

    public async Task AlterarEtapa(string idUsuario, int idAtendimento, int idEtapa)
    {
        IQueryable<Atendimento> query = _unityOfWork.AtendimentoRepositorio.Retorno()
            .Where(a => a.Id == idAtendimento);

        Usuario? usuario =
            await _unityOfWork.UsuarioRepositorio.BuscarObjetoUnico(x => x.Id.ToString() == idUsuario);

        Guid idEmpresa;
        if (usuario is not null)
        {
            idEmpresa = usuario.Empresa.Id;
            query = query.Where(a => a.Funcionario.Empresa.Id == idEmpresa);
        }
        else
        {
            Funcionario funcionario =
                await _unityOfWork.FuncionarioRepositorio.BuscarObjetoUnico(x => x.Id.ToString() == idUsuario)
                ?? throw new NaoEncontradoExecao("Usuário solicitante não encontrado.");

            idEmpresa = funcionario.Empresa.Id;
            query = query.Where(a => a.Funcionario.Id == funcionario.Id);
        }

        Atendimento atendimento = await query.FirstOrDefaultAsync()
                                  ?? throw new NaoEncontradoExecao("Atendimento não encontrado.");

        Etapa etapa = await _unityOfWork.EtapaRepositorio.Retorno()
                          .FirstOrDefaultAsync(e => e.Id == idEtapa && e.Funil.Empresa.Id == idEmpresa)
                      ?? throw new NaoEncontradoExecao("Etapa não encontrada.");

        atendimento.AlterarEtapa(etapa);
        _unityOfWork.AtendimentoRepositorio.Atualiza(atendimento);
        await _unityOfWork.Commit();
    }

    private static AtendimentoDetalheDto MapeamentoDto(Atendimento atendimento)
    {
        var etapa = atendimento.Etapa;
        var funil = etapa.Funil;
        var atendente = atendimento.Funcionario;

        return new AtendimentoDetalheDto(
            atendimento.Id,
            atendimento.Situacao,
            DateTime.SpecifyKind(atendimento.DthrCriacao, DateTimeKind.Utc),
            new AtendimentoEtapaDto(etapa.Id, etapa.Tipo, etapa.Nome.Completo, etapa.Ordem.Valor),
            new FunilEtapaDto(funil.Id, funil.Tipo, funil.Nome.Completo, funil.Descricao.Texto),
            new AtendenteDto(atendente.Id, atendente.Nome.Completo, atendente.Email.Endereco));
    }
}
