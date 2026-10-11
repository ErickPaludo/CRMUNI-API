using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.Interfaces;
using CRMUNI.Application.Interfaces.Autenticacao;
using CRMUNI.Application.Interfaces.Seguranca;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.Usuarios;
using CRMUNI.Domain.ObjetosValor.Funcionarios;
using CRMUNI.Domain.ObjetosValor.Geral;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Servicos;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Application.Services;

public class UsuarioServico : IUsuarioServico
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly ISegurancaServico _segurancaServico;

    public UsuarioServico(IUnityOfWork unitOfWork,ISegurancaServico segurancaServico) 
    {
        _unitOfWork = unitOfWork;
        _segurancaServico = segurancaServico;
    }

    public async Task Cadastra(Empresa empresa,UsuarioDTO usuarioDto)
    {
        // if (await _unitOfWork.UsuarioRepositorio.IdExiste(x => x.Email.Endereco == request.Email.ToLower()))
        //     throw new UsuariosValidacao("Já existe um usuário cadastrado com esse e-mail.");

        var converteSenha = _segurancaServico.CriaSenhaArgon(usuarioDto.Senha,usuarioDto.ConfirmacaoSenha);
        NomeUsuario nomeUsuario = new NomeUsuario(usuarioDto.PrimeiroNome, usuarioDto.SegundoNome);
        Email email = Email.Create(usuarioDto.Email);
        Senha senha = new Senha(converteSenha.salt, converteSenha.hash);

        Usuario usuario = new Usuario(empresa, nomeUsuario, email, senha);

        await _unitOfWork.UsuarioRepositorio.Insere(usuario);
    }
}