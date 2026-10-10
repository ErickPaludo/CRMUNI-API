using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.Etapas;
using CRMUNI.Domain.Entidades.Funis;
using CRMUNI.Domain.Entidades.Setores;
using CRMUNI.Domain.ObjetosValor.descricoes;
using CRMUNI.Domain.ObjetosValor.Descricoes;
using CRMUNI.Domain.ObjetosValor.Documentos;
using CRMUNI.Domain.ObjetosValor.Etapas;
using CRMUNI.Domain.ObjetosValor.Geral;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.ObjetosValor.Telefones;
using CRMUNI.Domain.Servicos;
using CRMUNI.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Application.Services;

public class EmpresaServico : IEmpresaServico
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly AppDbContext _appDbContext;

    public EmpresaServico(IUnityOfWork unitOfWork, AppDbContext appDbContext)
    {
        _unitOfWork = unitOfWork;
        _appDbContext = appDbContext;
    }

    public async Task Cadastra(EmpresaUsuarioDTO empresaUsuarioDTO)
    {
        var empresaDTO = empresaUsuarioDTO.Empresa;
        Email emailEmpresa = Email.Create(empresaDTO.Email);
        NomeEmpresa nomeEmpresa = new NomeEmpresa(empresaDTO.Nome);
        Telefone telefoneEmpresa = Telefone.Create(empresaDTO.Telefone);
        Cnpj cnpjEmpresa = new Cnpj(empresaDTO.Cnpj);

        Empresa empresa = new Empresa(emailEmpresa, nomeEmpresa, telefoneEmpresa, cnpjEmpresa);
        await _unitOfWork.EmpresaRepositorio.Insere(empresa);

        #region Setor Comercial

        NomeSetor nomeSetorComercial = new NomeSetor("Comercial");
        DescricaoSetor descricaoSetorComercial =
            new DescricaoSetor($"Setor responsavel pelas vendas de planos da {nomeEmpresa.Completo}");
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

        #region Funil Curioso

        NomeFunil nomeFunilCurioso = new NomeFunil("Curioso");
        DescricaoFunil descricaoFunilCurioso = new DescricaoFunil("Dedicado a clientes com duvidas sobre valores");
        Funil curioso = new Funil(empresa, EFunil.Curioso, nomeFunilCurioso, descricaoFunilCurioso);
        await _unitOfWork.FunilRepositorio.Insere(curioso);

        #endregion

        #region Funil Potencial Cliente

        NomeFunil nomeFunilPotencialCliente = new NomeFunil("Potencial Cliente");
        DescricaoFunil descricaoFunilPotencialCliente =
            new DescricaoFunil("Dedicado a clientes com duvidas sobre valores");
        Funil potencialCliente = new Funil(empresa, EFunil.PotencialCliente, nomeFunilPotencialCliente,
            descricaoFunilPotencialCliente);
        await _unitOfWork.FunilRepositorio.Insere(potencialCliente);

        #endregion

        #region Funil Potencial Cliente

        NomeFunil nomeFunilVendido = new NomeFunil("Vendido");
        DescricaoFunil descricaoFunilVendido =
            new DescricaoFunil("Dedicado a clientes com duvidas sobre valores");
        Funil vendido = new Funil(empresa, EFunil.Vendido, nomeFunilVendido,
            descricaoFunilVendido);
        await _unitOfWork.FunilRepositorio.Insere(vendido);

        #endregion

        #region Etapa Inicial

        NomeEtapa nomeInicial = new NomeEtapa("Inicial");
        Ordem ordemInicial = Ordem.Create(0);

        Etapa inicialCurioso = new Etapa(EEtapa.Inicial, curioso, nomeInicial, ordemInicial);

        Etapa inicialPotencialCliente = new Etapa(EEtapa.Inicial, potencialCliente, nomeInicial, ordemInicial);

        await _unitOfWork.EtapaRepositorio.Insere(inicialCurioso);
        await _unitOfWork.EtapaRepositorio.Insere(inicialPotencialCliente);

        #endregion

        #region Etapa PrimeiroContato

        NomeEtapa nomePrimeiroContato = new NomeEtapa("Primeiro Contato");
        Ordem ordemContatoCurioso = Ordem.Create(1);

        Etapa primeiroContatoCurioso =
            new Etapa(EEtapa.PrimeiroContato, curioso, nomePrimeiroContato, ordemContatoCurioso);

        Etapa primeiroContatoPotencialCliente =
            new Etapa(EEtapa.PrimeiroContato, potencialCliente, nomePrimeiroContato, ordemContatoCurioso);

        await _unitOfWork.EtapaRepositorio.Insere(primeiroContatoCurioso);
        await _unitOfWork.EtapaRepositorio.Insere(primeiroContatoPotencialCliente);

        #endregion

        #region Etapa Apresentacao de Planos

        NomeEtapa nomeApresentacaoPlanos = new NomeEtapa("Apresentação de Planos ");
        Ordem ordemApresentacaoPlanos = Ordem.Create(2);

        Etapa apresentacaoPlanosCurioso =
            new Etapa(EEtapa.ApresentacaoPlanos, curioso, nomeApresentacaoPlanos, ordemApresentacaoPlanos);

        Etapa apresentacaoPlanosPotencialCliente =
            new Etapa(EEtapa.ApresentacaoPlanos, potencialCliente, nomeApresentacaoPlanos, ordemApresentacaoPlanos);

        await _unitOfWork.EtapaRepositorio.Insere(apresentacaoPlanosCurioso);
        await _unitOfWork.EtapaRepositorio.Insere(apresentacaoPlanosPotencialCliente);

        #endregion

        #region Etapa Aguardando Decisao

        NomeEtapa nomeAguardandoDecisao = new NomeEtapa("Aguardando decisão");
        Ordem ordemAguardandoDecisao = Ordem.Create(3);

        Etapa aguardandoDecisaoPlanosCurioso =
            new Etapa(EEtapa.AguardandoDecicao, curioso, nomeAguardandoDecisao, ordemAguardandoDecisao);

        Etapa aguardandoDecisaoPotencialCliente =
            new Etapa(EEtapa.AguardandoDecicao, potencialCliente, nomeAguardandoDecisao, ordemAguardandoDecisao);

        await _unitOfWork.EtapaRepositorio.Insere(aguardandoDecisaoPlanosCurioso);
        await _unitOfWork.EtapaRepositorio.Insere(aguardandoDecisaoPotencialCliente);

        #endregion

        #region Etapa Conversao

        NomeEtapa nomeConversao = new NomeEtapa("Conversão");
        Ordem ordemConversao = Ordem.Create(4);

        Etapa conversaoPotencialCliente =
            new Etapa(EEtapa.Conversao, potencialCliente, nomeConversao, ordemConversao);

        Etapa vendidoPotencialCliente =
            new Etapa(EEtapa.Conversao, vendido, nomeConversao, ordemConversao);

        await _unitOfWork.EtapaRepositorio.Insere(conversaoPotencialCliente);
        await _unitOfWork.EtapaRepositorio.Insere(vendidoPotencialCliente);

        #endregion

        #region Etapa Concluido

        NomeEtapa nomeConcluido = new NomeEtapa("Concluído");
        Ordem ordemConcluido = Ordem.Create(5);

        Etapa concluidoCurioso = new Etapa(EEtapa.Concluido, curioso, nomeConcluido, ordemConcluido);

        Etapa concluidoPotencialCliente =
            new Etapa(EEtapa.Concluido, potencialCliente, nomeConcluido, ordemConcluido);

        Etapa concluidoVendido =
            new Etapa(EEtapa.Concluido, vendido, nomeConcluido, ordemConcluido);

        await _unitOfWork.EtapaRepositorio.Insere(concluidoCurioso);
        await _unitOfWork.EtapaRepositorio.Insere(concluidoPotencialCliente);
        await _unitOfWork.EtapaRepositorio.Insere(concluidoVendido);

        #endregion

        #region Etapa Perdido

        NomeEtapa nomePerdido = new NomeEtapa("Perdido");
        Ordem ordemPerdido = Ordem.Create(6);

        Etapa perdidoCurioso = new Etapa(EEtapa.Perdido, curioso, nomePerdido, ordemPerdido);

        Etapa perdidoPotencialCliente =
            new Etapa(EEtapa.Perdido, potencialCliente, nomePerdido, ordemPerdido);

        Etapa perdidoVendido =
            new Etapa(EEtapa.Perdido, vendido, nomePerdido, ordemPerdido);

        await _unitOfWork.EtapaRepositorio.Insere(perdidoCurioso);
        await _unitOfWork.EtapaRepositorio.Insere(perdidoPotencialCliente);
        await _unitOfWork.EtapaRepositorio.Insere(perdidoVendido);

        #endregion


        await _unitOfWork.Commit();
    }
}