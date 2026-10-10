using System.Security.Cryptography;
using System.Text;
using CRMUNI.Application.Interfaces.Seguranca;
using CRMUNI.Infra.Security.Uteis.Seguranca;
using CRMUNI.Infra.Seguranca.Configuracoes.Seguranca;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace CRMUNI.Infra.Security.Servicos.Seguranca
{
    public class SegurancaServico : ISegurancaServico
    {
        private readonly SegurancaConfig _segurancaConfig;
        public SegurancaServico(IOptions<SegurancaConfig> segurancaConfig)
        {
            _segurancaConfig = segurancaConfig.Value;
        }
        public (string salt, string hash) CriaSenhaArgon(string senha, string? salt = null)
        {
            ValidaNivelDeSegurancaSenha(senha, salt is not null);
            byte[] senhaBytes = Encoding.UTF8.GetBytes(senha);

            byte[] saltBytes = salt is null ? UtilSeguranca.GeraBytesAleatorios(32) : Convert.FromBase64String(salt);

            var cripto = new Argon2id(senhaBytes)
            {
                DegreeOfParallelism = 2,
                MemorySize = 4000,
                Iterations = 20,
                Salt = saltBytes,
                KnownSecret = UtilSeguranca.ConverteParaBytes(_segurancaConfig.Pepper)
            };

            var hash = cripto.GetBytes(32);
            string hashBase = Convert.ToBase64String(hash);
            string saltBase = Convert.ToBase64String(saltBytes);

            return (saltBase, hashBase);
        }

        public bool ValidaSenhaArgon(string senhaBanco, string senha, string salt)
        {
            return CryptographicOperations.FixedTimeEquals(
               UtilSeguranca.ConverteParaBytes(senhaBanco),
               UtilSeguranca.ConverteParaBytes(CriaSenhaArgon(senha, salt).hash)
            );
        }
        private void ValidaNivelDeSegurancaSenha(string senha,bool login = false)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new Exception("Informe a senha");

            if (senha.Length < 8)
                throw new Exception("A senha deve conter no mínimo 8 caracteres");

            if (senha.Length > 16)
                throw new Exception("A senha deve conter no máximo 16 caracteres");

            if (login)
                return;

            var nivelSeguranca = Zxcvbn.Core.EvaluatePassword(senha).Score;

            if (nivelSeguranca >= 3)
                return;

            string mensagem = nivelSeguranca switch
            {
                0 => "Senha extremamente fraca",
                1 => "Senha muito fraca",
                2 => "Senha fraca",
                _ => "Senha inválida"
            };

            throw new Exception(mensagem);
        }
    }
}
