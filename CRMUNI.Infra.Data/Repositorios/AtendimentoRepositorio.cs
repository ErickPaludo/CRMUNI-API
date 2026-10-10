using CRMUNI.Domain.Entidades.Atendimentos;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class AtendimentoRepositorio : BaseRepositorio<Atendimento>, IAtendimentoRepositorio
{
    public AtendimentoRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}