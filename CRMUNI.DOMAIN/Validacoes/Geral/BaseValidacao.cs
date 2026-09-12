namespace CRMUNI.DOMAIN.Validacoes.Geral
{
    public abstract class BaseValidacao : Exception
    {
        protected BaseValidacao(string erro) : base(erro) { }
        public static void VerificaExcessao<T>(bool condicao, string mensagem) where T : BaseValidacao
        {
            if (condicao)
                throw (T)Activator.CreateInstance(typeof(T), mensagem)!;
        }
    }
}
