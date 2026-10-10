using System.ComponentModel.DataAnnotations.Schema;
using CRMUNI.Domaidd.Entidades.Atendimentos;
using CRMUNI.Domaidd.Entidades.EntidadesBase;
using CRMUNI.Domaidd.Entidades.Mensagens;
using CRMUNI.Domaidd.Entidades.Setores;
using CRMUNI.Domaidd.ObjetosValor.Funcionarios;
using CRMUNI.Domaidd.ObjetosValor.Geral;
using CRMUNI.Domaidd.ObjetosValor.Nomes;
using CRMUNI.Domaidd.Validacoes.Entidades.Funcionarios;
using CRMUNI.Domaidd.Validacoes.Utilitarios;

namespace CRMUNI.Domaidd.Entidades.Funcionarios;

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