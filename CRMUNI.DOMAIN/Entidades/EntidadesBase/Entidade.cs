namespace CRMUNI.DOMAIN.Entidades.EntidadesBase;

public abstract class Entidade
{
    public DateTime DthrCriacao { get; } = DateTime.UtcNow;
    public DateTime DthrAlteracao { get; protected set; }
}