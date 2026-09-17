using CRMUNI.DOMAIN.Validacoes.Entidades.Funcionarios;
using CRMUNI.DOMAIN.Validacoes.Geral;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMUNI.DOMAIN.Validacoes.Entidades.Funis
{
    public sealed class FunilValidacao : BaseValidacao,IValidacao<FunilValidacao>
    {
        public FunilValidacao(string erro) : base(erro)
        {
        }

        public static void Verifica(bool condicao, string mensagem)
            => VerificaExcessao<FunilValidacao>(condicao, mensagem);
    }
}
