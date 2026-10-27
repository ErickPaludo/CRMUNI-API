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

    public IQueryable<Funcionario> Retorno()
    {
        return _contexto.Funcionarios
            .Include(x => x.Setor);
    }
}