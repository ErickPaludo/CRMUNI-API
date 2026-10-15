using CRMUNI.Application.DTOs.Autenticacao;
using CRMUNI.Application.Excecoes;
using CRMUNI.Application.Interfaces;
using CRMUNI.Application.Interfaces.Autenticacao;
using CRMUNI.Application.Interfaces.Seguranca;
using CRMUNI.Application.Modelos.Autenticacao;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Entidades.Usuarios;
using CRMUNI.Domain.ObjetosValor.Geral;
using CRMUNI.Domain.Servicos;

namespace CRMUNI.Application.Services;

public class AppAutenticacaoServico : IAppAutenticacaoServico
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IAutenticacaoServico _autenticacaoServico;
    private readonly ISegurancaServico _segurancaServico;

    public AppAutenticacaoServico(IUnityOfWork unitOfWork, IAutenticacaoServico autenticacaoServico,
        ISegurancaServico segurancaServico)
    {
        _unitOfWork = unitOfWork;
        _autenticacaoServico = autenticacaoServico;
        _segurancaServico = segurancaServico;
    }

    public async Task<ResultadoToken> GeraToken(AutenticacaoDto dto)
    {
        Email email = Email.Create(dto.Email);
        Usuario? usuario = await
            _unitOfWork.UsuarioRepositorio.BuscarObjetoUnico(x => x.Email.Endereco == email.Endereco);

        if (usuario is not null)
        {
            _segurancaServico.ValidaSenhaArgon(usuario.Senha.Hash,dto.Senha,usuario.Senha.Salt);
       
            return _autenticacaoServico.GeraToken(usuario.Id.ToString(), email.Endereco);
        }

        Funcionario? funcionario = await
            _unitOfWork.FuncionarioRepositorio.BuscarObjetoUnico(x => x.Email.Endereco == email.Endereco);

        if (funcionario is null)
            throw new NaoEncontradoExecao("Usuário ou senha incorreta");
        
        _segurancaServico.ValidaSenhaArgon(dto.Senha,funcionario.Senha.Hash,funcionario.Senha.Salt);
        return _autenticacaoServico.GeraToken(funcionario.Id.ToString(), email.Endereco);
    }
}