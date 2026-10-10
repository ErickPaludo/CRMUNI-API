using System.ComponentModel.DataAnnotations.Schema;
using CRMUNI.Domain.Entidades.Contatos;
using CRMUNI.Domain.Entidades.EntidadesBase;
using CRMUNI.Domain.Entidades.Etapas;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Entidades.Mensagens;
using CRMUNI.Domain.ObjetosValor.Atendimentos;
using CRMUNI.Domain.Validacoes.Entidades.Atendimentos;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.Entidades.Atendimentos;

public sealed class Atendimento : EntidadeIdInt
{
    public Contato? Contato { get; }
    public Funcionario Funcionario { get; private set; }
    public Etapa Etapa { get; private set; }
    public EAtendimentoSituacao Situacao { get; private set; }
    public Agendamento? Agendamento { get; private set; }

    public List<Mensagem> Mensagem { get; } = new();
    public Atendimento(){}
    private Atendimento(Contato contato, Funcionario funcionario,Etapa etapa)
    {
        ValidaNulo.Verifica(contato,AtendimentoMensagens.PropriedadeNula("Contato"));
        ValidaNulo.Verifica(funcionario,AtendimentoMensagens.PropriedadeNula("Funcionario"));
        ValidaNulo.Verifica(etapa,AtendimentoMensagens.PropriedadeNula("Etapa"));
        
        Contato = contato;
        Funcionario = funcionario;
        Etapa = etapa;
        Situacao = EAtendimentoSituacao.Aguardando;
    }

    public static Atendimento Create(Contato contato, Funcionario funcionario,Etapa etapa)
        => new(contato, funcionario,etapa);
}