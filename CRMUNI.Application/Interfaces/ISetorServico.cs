using CRMUNI.Domain.Entidades.Empresas;

namespace CRMUNI.Application.Interfaces;

public interface ISetorServico
{
    Task Cadastra(Empresa empresa);
}