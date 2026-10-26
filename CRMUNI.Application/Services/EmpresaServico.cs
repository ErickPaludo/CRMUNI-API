using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.Excecoes;
using CRMUNI.Application.Interfaces;
using CRMUNI.Application.Interfaces.Seguranca;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.ObjetosValor.Documentos;
using CRMUNI.Domain.ObjetosValor.Geral;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.ObjetosValor.Telefones;
using CRMUNI.Domain.Servicos;

namespace CRMUNI.Application.Services;

public class EmpresaServico : IEmpresaServico
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly ISetorServico _setorServico;
    private readonly IFunilServico _funilServico;
    private readonly IUsuarioServico _usuarioServico;

    private readonly ISegurancaServico _segurancaServico; //TEMPORARIO


    public EmpresaServico(IUnityOfWork unitOfWork, ISetorServico setorServico, IFunilServico funilServico,
        IUsuarioServico usuarioServico, ISegurancaServico segurancaServico)
    {
        _unitOfWork = unitOfWork;
        _setorServico = setorServico;
        _funilServico = funilServico;
        _usuarioServico = usuarioServico;
        _segurancaServico = segurancaServico;
    }

    public async Task<EmpresaRetornoDto> Cadastra(EmpresaUsuarioDto empresaUsuarioDto)
    {
        var empresaDto = empresaUsuarioDto.Empresa;
        Cnpj cnpjEmpresa = new Cnpj(empresaDto.Cnpj);

        if (await _unitOfWork.EmpresaRepositorio.BuscarObjetoUnico(e
                => e.Cnpj.Codigo == cnpjEmpresa.Codigo) != null)
            throw new ConflitoExecao("Empresa já cadastrada.");

        var usuarioDto = empresaUsuarioDto.Usuario;
        Email emailEmpresa = Email.Create(empresaDto.Email);
        NomeEmpresa nomeEmpresa = new NomeEmpresa(empresaDto.Nome);
        Telefone telefoneEmpresa = Telefone.Create(empresaDto.Telefone);

        Empresa empresa = new Empresa(emailEmpresa, nomeEmpresa, telefoneEmpresa, cnpjEmpresa);
        await _unitOfWork.EmpresaRepositorio.Insere(empresa);

        //Chama o serviço de cadastro Setor
        await _setorServico.Cadastra(empresa);

        //Chama o serviço de cadastro Funil
        await _funilServico.Cadastra(empresa);

        await _usuarioServico.Cadastra(empresa, usuarioDto);

        await _unitOfWork.Commit();

        return MapeamentoCadastro(empresa);
    }

    public EmpresaRetornoDto MapeamentoCadastro(Empresa empresa)
        => new EmpresaRetornoDto(empresa.Id,
            empresa.Nome.Completo,
            empresa.Cnpj.Codigo,
            empresa.Email.Endereco,
            empresa.Telefone.Numero);
}