
namespace CRMUNI.Infra.Security.Uteis.Autenticacao
{
    public static class UtilAutenticacao
    {
        public static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }
        public static long DateTimeInUnixTimestamp(DateTime date)
        {
            return new DateTimeOffset(date).ToUnixTimeSeconds();
        }
        public static DateTime UnixTimestampInDateTime(long unixTimestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;
        }
    }
}
