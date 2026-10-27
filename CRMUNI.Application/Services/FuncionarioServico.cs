using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.DTOs.Filtros;
using CRMUNI.Application.Excecoes;
using CRMUNI.Application.Interfaces;
using CRMUNI.Application.Interfaces.Seguranca;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Entidades.Setores;
using CRMUNI.Domain.Entidades.Usuarios;
using CRMUNI.Domain.ObjetosValor.Funcionarios;
using CRMUNI.Domain.ObjetosValor.Geral;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Servicos;
using Microsoft.EntityFrameworkCore;

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

    public async Task<FuncionarioRetornoDto> Cadastra(Guid idUsuario, FuncionarioDto dto)
    {
        Usuario? usuario =
            await _unityOfWork.UsuarioRepositorio.BuscarObjetoUnico(x => x.Id == idUsuario);

        if (usuario is null)
            throw new NaoPermitidoExcecao("Você não possui permissão para esta ação.");


        Setor? setor =
            await _unityOfWork.SetorRepositorio.BuscarObjetoUnico(x => x.Id.Equals(dto.IdSetor));

        if (setor is null)
            throw new NaoEncontradoExecao("Setor não encontrado");

        NomeFuncionario nome = new NomeFuncionario(dto.PrimeiroNome, dto.SegundoNome);

        if (await _unityOfWork.UsuarioRepositorio.BuscarObjetoUnico(x
                => x.Email.Endereco.Equals(dto.Email)) is not null
            || await _unityOfWork.FuncionarioRepositorio.BuscarObjetoUnico(x
                => x.Email.Endereco.Equals(dto.Email)) is not null)
            throw new ConflitoExecao("Email já está em uso.");

        Email email = Email.Create(dto.Email);
        Senha senha = _segurancaServico.ConverteSenhaParaHash(dto.Senha, dto.ConfirmacaoSenha);

        Funcionario funcionario = new Funcionario(setor, nome, email, senha);
        await _unityOfWork.FuncionarioRepositorio.Insere(funcionario);
        await _unityOfWork.Commit();

        return MapeamentoCadastro(funcionario);
    }

    public async Task<List<FuncionarioRetornoDto>> Retorno(string idUsuario, FuncionarioFiltros filtros)
    {
        bool isUsuario = true;
        Funcionario? funcionario = null;

        Usuario? usuario =
            await _unityOfWork.UsuarioRepositorio.BuscarObjetoUnico(x => x.Id.ToString() == idUsuario);


        if (usuario is null)
            isUsuario = false;

        if (!isUsuario)
        {
            funcionario =
                await _unityOfWork.FuncionarioRepositorio.BuscarObjetoUnico(x => x.Id.ToString() == idUsuario);
            if (funcionario is null)
                throw new NaoEncontradoExecao("Usuário solicitante não encontrado.");
        }

        Empresa empresa = isUsuario ? usuario!.Empresa : funcionario!.Setor.Empresa;

        IQueryable<Funcionario> query = _unityOfWork.FuncionarioRepositorio.Retorno();

        query = query.Where(x => x.Setor.Empresa.Id == empresa.Id);
        
        if (filtros.Id.HasValue)
            query = query.Where(x => x.Id == filtros.Id);
        
        if (filtros.PrimeiroNome is not null)
            query = query.Where(x => x.Nome.Primeiro.Contains(filtros.PrimeiroNome));
        
        if (filtros.SegundoNome is not null)
            query = query.Where(x => x.Nome.Segundo!.Contains(filtros.SegundoNome));
        
        if (filtros.Email is not null)
            query = query.Where(x => x.Email.Endereco.Contains(filtros.Email));
        
        List<Funcionario> funcionarios = await query.ToListAsync();

        return funcionarios.Select(x => MapeamentoCadastro(x)).ToList();
    }

    private FuncionarioRetornoDto MapeamentoCadastro(Funcionario funcionario)
        => new FuncionarioRetornoDto(funcionario.Id,
            funcionario.Setor.Id,
            funcionario.Nome.Primeiro,
            funcionario.Nome.Segundo!,
            funcionario.Nome.Completo,
            funcionario.Email.Endereco);
}