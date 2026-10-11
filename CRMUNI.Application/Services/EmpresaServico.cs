using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.Interfaces;
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

    public EmpresaServico(IUnityOfWork unitOfWork, ISetorServico setorServico, IFunilServico funilServico,
        IUsuarioServico usuarioServico)
    {
        _unitOfWork = unitOfWork;
        _setorServico = setorServico;
        _funilServico = funilServico;
        _usuarioServico = usuarioServico;
    }

    public async Task Cadastra(EmpresaUsuarioDTO empresaUsuarioDto)
    {
        var empresaDto = empresaUsuarioDto.Empresa;
        var usuarioDto = empresaUsuarioDto.Usuario;
        Email emailEmpresa = Email.Create(empresaDto.Email);
        NomeEmpresa nomeEmpresa = new NomeEmpresa(empresaDto.Nome);
        Telefone telefoneEmpresa = Telefone.Create(empresaDto.Telefone);
        Cnpj cnpjEmpresa = new Cnpj(empresaDto.Cnpj);

        Empresa empresa = new Empresa(emailEmpresa, nomeEmpresa, telefoneEmpresa, cnpjEmpresa);
        await _unitOfWork.EmpresaRepositorio.Insere(empresa);

        //Chama o serviço de cadastro Setor
        await _setorServico.Cadastra(empresa);

        //Chama o serviço de cadastro Funil
        await _funilServico.Cadastra(empresa);

        await _usuarioServico.Cadastra(empresa,usuarioDto);
        
        await _unitOfWork.Commit();
    }
}