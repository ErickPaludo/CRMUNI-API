using CRMUNI.Domain.Entidades.Contatos;
using CRMUNI.Domain.ObjetosValor.Telefones;

namespace CRMUNI.Application.Interfaces;

public interface IContatoServico
{
    Task<Contato?> Obter(Telefone telefone);
}