namespace CRMUNI.DOMAIN.ObjetosValor.Telefones;

public sealed record Celular : Telefone
{
    public override int TamanhoNumeroTelefone { get; } = 12 + 1; //13 jamais ;D

    public Celular(){}
    private Celular(string numeroTelefone) : base(numeroTelefone) 
    {
    }
    
}