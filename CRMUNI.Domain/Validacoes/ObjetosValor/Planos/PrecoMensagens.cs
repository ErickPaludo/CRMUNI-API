namespace CRMUNI.Domain.Validacoes.ObjetosValor.Planos;

public static class PrecoMensagens
{
    /*
     Codigo de mensagem 0.6.x
     Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"
    */
    
    public const string PrecoNulo = "0.6.0 - O preço não pode ser nullo";
    public const string SaldoInvalido = "0.6.11 - O preço deve ser maior que zero";
    public const string CasasDecimaisInvalidas = "0.6.12 - O preço deve ter no máximo 2 casas decimais";
    public const string ValorMaximoExcedido = "0.6.13 - O preço excede o valor máximo permitido";

}