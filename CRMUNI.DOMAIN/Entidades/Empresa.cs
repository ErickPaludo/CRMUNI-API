using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.Geral;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.ObjetosValor.Telefones;

namespace CRMUNI.DOMAIN.Entidades;

public sealed class Empresa : EntidadeIdGuid
{
    public Email Email { get; private set; }
    public NomeEmpresa Nome { get; set; }
    public Telefone Telefone { get; set; }
    public string Cnpj { get; set; }
    
    //public string Senha { get; set; } --Realmente nescessario?
}