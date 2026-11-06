using CRMUNI.Domain.Entidades.Atendimentos;
using CRMUNI.Domain.Entidades.Contatos;
using CRMUNI.Domain.Entidades.EntidadesBase;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.ObjetosValor.descricoes;
using CRMUNI.Domain.Validacoes.Entidades.Mensagens;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.Entidades.Mensagens;

/// <summary>
/// Mensagem de um atendimento. O remetente e quem estiver preenchido: Contato (cliente) ou Funcionario.
/// </summary>
public sealed class Mensagem : EntidadeIdInt
{
    public Funcionario? Funcionario { get; private set; }
    public Contato? Contato { get; private set; }
    public Conteudo Conteudo { get; private set; }
    public Atendimento Atendimento { get; private set; }

    public bool EnviadaPeloFuncionario => Funcionario is not null;

    public Mensagem(){}

    private Mensagem(Atendimento atendimento, Conteudo conteudo, bool doFuncionario)
    {
        ValidaNulo.Verifica(atendimento, MensagemMensagens.PropriedadeNula("Atendimento"));
        ValidaNulo.Verifica(conteudo, MensagemMensagens.PropriedadeNula("Conteudo"));

        Atendimento = atendimento;
        Conteudo = conteudo;

        if (doFuncionario)
        {
            ValidaNulo.Verifica(atendimento.Funcionario, MensagemMensagens.PropriedadeNula("Funcionario"));
            Funcionario = atendimento.Funcionario;
        }
        else
        {
            ValidaNulo.Verifica(atendimento.Contato, MensagemMensagens.PropriedadeNula("Contato"));
            Contato = atendimento.Contato;
        }
    }

    public static Mensagem DoCliente(Atendimento atendimento, Conteudo conteudo)
        => new(atendimento, conteudo, doFuncionario: false);

    public static Mensagem DoFuncionario(Atendimento atendimento, Conteudo conteudo)
        => new(atendimento, conteudo, doFuncionario: true);
}
