using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Entidades.Contatos;
using CRMUNI.Domain.ObjetosValor.Telefones;
using CRMUNI.Domain.Servicos;

namespace CRMUNI.Application.Services;

public sealed class ContatoServico : IContatoServico
{
    private readonly IUnityOfWork _unityOfWork;

    public ContatoServico(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task<Contato?> Obter(Telefone telefone)
    {
        return await _unityOfWork.ContatoRepositorio.BuscarObjetoUnico(x
            => x.Celular.Numero.Equals(telefone.Numero));
    }
}