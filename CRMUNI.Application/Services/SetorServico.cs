using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.Setores;
using CRMUNI.Domain.ObjetosValor.descricoes;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Servicos;

namespace CRMUNI.Application.Services;

public class SetorServico : ISetorServico
{
    private readonly IUnityOfWork _unitOfWork;

    public SetorServico(IUnityOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Cadastra(Empresa empresa)
    {
        #region Setor Comercial

        NomeSetor nomeSetorComercial = new NomeSetor("Comercial");
        DescricaoSetor descricaoSetorComercial =
            new DescricaoSetor($"Setor responsavel pelas vendas de planos da {empresa.Nome.Completo}");
        Setor comercial = Setor.Create(ESetor.Comercial, empresa, nomeSetorComercial, descricaoSetorComercial);
        await _unitOfWork.SetorRepositorio.Insere(comercial);

        #endregion

        #region Setor Financeiro

        NomeSetor nomeSetorFinanceiro = new NomeSetor("Financeiro");
        DescricaoSetor descricaoSetorFinanceiro =
            new DescricaoSetor($"Responsavel pelos valores e acordos dos planos");
        Setor financeiro = Setor.Create(ESetor.Financeiro, empresa, nomeSetorFinanceiro, descricaoSetorFinanceiro);
        await _unitOfWork.SetorRepositorio.Insere(financeiro);

        #endregion

        #region Setor Suporte

        NomeSetor nomeSetorSuporte = new NomeSetor("Suporte");
        DescricaoSetor descricaoSetorSuporte =
            new DescricaoSetor($"O funcionamento desses seres se dá basicamente por café,ódio e chamados.");
        Setor suporte = Setor.Create(ESetor.Suporte, empresa, nomeSetorSuporte, descricaoSetorSuporte);
        await _unitOfWork.SetorRepositorio.Insere(suporte);

        #endregion
    }
}