using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.Interfaces;
using CRMUNI.Application.Interfaces.Seguranca;
using CRMUNI.Domain.Servicos;

namespace CRMUNI.Application.Services;

public sealed class FuncionarioServico : IFuncionarioSevico
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly ISegurancaServico _segurancaServico;

    public FuncionarioServico(IUnityOfWork unityOfWork, ISegurancaServico segurancaServico)
    {
        _unityOfWork = unityOfWork;
        _segurancaServico = segurancaServico;
    }

    public async Task<FuncionarioRetornoDto> Cadastra(Guid IdUsuario,FuncionarioDto dto)
    {
        throw new NotImplementedException();
    }
}