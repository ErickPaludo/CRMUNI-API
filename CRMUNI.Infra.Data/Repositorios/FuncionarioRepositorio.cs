using CRMUNI.DOMAIN.Entidades.Etapas;
using CRMUNI.DOMAIN.Entidades.Funcionarios;
using CRMUNI.DOMAIN.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class FuncionarioRepositorio : BaseRepositorio<Funcionario>, IFuncionarioRepositorio
{
    public FuncionarioRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}