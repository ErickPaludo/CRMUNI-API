using CRMUNI.Domaidd.Validacoes.Entidades.Funcionarios;
using System;
using System.Collections.Generic;
using System.Text;
using CRMUNI.Domaidd.Validacoes.Geral;

namespace CRMUNI.Domaidd.Validacoes.Entidades.Funis
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
