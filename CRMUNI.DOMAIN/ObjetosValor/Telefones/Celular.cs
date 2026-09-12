namespace CRMUNI.DOMAIN.ObjetosValor.Telefones;

public sealed record Celular : Telefone
{
    protected override int TamanhoNumeroTelefone { get; } = 12 + 1; //13 jamais ;D

    private Celular(string numeroTelefone) : base(numeroTelefone) 
    {
    }
    
}