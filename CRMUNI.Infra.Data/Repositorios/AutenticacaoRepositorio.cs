using CRMUNI.Domain.Entidades.Autenticacoes;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class AutenticacaoRepositorio : BaseRepositorio<Autenticacao>, IAutenticacaoRepositorio
{
    public AutenticacaoRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}