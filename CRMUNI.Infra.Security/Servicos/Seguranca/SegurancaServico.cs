using System.Security.Cryptography;
using System.Text;
using CRMUNI.Application.Excecoes;
using CRMUNI.Application.Interfaces.Seguranca;
using CRMUNI.Domain.ObjetosValor.Funcionarios;
using CRMUNI.Infra.Security.Execoes;
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

        public Senha ConverteSenhaParaHash(string senha, string confirmacaoSenha)
        {
            ValidaNivelDeSegurancaSenha(senha);

            byte[] saltBytes = GeraSaltAleatorio();
            var cripto = CriptografaSenha(senha, saltBytes);

            var hash = cripto.GetBytes(32);
            return MontaSenha(saltBytes, hash);
        }

        public void ValidaSenhaArgon(Senha senha, string senhaTexto)
        {
            byte[] saltBytes = Convert.FromBase64String(senha.Salt);
            byte[] hashBytes = Convert.FromBase64String(senha.Hash);

            Argon2id novaSenhaCriptografada = CriptografaSenha(senhaTexto, saltBytes);
            byte[] novoHash = novaSenhaCriptografada.GetBytes(32);


            bool valido = CryptographicOperations.FixedTimeEquals(hashBytes, novoHash);
            if (!valido)
                throw new NaoAutorizadoExecao("Usuário ou senha incorretos.");
        }

        private Argon2id CriptografaSenha(string senhaTexto, byte[] salt)
            => new Argon2id(Encoding.UTF8.GetBytes(senhaTexto))
            {
                DegreeOfParallelism = 2,
                MemorySize = 4000,
                Iterations = 20,
                Salt = salt,
                KnownSecret = UtilSeguranca.ConverteParaBytes(_segurancaConfig.Pepper)
            };

        private Senha MontaSenha(byte[] salt, byte[] hash)
            => new Senha(Convert.ToBase64String(salt), Convert.ToBase64String(hash));

        private void ValidaNivelDeSegurancaSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new Exception("Informe a senha");

            if (senha.Length < 8)
                throw new Exception("A senha deve conter no mínimo 8 caracteres");

            if (senha.Length > 16)
                throw new Exception("A senha deve conter no máximo 16 caracteres");

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

        private byte[] GeraSaltAleatorio()
            => UtilSeguranca.GeraBytesAleatorios(32);
    }
}