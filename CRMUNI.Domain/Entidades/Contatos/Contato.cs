using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.EntidadesBase;
using CRMUNI.Domain.Entidades.Planos;
using CRMUNI.Domain.ObjetosValor.Documentos;
using CRMUNI.Domain.ObjetosValor.Geral;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.ObjetosValor.Telefones;
using CRMUNI.Domain.Validacoes.Entidades.Contatos;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.Entidades.Contatos;

public sealed class Contato : EntidadeIdInt
{
    public Empresa Empresa { get; }
    public Cpf? Cpf { get; private set; }
    public NomeContato Nome { get; private set; }
    public Celular Celular { get; private set; }
    public Email Email { get; private set; }
    public Plano? Plano { get; private set; }
    public EContatoSituacao Situacao { get; private set; }
    public EOrigem Origem { get; }
    //TODO: Adicionar CPF
    
    public Contato(){}
    private Contato(Empresa empresa, NomeContato nome, Celular celular, Email email, EContatoSituacao situacao, EOrigem origem, Plano? plano)
    {
        ValidaNulo.Verifica(empresa, ContatoMensagens.PropriedadeNula("Empresa"));
        ValidaNulo.Verifica(nome, ContatoMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(celular, ContatoMensagens.PropriedadeNula("Celular"));
        ValidaNulo.Verifica(email, ContatoMensagens.PropriedadeNula("Email"));

        ValidaNulo.Verifica(situacao, ContatoMensagens.PropriedadeNula("Email"));
        ValidaEnum<EContatoSituacao>.Verifica(situacao, ContatoMensagens.EnumInvalido("Situacao"));
        
        ValidaNulo.Verifica(origem, ContatoMensagens.PropriedadeNula("Origem"));
        ValidaEnum<EOrigem>.Verifica(origem, ContatoMensagens.EnumInvalido("Origem"));
 
        Empresa = empresa;
        Nome = nome;
        Celular = celular;
        Email = email;
        Situacao = situacao;
        Origem = origem;
        Plano = plano;
    }

    public static Contato Create(Empresa empresa, NomeContato nome, Celular celular, Email email, EContatoSituacao situacao)
        => new(empresa, nome, celular, email, situacao,EOrigem.Outros,null);
    
    public static Contato Create(Empresa empresa,NomeContato nome, Celular celular, Email email, EContatoSituacao situacao,EOrigem origem)
        => new(empresa,nome, celular, email, situacao,origem,null);
}
