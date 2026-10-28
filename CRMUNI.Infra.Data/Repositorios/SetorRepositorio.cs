using System.Linq.Expressions;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Entidades.Setores;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Domain.Servicos;
using CRMUNI.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Infra.Data.Repositorios;

public class SetorRepositorio : BaseRepositorio<Setor>, ISetorRepositorio
{
    private readonly AppDbContext _contexto;

    public SetorRepositorio(AppDbContext contexto) : base(contexto)
    {
        _contexto = contexto;
    }

    public IQueryable<Setor> Retorno()
    {
        return _contexto.Setores
            .Include(x => x.Empresa);
    }
}