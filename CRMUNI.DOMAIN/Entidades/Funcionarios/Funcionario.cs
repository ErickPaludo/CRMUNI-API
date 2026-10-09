using System.ComponentModel.DataAnnotations.Schema;
using CRMUNI.DOMAIN.Entidades.Atendimentos;
using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.Entidades.Mensagens;
using CRMUNI.DOMAIN.Entidades.Setores;
using CRMUNI.DOMAIN.ObjetosValor.Funcionarios;
using CRMUNI.DOMAIN.ObjetosValor.Geral;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.Entidades.Funcionarios;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades.Funcionarios;

public sealed class Funcionario : EntidadeIdInt
{
    public Setor Setor { get; private set; }
    public NomeFuncionario Nome { get; private set; }
    public Email Email { get; private set; }
    public Senha Senha { get; private set; }
    
    public List<Mensagem> Mensagens  { get;} = new List<Mensagem>();
    public List<Atendimento> Atendimentos  { get;} = new();
    public Funcionario(){}
    public Funcionario(Setor setor, NomeFuncionario nome,Email email, Senha senha)
    {
        ValidaNulo.Verifica(setor,FuncionarioMensagens.PropriedadeNula("Setor"));
        ValidaNulo.Verifica(nome, FuncionarioMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(email, FuncionarioMensagens.PropriedadeNula("Email"));
        ValidaNulo.Verifica(senha, FuncionarioMensagens.PropriedadeNula("Setor"));
        
        Email = email;
        Setor = setor;
        Nome = nome;
        Senha = senha;
    }
}