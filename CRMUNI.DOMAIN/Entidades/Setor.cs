using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;

namespace CRMUNI.DOMAIN.Entidades;

public sealed class Setor : EntidadeIdGuid
{
    public Empresa Empresa { get; }
    public NomeSetor Nome { get; private set; } 
}