using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMUNI.Infra.Seguranca.Configuracoes.Autenticacao
{
    public class AutenticaoConfig
    {
        public string SecretKeyJWT { get; set; }
        public int ExpiracaoEmMinutos { get; set; }
        public int ExpiracaoRefreshTokenDias { get; set; }
    }
}
