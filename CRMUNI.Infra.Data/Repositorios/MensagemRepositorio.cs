using CRMUNI.Domain.Entidades.Mensagens;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Infra.Data.Repositorios;

public class MensagemRepositorio : BaseRepositorio<Mensagem>, IMensagemRepositorio
{
    private readonly AppDbContext _contexto;

    public MensagemRepositorio(AppDbContext contexto) : base(contexto)
    {
        _contexto = contexto;
    }

    public IQueryable<Mensagem> Retorno()
        => _contexto.Set<Mensagem>()
            .Include(x => x.Contato)
            .Include(x => x.Funcionario);
}
