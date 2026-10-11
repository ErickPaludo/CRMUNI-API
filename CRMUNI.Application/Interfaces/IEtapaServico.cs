using CRMUNI.Domain.Entidades.Funis;
using CRMUNI.Domain.Entidades.Setores;

namespace CRMUNI.Application.Interfaces;

public interface IEtapaServico
{
    Task Cadastra(Funil[] funils);
}