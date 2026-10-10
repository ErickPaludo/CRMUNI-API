using System.ComponentModel.DataAnnotations.Schema;
using CRMUNI.Domaidd.Entidades.Contatos;
using CRMUNI.Domaidd.Entidades.EntidadesBase;
using CRMUNI.Domaidd.Entidades.Etapas;
using CRMUNI.Domaidd.Entidades.Funcionarios;
using CRMUNI.Domaidd.Entidades.Mensagens;
using CRMUNI.Domaidd.ObjetosValor.Atendimentos;
using CRMUNI.Domaidd.Validacoes.Entidades.Atendimentos;
using CRMUNI.Domaidd.Validacoes.Utilitarios;

namespace CRMUNI.Domaidd.Entidades.Atendimentos;

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