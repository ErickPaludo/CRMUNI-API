using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.Entidades.Enumeradores;
using CRMUNI.DOMAIN.Validacoes.Entidades.Atendimentos;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades;

public sealed class Atendimento : EntidadeIdInt
{
    public Contato Contato { get; }
    public Funcionario Funcionario { get; private set; }
    public Etapa Etapa { get; private set; }
    public EAtendimentoSituacao Situacao { get; private set; }
    //Todo tempo atendimento


    public List<Mensagem> Mensagem { get; } = new();
    public List<Funcionario> Funcionarios { get; } = new(); //Pode ser redundante, ja que mensagem possui a entidade Funcionario, e vice versa

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