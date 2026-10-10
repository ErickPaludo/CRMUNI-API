using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRMUNI.Infra.Seguranca.Uteis.Seguranca
{
    public static class UtilSeguranca
    {
        public static byte[] ConverteParaBytes(string valor)
        {
            return Encoding.UTF8.GetBytes(valor);
        }
        public static byte[] GeraBytesAleatorios(int tamanho)
        {
            return RandomNumberGenerator.GetBytes(tamanho);
        }
        public static string GeraBase64Aleatorios(int tamanho)
        {
            return Convert.ToBase64String(GeraBytesAleatorios(tamanho));
        }
        public static byte[] ConverteDeBase64Seguro(string base64)
        {
            if (string.IsNullOrEmpty(base64)) return Array.Empty<byte>();

            // Corrige o padding (preenchimento) caso esteja faltando
            string base64Corrigido = base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');

            // Substitui caracteres de URL-safe Base64 se existirem
            base64Corrigido = base64Corrigido.Replace('-', '+').Replace('_', '/');

            return Convert.FromBase64String(base64Corrigido);
        }
    }
}
