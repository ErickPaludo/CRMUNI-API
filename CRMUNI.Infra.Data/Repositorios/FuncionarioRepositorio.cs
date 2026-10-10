using CRMUNI.Domain.Entidades.Etapas;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class FuncionarioRepositorio : BaseRepositorio<Funcionario>, IFuncionarioRepositorio
{
    public FuncionarioRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}