using CRMUNI.DOMAIN.Entidades.Mensagens;
using CRMUNI.DOMAIN.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class MensagemRepositorio : BaseRepositorio<Mensagem>, IMensagemRepositorio
{
    public MensagemRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}