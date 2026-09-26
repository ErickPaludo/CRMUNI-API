using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.Entidades.Usuarios;

public class UsuarioValidacao : BaseValidacao, IValidacao<UsuarioValidacao>
{
    public UsuarioValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<UsuarioValidacao>(condicao, mensagem);
}