namespace CRMUNI.Domain.Entidades.EntidadesBase;

public abstract class EntidadeIdGuid : Entidade
{
    public Guid Id { get; } = Guid.CreateVersion7();
}