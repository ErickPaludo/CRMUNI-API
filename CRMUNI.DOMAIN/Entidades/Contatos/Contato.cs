using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.Geral;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.ObjetosValor.Telefones;
using CRMUNI.DOMAIN.Validacoes.Entidades.Contatos;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades.Contatos;

public sealed class Contato : EntidadeIdInt
{
    public NomeContato Nome { get; private set; }
    public Celular Celular { get; private set; }
    public Email Email { get; private set; }
    public EContatoSituacao Situacao { get; private set; }
    //TODO: Contato enum Insta, Google, Boca a boca outros

    private Contato(NomeContato nome, Celular celular, Email email, EContatoSituacao situacao)
    {
        ValidaNulo.Verifica(nome, ContatoMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(celular, ContatoMensagens.PropriedadeNula("Celular"));
        ValidaNulo.Verifica(email, ContatoMensagens.PropriedadeNula("Email"));

        ValidaNulo.Verifica(situacao, ContatoMensagens.PropriedadeNula("Email"));
        ValidaEnum<EContatoSituacao>.Verifica(situacao, ContatoMensagens.SituacaoInvalida);

        Nome = nome;
        Celular = celular;
        Email = email;
        Situacao = situacao;
    }

    public static Contato Create(NomeContato nome, Celular celular, Email email, EContatoSituacao situacao)
        => new(nome, celular, email, situacao);
}
