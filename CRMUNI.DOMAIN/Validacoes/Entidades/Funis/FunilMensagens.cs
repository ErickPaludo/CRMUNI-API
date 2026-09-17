using System;
using System.Collections.Generic;
using System.Text;

namespace CRMUNI.DOMAIN.Validacoes.Entidades.Funis
{
    public static class FunilMensagens
    {
        /// <summary>
        /// Codigo de mensagem 7.0.x
        /// Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"
        /// </summary>
        /// 
        public static string PropriedadeNula(string propriedade) => $"7.0.0 - {propriedade} não pode ser nulla.";
    }
}
