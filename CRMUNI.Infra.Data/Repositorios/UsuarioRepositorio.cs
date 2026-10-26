using System.Linq.Expressions;
using CRMUNI.Domain.Entidades.Autenticacoes;
using CRMUNI.Domain.Entidades.Usuarios;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Infra.Data.Repositorios;

public class UsuarioRepositorio : BaseRepositorio<Usuario>, IUsuarioRepositorio
{
    private readonly AppDbContext _contexto;
    public UsuarioRepositorio(AppDbContext contexto) : base(contexto)
    {
        _contexto = contexto;
    }
    
    public async Task<Usuario?> BuscarObjetoUnico(Expression<Func<Usuario, bool>> predicado)
        => await _contexto.Set<Usuario>()
            .Include(a => a.Empresa)
            .FirstOrDefaultAsync(predicado);
    
}