using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.Geral;

namespace CRMUNI.DOMAIN.Entidades;

public sealed class Empresa : EntidadeIdGuid
{
    public Email Email { get; private set; }
    //public string Senha { get; set; } --Realmente nescessario?
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Cnpj { get; set; }
    
}