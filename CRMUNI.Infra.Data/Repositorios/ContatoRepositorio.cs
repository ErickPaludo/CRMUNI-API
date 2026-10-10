using CRMUNI.Domain.Entidades.Contatos;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class ContatoRepositorio : BaseRepositorio<Contato>, IContatoRepositorio
{
    public ContatoRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}