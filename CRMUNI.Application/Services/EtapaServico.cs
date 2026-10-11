using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Entidades.Etapas;
using CRMUNI.Domain.Entidades.Funis;
using CRMUNI.Domain.Entidades.Setores;
using CRMUNI.Domain.ObjetosValor.Etapas;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Servicos;

namespace CRMUNI.Application.Services;

public class EtapaServico : IEtapaServico
{
    private readonly IUnityOfWork _unitOfWork;

    public EtapaServico(IUnityOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Cadastra(Funil[] funils)
    {
        Funil curioso = funils[0];
        Funil potencialCliente = funils[1];
        Funil vendido = funils[2];

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
    }
}