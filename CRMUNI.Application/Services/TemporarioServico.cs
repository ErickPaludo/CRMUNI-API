using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Servicos;

namespace CRMUNI.Application.Services;

public class TemporarioServico : ITemporarioServico
{
    private readonly IUnityOfWork _unityOfWork;

    public TemporarioServico(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    // Ordem: dependentes antes das entidades referenciadas, para não violar FKs.
    public async Task DeleteAll()
    {
        await _unityOfWork.MensagemRepositorio.DeleteAll();
        await _unityOfWork.AtendimentoRepositorio.DeleteAll();
        await _unityOfWork.AutenticacaoRepositorio.DeleteAll();
        await _unityOfWork.ContatoRepositorio.DeleteAll();
        await _unityOfWork.FuncionarioRepositorio.DeleteAll();
        await _unityOfWork.EtapaRepositorio.DeleteAll();
        await _unityOfWork.FunilRepositorio.DeleteAll();
        await _unityOfWork.PlanoRepositorio.DeleteAll();
        await _unityOfWork.SetorRepositorio.DeleteAll();
        await _unityOfWork.UsuarioRepositorio.DeleteAll();
        await _unityOfWork.EmpresaRepositorio.DeleteAll();
    }
}
