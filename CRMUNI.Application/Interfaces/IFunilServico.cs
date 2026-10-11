using CRMUNI.Domain.Entidades.Empresas;

namespace CRMUNI.Application.Interfaces;

public interface IFunilServico
{
    Task Cadastra(Empresa empresa);
}