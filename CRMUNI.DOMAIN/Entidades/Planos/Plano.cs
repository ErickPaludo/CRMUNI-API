using CRMUNI.DOMAIN.Entidades.Empresas;
using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;

namespace CRMUNI.DOMAIN.Entidades.Planos;

public class Plano : EntidadeIdGuid
{
    public Empresa Empresa { get; }
    public NomePlano Nome { get; private set; }
}