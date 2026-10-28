using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.DTOs.Filtros;
using CRMUNI.Application.Excecoes;
using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Entidades.Setores;
using CRMUNI.Domain.Entidades.Usuarios;
using CRMUNI.Domain.ObjetosValor.descricoes;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Servicos;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Application.Services;

public class SetorServico : ISetorServico
{
    private readonly IUnityOfWork _unityOfWork;

    public SetorServico(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task Cadastra(Empresa empresa)
    {
        #region Setor Comercial

        NomeSetor nomeSetorComercial = new NomeSetor("Comercial");
        DescricaoSetor descricaoSetorComercial =
            new DescricaoSetor($"Setor responsavel pelas vendas de planos da {empresa.Nome.Completo}");
        Setor comercial = Setor.Create(ESetor.Comercial, empresa, nomeSetorComercial, descricaoSetorComercial);
        await _unityOfWork.SetorRepositorio.Insere(comercial);

        #endregion

        #region Setor Financeiro

        NomeSetor nomeSetorFinanceiro = new NomeSetor("Financeiro");
        DescricaoSetor descricaoSetorFinanceiro =
            new DescricaoSetor($"Responsavel pelos valores e acordos dos planos");
        Setor financeiro = Setor.Create(ESetor.Financeiro, empresa, nomeSetorFinanceiro, descricaoSetorFinanceiro);
        await _unityOfWork.SetorRepositorio.Insere(financeiro);

        #endregion

        #region Setor Suporte

        NomeSetor nomeSetorSuporte = new NomeSetor("Suporte");
        DescricaoSetor descricaoSetorSuporte =
            new DescricaoSetor($"O funcionamento desses seres se dá basicamente por café,ódio e chamados.");
        Setor suporte = Setor.Create(ESetor.Suporte, empresa, nomeSetorSuporte, descricaoSetorSuporte);
        await _unityOfWork.SetorRepositorio.Insere(suporte);

        #endregion
    }

    public async Task<SetoresDto> Retorno(string idUsuario,SetorFiltro filtros)
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
        
        IQueryable<Setor> query = _unityOfWork.SetorRepositorio.Retorno();

        query = query.Where(x => x.Empresa.Id == empresa.Id);

        if (filtros.Id is not null)
            query = query.Where(x => x.Id == filtros.Id);

        if (filtros.Tipo.HasValue)
            query = query.Where(x => x.Tipo == filtros.Tipo.Value);
        
        List<Setor> funcionarios = await query.ToListAsync();

        List<SetorDto> setorDto = funcionarios
            .Select(MapeamentoDto)
            .ToList();

        return new SetoresDto(setorDto);
    }
    
    public SetorDto MapeamentoDto(Setor setor)
    => new SetorDto(setor.Id,setor.Empresa.Id,setor.Tipo,setor.Nome.Completo,setor.DescricaoSetor.Texto);
}