using System.Linq.Expressions;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Entidades.Setores;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Infra.Data.Repositorios;

public class SetorRepositorio : BaseRepositorio<Setor>, ISetorRepositorio
{
    public SetorRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}