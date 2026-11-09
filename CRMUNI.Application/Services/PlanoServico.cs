using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.DTOs.Filtros;
using CRMUNI.Application.Excecoes;
using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Entidades.Planos;
using CRMUNI.Domain.Entidades.Usuarios;
using CRMUNI.Domain.ObjetosValor.Descricoes;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.ObjetosValor.Planos;
using CRMUNI.Domain.Servicos;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Application.Services;

public sealed class PlanoServico : IPlanoServico
{
    private readonly IUnityOfWork _unityOfWork;

    public PlanoServico(IUnityOfWork unityOfWork)
        => _unityOfWork = unityOfWork;


    public async Task<PlanoDto> Cadatro(string idUsuario, PlanoCadastroDto dto)
    {
        Usuario? usuario = await _unityOfWork.UsuarioRepositorio.BuscarObjetoUnico(x
            => x.Id.ToString() == idUsuario);

        if (usuario is null)
            throw new NaoPermitidoExcecao("Você não possui permissão para esta ação.");

        NomePlano nome = new NomePlano(dto.Nome);
        DescricaoPlano descricao = new DescricaoPlano(dto.Descricao);
        Preco preco = new Preco(dto.Preco);
        Plano plano = new Plano(usuario.Empresa, nome, preco, descricao);

        await _unityOfWork.PlanoRepositorio.Insere(plano);
        await _unityOfWork.Commit();

        return MapeamentoDto(plano);
    }

    public async Task<PlanosDto> Retorno(string idUsuario, PlanoFiltro filtros)
    {
        bool isUsuario = true;
        Funcionario? funcionario = null;

        Usuario? usuario =
            await _unityOfWork.UsuarioRepositorio.BuscarObjetoUnico(x => x.Id.ToString() == idUsuario);

        if (usuario is null)
            isUsuario = false;

        if (!isUsuario)
        {
            funcionario =
                await _unityOfWork.FuncionarioRepositorio.BuscarObjetoUnico(x => x.Id.ToString() == idUsuario);

            if (funcionario is null)
                throw new NaoEncontradoExecao("Usuário solicitante não encontrado.");
        }

        Empresa empresa = isUsuario ? usuario!.Empresa : funcionario!.Empresa;

        IQueryable<Plano> query = _unityOfWork.PlanoRepositorio.Retorno();

        query = query.Where(x => x.Empresa.Id.Equals(empresa.Id));

        if (filtros.Id.HasValue)
            query = query.Where(x => x.Id == filtros.Id.Value);

        if (filtros.Situacao.HasValue)
            query = query.Where(x => x.Situacao == filtros.Situacao.Value);

        if (filtros.Nome is not null)
            query = query.Where(x => x.Nome.Primeiro.Contains(filtros.Nome));

        List<Plano> planos = await query
            .OrderBy(x => x.Nome.Primeiro)
            .ToListAsync();

        List<PlanoDto> planoDtos = planos
            .Select(MapeamentoDto)
            .ToList();

        return new PlanosDto(planoDtos);
    }

    private static PlanoDto MapeamentoDto(Plano plano)
        => new PlanoDto(plano.Id, plano.Empresa.Id, plano.Nome.Completo, plano.Descricao.Texto, plano.Preco.Valor, plano.Situacao);
}
