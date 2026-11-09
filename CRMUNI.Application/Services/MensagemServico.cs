using CRMUNI.Application.DTOs.Mensagens;
using CRMUNI.Application.Excecoes;
using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Entidades.Atendimentos;
using CRMUNI.Domain.Entidades.Mensagens;
using CRMUNI.Domain.ObjetosValor.descricoes;
using CRMUNI.Domain.Servicos;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Application.Services;

public sealed class MensagemServico : IMensagemServico
{
    private readonly IUnityOfWork _unityOfWork;

    public MensagemServico(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task<MensagemDto> GravaMensagem(int idAtendimento, int idFuncionario, ERemetente remetente,
        string texto)
    {
        Atendimento atendimento = await _unityOfWork.AtendimentoRepositorio.Retorno()
                                      .FirstOrDefaultAsync(x => x.Id == idAtendimento && x.Funcionario.Id == idFuncionario)
                                  ?? throw new NaoEncontradoExecao("Atendimento não encontrado.");

        Conteudo conteudo = new Conteudo(texto);
        Mensagem mensagem = remetente == ERemetente.Funcionario
            ? Mensagem.DoFuncionario(atendimento, conteudo)
            : Mensagem.DoCliente(atendimento, conteudo);

        await _unityOfWork.MensagemRepositorio.Insere(mensagem);
        await _unityOfWork.Commit();

        return MapeamentoDto(mensagem, idAtendimento);
    }

    public async Task<List<MensagemDto>> RetornaMensagens(int idAtendimento, int idFuncionario)
    {
        List<Mensagem> mensagens = await _unityOfWork.MensagemRepositorio.Retorno()
            .Where(x => x.Atendimento.Id == idAtendimento && x.Atendimento.Funcionario.Id == idFuncionario)
            .OrderBy(x => x.DthrCriacao)
            .ThenBy(x => x.Id)
            .ToListAsync();

        return mensagens.Select(m => MapeamentoDto(m, idAtendimento)).ToList();
    }

    private static MensagemDto MapeamentoDto(Mensagem mensagem, int idAtendimento)
    {
        // Gravado em UTC; o datetime2 do banco volta sem Kind.
        DateTime dataHora = DateTime.SpecifyKind(mensagem.DthrCriacao, DateTimeKind.Utc);

        return mensagem.EnviadaPeloFuncionario
            ? new(mensagem.Id, idAtendimento, mensagem.Funcionario!.Nome.Completo, ERemetente.Funcionario,
                mensagem.Conteudo.Texto, dataHora)
            : new(mensagem.Id, idAtendimento, mensagem.Contato!.Nome.Completo, ERemetente.Cliente,
                mensagem.Conteudo.Texto, dataHora);
    }
}
