using CRMUNI.Domaidd.Entidades.Atendimentos;
using CRMUNI.Domaidd.Entidades.Contatos;
using CRMUNI.Domaidd.Entidades.EntidadesBase;
using CRMUNI.Domaidd.Entidades.Funcionarios;
using CRMUNI.Domaidd.ObjetosValor.descricoes;
using CRMUNI.Domaidd.Validacoes.Entidades.Mensagens;
using CRMUNI.Domaidd.Validacoes.Utilitarios;

namespace CRMUNI.Domaidd.Entidades.Mensagens;

public sealed class Mensagem : EntidadeIdInt
{
    public Funcionario? Funcionario { get; }
    public Contato? Contato { get;}
    public Conteudo Conteudo { get; private set; }
    public Atendimento Atendimento { get; }
    
    public Mensagem(){}
    public Mensagem(Atendimento atendimento, Funcionario funcionario, Contato? contato,Conteudo? conteudo)
    {
        ValidaNulo.Verifica(atendimento,MensagemMensagens.PropriedadeNula("Atendimento"));
        ValidaNulo.Verifica(funcionario,MensagemMensagens.PropriedadeNula("Funcionario"));
        ValidaNulo.Verifica(contato,MensagemMensagens.PropriedadeNula("Contato"));
        ValidaNulo.Verifica(conteudo,MensagemMensagens.PropriedadeNula("Conteudo"));
        
        Atendimento = atendimento;
        Funcionario = funcionario;
        Contato = contato;
        Conteudo = conteudo;
    }
}