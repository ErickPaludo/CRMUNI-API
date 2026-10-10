namespace CRMUNI.Domain.Validacoes.Geral;

public interface IValidacao<T> where T : BaseValidacao
{
    static abstract void Verifica(bool condicao, string mensagem);
}