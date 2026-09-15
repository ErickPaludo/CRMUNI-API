namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Descricoes;

public static class DescricaoMensagens
{
    /// <summary>
    /// Codigo de mensagem 0.4.x
    /// Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"
    /// </summary>
    
    public const string DescricaoNula = "0.4.0 - Descricao não pode ser nullo";

    public const string DescricaoObrigatoria = "0.4.1 - Descricao deve ser informado.";

    public static string DescricaoMinimo(int minDescricao) =>
        $"0.4.2 - Descricao deve conter no mínimo {minDescricao} caracteres.";

    public static string DescricaoMaximo(int maxDescricao) =>
        $"0.4.3 - Descricao deve possuir no máximo {maxDescricao} dígitos.";

}