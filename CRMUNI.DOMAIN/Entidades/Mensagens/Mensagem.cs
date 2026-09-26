using CRMUNI.DOMAIN.Entidades.Contatos;
using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.Entidades.Funcionarios;
using CRMUNI.DOMAIN.ObjetosValor.descricoes;
using CRMUNI.DOMAIN.Validacoes.Entidades.Mensagens;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades.Mensagens;

public sealed class Mensagem : EntidadeIdInt
{
    public Funcionario Funcionario { get; }
    public Contato Contato { get;}
    public Conteudo Conteudo { get; private set; }
    
    private Mensagem(Funcionario funcionario, Contato contato,Conteudo conteudo)
    {
        ValidaNulo.Verifica(funcionario,MensagemMensagens.PropriedadeNula("Funcionario"));
        ValidaNulo.Verifica(contato,MensagemMensagens.PropriedadeNula("Contato"));
        ValidaNulo.Verifica(conteudo,MensagemMensagens.PropriedadeNula("Conteudo"));
        
        Funcionario = funcionario;
        Contato = contato;
        Conteudo = conteudo;
    }
    public static Mensagem Create(Funcionario funcionario, Contato contato, Conteudo conteudo)
        => new(funcionario, contato, conteudo);
}