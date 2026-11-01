using CRMUNI.Domain.Validacoes.ObjetosValor.Nomes;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.ObjetosValor.Nomes;

public sealed record NomeContato : Nome
{
    public NomeContato(){}
    public NomeContato(string primeiroNome, string segundoNome) : base(primeiroNome, segundoNome)
    {
    }

    /// <summary>
    /// Cria a partir do nome completo digitado pelo contato: a primeira palavra e o primeiro nome,
    /// o restante e o segundo nome.
    /// </summary>
    public static NomeContato Create(string nomeCompleto)
    {
        ValidaNulo.Verifica(nomeCompleto, NomeMensagens.NomeNulo);

        string[] partes = nomeCompleto.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        NomeValidacao.Verifica(partes.Length == 0, NomeMensagens.NomeObrigatorio);
        NomeValidacao.Verifica(partes.Length < 2, NomeMensagens.SegundoNomeObrigatorio);

        return new NomeContato(partes[0], partes[1]);
    }
};
