using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.Excecoes;
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

    public UsuarioServico(IUnityOfWork unitOfWork, ISegurancaServico segurancaServico)
    {
        _unitOfWork = unitOfWork;
        _segurancaServico = segurancaServico;
    }

    public async Task<UsuarioRetornoDto> Cadastra(Empresa empresa, UsuarioDto usuarioDto)
    {
        Email email = Email.Create(usuarioDto.Email);

        if (await _unitOfWork.UsuarioRepositorio.BuscarObjetoUnico(e
                => e.Email.Endereco == email.Endereco) != null)
            throw new ConflitoExecao("Usuário já cadastrado.");

        Senha senha = _segurancaServico.ConverteSenhaParaHash(usuarioDto.Senha, usuarioDto.ConfirmacaoSenha);
        NomeUsuario nomeUsuario = new NomeUsuario(usuarioDto.PrimeiroNome, usuarioDto.SegundoNome);

        Usuario usuario = new Usuario(empresa, nomeUsuario, email, senha);

        await _unitOfWork.UsuarioRepositorio.Insere(usuario);

        return MapeamentoCadastro(usuario);
    }

    public UsuarioRetornoDto MapeamentoCadastro(Usuario usuario)
        => new UsuarioRetornoDto(usuario.Id,
            usuario.Empresa.Id,
            usuario.Nome.Primeiro,
            usuario.Nome.Segundo!,
            usuario.Nome.Completo,
            usuario.Email.Endereco);
}