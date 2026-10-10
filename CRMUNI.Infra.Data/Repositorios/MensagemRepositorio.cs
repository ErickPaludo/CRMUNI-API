using CRMUNI.Domain.Entidades.Mensagens;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class MensagemRepositorio : BaseRepositorio<Mensagem>, IMensagemRepositorio
{
    public MensagemRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}