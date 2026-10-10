using CRMUNI.Domain.Entidades.Atendimentos;
using CRMUNI.Domain.Entidades.Contatos;
using CRMUNI.Domain.Entidades.EntidadesBase;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.ObjetosValor.descricoes;
using CRMUNI.Domain.Validacoes.Entidades.Mensagens;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.Entidades.Mensagens;

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