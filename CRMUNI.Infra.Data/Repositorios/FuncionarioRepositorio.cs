using System.Linq.Expressions;
using CRMUNI.Domain.Entidades.Etapas;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Infra.Data.Repositorios;

public class FuncionarioRepositorio : BaseRepositorio<Funcionario>, IFuncionarioRepositorio
{
    private readonly AppDbContext _contexto;

    public FuncionarioRepositorio(AppDbContext contexto) : base(contexto)
    {
        _contexto = contexto;
    }

    public async Task<Funcionario?> BuscarObjetoUnico(System.Linq.Expressions.Expression<Func<Funcionario, bool>> predicado)
    {
        return await _contexto.Funcionarios
            .Include(x => x.Setor)
            .ThenInclude(s => s.Empresa)
            .Include(x => x.Empresa)
            .FirstOrDefaultAsync(predicado);
    }

    public IQueryable<Funcionario> Retorno()
    {
        return _contexto.Funcionarios
            .Include(x => x.Setor)
            .ThenInclude(s => s.Empresa)
            .Include(x => x.Empresa);
    }
}