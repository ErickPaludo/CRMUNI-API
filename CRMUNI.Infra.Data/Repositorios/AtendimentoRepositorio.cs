using CRMUNI.Domain.Entidades.Atendimentos;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Infra.Data.Repositorios;

public class AtendimentoRepositorio : BaseRepositorio<Atendimento>, IAtendimentoRepositorio
{
    private readonly AppDbContext _contexto;

    public AtendimentoRepositorio(AppDbContext contexto) : base(contexto)
    {
        _contexto = contexto;
    }

    public IQueryable<Atendimento> Retorno()
    {
        return _contexto.Atendimentos
            .Include(x => x.Funcionario)
            .Include(x => x.Contato)
            .Include(x => x.Etapa)
            .ThenInclude(e => e.Funil);
    }
}
