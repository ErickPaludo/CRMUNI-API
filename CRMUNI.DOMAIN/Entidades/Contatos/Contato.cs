using CRMUNI.DOMAIN.Entidades.Empresas;
using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.Geral;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.ObjetosValor.Telefones;
using CRMUNI.DOMAIN.Validacoes.Entidades.Contatos;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades.Contatos;

public sealed class Contato : EntidadeIdInt
{
    public Empresa Empresa { get; }
    public NomeContato Nome { get; private set; }
    public Celular Celular { get; private set; }
    public Email Email { get; private set; }
    public EContatoSituacao Situacao { get; private set; }
    public EOrigem Origem { get; }
    //TODO: Adicionar CPF
    
    public Contato(){}
    private Contato(Empresa empresa,NomeContato nome, Celular celular, Email email, EContatoSituacao situacao, EOrigem origem)
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
    }

    public static Contato Create(Empresa empresa, NomeContato nome, Celular celular, Email email, EContatoSituacao situacao)
        => new(empresa, nome, celular, email, situacao,EOrigem.Outros);
    
    public static Contato Create(Empresa empresa,NomeContato nome, Celular celular, Email email, EContatoSituacao situacao,EOrigem origem)
        => new(empresa,nome, celular, email, situacao,origem);
}
