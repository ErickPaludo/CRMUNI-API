using CRMUNI.Application.DTOs.Autenticacao;
using CRMUNI.Application.Excecoes;
using CRMUNI.Application.Interfaces;
using CRMUNI.Application.Interfaces.Autenticacao;
using CRMUNI.Application.Interfaces.Seguranca;
using CRMUNI.Application.Modelos.Autenticacao;
using CRMUNI.Domain.Entidades.Autenticacoes;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Entidades.Usuarios;
using CRMUNI.Domain.ObjetosValor.Funcionarios;
using CRMUNI.Domain.ObjetosValor.Geral;
using CRMUNI.Domain.Servicos;
using CRMUNI.Domain.Validacoes.Entidades.Autenticacoes;

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

    public async Task<ResultadoTokenDto> GeraToken(AutenticacaoDto dto)
    {
        Email email = Email.Create(dto.Email);

        Usuario? usuario = await _unitOfWork.UsuarioRepositorio.BuscarObjetoUnico(x => x.Email.Endereco == email.Endereco);
        if (usuario is not null)
            return await GeraTokenUsuario(usuario, dto);

        Funcionario? funcionario = await _unitOfWork.FuncionarioRepositorio.BuscarObjetoUnico(x => x.Email.Endereco == email.Endereco);
        if (funcionario is not null)
            return await GeraTokenFuncionario(funcionario, dto);

        throw new NaoEncontradoExecao("Usuário ou senha incorreta");
    }

    private async Task<ResultadoTokenDto> GeraTokenUsuario(Usuario usuario, AutenticacaoDto dto)
    {
        _segurancaServico.ValidaSenhaArgon(usuario.Senha, dto.Senha);
        var token = _autenticacaoServico.GeraToken(usuario.Id.ToString(), usuario.Email.Endereco);
        Autenticacao novaAutenticacao = new Autenticacao(usuario, token.refreshToken, token.expirationRefreshToken);

        Autenticacao? autenticacao = await _unitOfWork.AutenticacaoRepositorio.BuscarObjetoUnico(x => x.Usuario != null && x.Usuario.Id.Equals(usuario.Id));

        if (autenticacao is null)
            await _unitOfWork.AutenticacaoRepositorio.Insere(novaAutenticacao);
        else
        {
            autenticacao.AtualizaRefreshToken(token.refreshToken, token.expirationRefreshToken);
            _unitOfWork.AutenticacaoRepositorio.Atualiza(autenticacao);
        }

        await _unitOfWork.Commit();
        return token;
    }

    private async Task<ResultadoTokenDto> GeraTokenFuncionario(Funcionario funcionario, AutenticacaoDto dto)
    {
        _segurancaServico.ValidaSenhaArgon(funcionario.Senha, dto.Senha);
        var token = _autenticacaoServico.GeraToken(funcionario.Id.ToString(), funcionario.Email.Endereco);
        Autenticacao novaAutenticacao = new Autenticacao(funcionario, token.refreshToken, token.expirationRefreshToken);

        Autenticacao? autenticacao = await _unitOfWork.AutenticacaoRepositorio.BuscarObjetoUnico(x => x.Funcionario != null && x.Funcionario.Id.Equals(funcionario.Id));

        if (autenticacao is null)
            await _unitOfWork.AutenticacaoRepositorio.Insere(novaAutenticacao);
        else
        {
            autenticacao.AtualizaRefreshToken(token.refreshToken, token.expirationRefreshToken);
            _unitOfWork.AutenticacaoRepositorio.Atualiza(autenticacao);
        }

        await _unitOfWork.Commit();
        return token;
    }
   
    public async Task<ResultadoRefreshTokenDto> GeraRefreshToken(string refreshToken)
    {
        Autenticacao? auth =
            await _unitOfWork.AutenticacaoRepositorio.BuscarObjetoUnico(x
                => x.RefreshToken!.Equals(refreshToken));

        if (auth is null)
            throw new AutenticacaoValidacao("Token expirado.");

        auth.ValidaRefreshToken(refreshToken);

        var retRefreshToken = _autenticacaoServico.RefreshToken(auth!, refreshToken);

        auth.AtualizaRefreshToken(retRefreshToken.refreshToken, retRefreshToken.expirationRefreshToken);
        _unitOfWork.AutenticacaoRepositorio.Atualiza(auth);
        await _unitOfWork.Commit();

        return new ResultadoRefreshTokenDto(
            retRefreshToken.token,
            retRefreshToken.expirationTokenFormatado,
            retRefreshToken.refreshToken,
            retRefreshToken.expirationRefreshTokenFormatado
        );
    }
    
}