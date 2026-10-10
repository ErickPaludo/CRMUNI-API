using System.ComponentModel.DataAnnotations.Schema;
using CRMUNI.Domain.Entidades.Atendimentos;
using CRMUNI.Domain.Entidades.EntidadesBase;
using CRMUNI.Domain.Entidades.Mensagens;
using CRMUNI.Domain.Entidades.Setores;
using CRMUNI.Domain.ObjetosValor.Funcionarios;
using CRMUNI.Domain.ObjetosValor.Geral;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Validacoes.Entidades.Funcionarios;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.Entidades.Funcionarios;

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