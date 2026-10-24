using CRMUNI.Domain.Entidades.Autenticacoes;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRMUNI.Infra.Data.Repositorios;

public class AutenticacaoRepositorio : BaseRepositorio<Autenticacao>, IAutenticacaoRepositorio
{
    private readonly AppDbContext _contexto;

    public AutenticacaoRepositorio(AppDbContext contexto) : base(contexto)
    {
        _contexto = contexto;
    }

    public async Task<Autenticacao?> BuscarObjetoUnico(Expression<Func<Autenticacao, bool>> predicado)
        => await _contexto.Set<Autenticacao>()
            .Include(a => a.Usuario)
            .FirstOrDefaultAsync(predicado);
}