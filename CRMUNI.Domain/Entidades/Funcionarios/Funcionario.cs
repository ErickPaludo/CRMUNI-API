using System.ComponentModel.DataAnnotations.Schema;
using CRMUNI.Domain.Entidades.Atendimentos;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.EntidadesBase;
using CRMUNI.Domain.Entidades.Mensagens;
using CRMUNI.Domain.Entidades.Setores;
using CRMUNI.Domain.ObjetosValor.Funcionarios;
using CRMUNI.Domain.ObjetosValor.Geral;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Validacoes.Entidades.Funcionarios;
using CRMUNI.Domain.Validacoes.Entidades.Setores;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.Entidades.Funcionarios;

public sealed class Funcionario : EntidadeIdInt
{
    public Empresa Empresa { get; private set; }
    public Setor Setor { get; private set; }
    public NomeFuncionario Nome { get; private set; }
    public Email Email { get; private set; }
    public Senha Senha { get; private set; }

    public List<Mensagem> Mensagens { get; } = new List<Mensagem>();
    public List<Atendimento> Atendimentos { get; } = new();

    public Funcionario()
    {
    }

    public Funcionario(Empresa empresa, Setor setor, NomeFuncionario nome, Email email, Senha senha)
    {
        ValidaNulo.Verifica(empresa, FuncionarioMensagens.PropriedadeNula("Empresa"));
        ValidaNulo.Verifica(setor, FuncionarioMensagens.PropriedadeNula("Setor"));
        ValidaNulo.Verifica(nome, FuncionarioMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(email, FuncionarioMensagens.PropriedadeNula("Email"));
        ValidaNulo.Verifica(senha, FuncionarioMensagens.PropriedadeNula("Setor"));

        Empresa = empresa;
        Email = email;
        Setor = setor;
        Nome = nome;
        Senha = senha;

        empresa.AddFuncionario(this);
    }

    public void AlterarSetor(Setor setor)
    {
        ValidaNulo.Verifica(setor, FuncionarioMensagens.PropriedadeNula("Setor"));
        SetorValidacao.Verifica(Atendimentos.Any(a
            => !a.Situacao.Equals(EAtendimentoSituacao.Finalizado)), SetorMensagens.PossuiAtendimentosAtivos);    
        Setor = setor;
    }
    
    public void AlterarNome(NomeFuncionario nome)
    {
        ValidaNulo.Verifica(nome, FuncionarioMensagens.PropriedadeNula("Nome"));
        Nome = nome;
    }
    
    public void AlterarEmail(Email email)
    {
        ValidaNulo.Verifica(email, FuncionarioMensagens.PropriedadeNula("Email"));
        SetorValidacao.Verifica(Email == email, SetorMensagens.EmailsIdenticos);
        Email = email;
    }
    
    public void AlterarSenha(Senha senha)
    {
        ValidaNulo.Verifica(senha, FuncionarioMensagens.PropriedadeNula("Senha"));
        SetorValidacao.Verifica(Senha == senha, SetorMensagens.SenhasIdenticos);
        Senha = senha;
    }
}