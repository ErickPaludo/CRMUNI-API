namespace CRMUNI.DOMAIN.Validacoes.Geral
{
    public static class Mensagens
    {
        public const string ID_IGUAL_MENOR_ZERO = "Id não pode ser menor que zero";
        public const string DATA_REGISTRO_INVALIDA = "Deve ser registrada a data atual, esta não pode ser manipulada.";
        public const string USUARIO_NAO_INFORMADO = "Usuário não informado!";
        public const string SITUACAO_INVALIDA = "Situação inválida.";
        public const string LIMITE_USUARIOS_MESTRES = "O limite de usuários mestres foi atingido. ";
        public const string USUARIO_INATIVO_NAO_PODE_SER_ATUALIZADO = "O usuário não está ativo!";
        public const string LIMITE_DE_CONVITES_PARA_USUARIOS_MESTRE = "Numero máximo de convites para usuários mestres atingido.";
        public const string TEMPO_NULO = "O tempo de expiração não pode ser nulo.";
        public const string COR_OBRIGATORIA = "A cor é obrigatória.";
        public const string TITULO_OBRIGATORIO = "O título é obrigatório.";
        public const string CODIGO_OBRIGATORIO = "O código é obrigatório.";

        public static string TITULO_NULO = "Título não pode ser nulo.";
        public static string CONTA_NULA = "Conta não pode ser nula.";
        public static string USUARIO_NULO = "Usuário não pode ser nulo.";
        public static string CONVITE_NULO = "Convite não pode ser nulo.";
        public static string SALDO_NULO = "Saldo não pode ser nulo.";

        public static string TITULO_TAMANHO_INVALIDO(int min, int max) => $"O título deve possuir entre {min} e {max} caracteres.";

        public static string OBSERVACAO_TAMANHO_INVALIDO(int tamanhoMaximo) => $"A observação deve possuir no máximo {tamanhoMaximo} caracteres.";
        

        public const string REMETENTE_NULO = "O remetente do convite não pode ser nulo.";
        public const string DESTINATARIO_NULO = "O destinatário do convite não pode ser nulo.";
        public const string ACESSO_INVALIDO = "Acesso inválido.";
        public static string PRECO_VENDA_NULO => "O preço de venda não pode ser nulo.";
        public static string MARGEM_LUCRO_NULA => "A margem de lucro não pode ser nula.";

        public static string USUARIO_OBRIGATORIO => "O usuário é obrigatório.";

        public static string REFRESH_TOKEN_NULO => "O refresh token não pode ser nulo.";
        public static string EXPIRATION_REFRESH_NULO => "O expiration refresh não pode ser nulo.";

        public static string MESA_INVALIDA => "Mesa deve ser informada";
    }
}
