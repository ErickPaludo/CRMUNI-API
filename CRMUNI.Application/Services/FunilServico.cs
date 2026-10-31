using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.DTOs.Filtros;
using CRMUNI.Application.Excecoes;
using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Entidades.Funis;
using CRMUNI.Domain.Entidades.Usuarios;
using CRMUNI.Domain.ObjetosValor.Descricoes;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Servicos;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Application.Services;

public class FunilServico : IFunilServico
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IEtapaServico _etapaServico;

    public FunilServico(IUnityOfWork unitOfWork, IEtapaServico etapaServico)
    {
        _unitOfWork = unitOfWork;
        _etapaServico = etapaServico;
    }

    public async Task Cadastra(Empresa empresa)
    {
        Funil[] funils = new Funil[3];

        #region Funil Curioso

        NomeFunil nomeFunilCurioso = new NomeFunil("Curioso");
        DescricaoFunil descricaoFunilCurioso = new DescricaoFunil("Dedicado a clientes com duvidas sobre valores");
        Funil curioso = new Funil(empresa, EFunil.Curioso, nomeFunilCurioso, descricaoFunilCurioso);
        await _unitOfWork.FunilRepositorio.Insere(curioso);
        funils[0] = curioso;

        #endregion

        #region Funil Potencial Cliente

        NomeFunil nomeFunilPotencialCliente = new NomeFunil("Potencial Cliente");
        DescricaoFunil descricaoFunilPotencialCliente =
            new DescricaoFunil("Dedicado a clientes com duvidas sobre valores");
        Funil potencialCliente = new Funil(empresa, EFunil.PotencialCliente, nomeFunilPotencialCliente,
            descricaoFunilPotencialCliente);
        await _unitOfWork.FunilRepositorio.Insere(potencialCliente);
        funils[1] = potencialCliente;

        #endregion

        #region Funil Potencial Cliente

        NomeFunil nomeFunilVendido = new NomeFunil("Vendido");
        DescricaoFunil descricaoFunilVendido =
            new DescricaoFunil("Dedicado a clientes com duvidas sobre valores");
        Funil vendido = new Funil(empresa, EFunil.Vendido, nomeFunilVendido,
            descricaoFunilVendido);
        await _unitOfWork.FunilRepositorio.Insere(vendido);
        funils[2] = vendido;

        #endregion
        
        //Chama serviço cadastro de funils
        await _etapaServico.Cadastra(funils);
    }

    public async Task<FunisDto> Retorno(string idUsuario, FunilFiltro filtros)
    {
        bool isUsuario = true;
        Funcionario? funcionario = null;

        Usuario? usuario =
            await _unitOfWork.UsuarioRepositorio.BuscarObjetoUnico(x => x.Id == new Guid(idUsuario));

        if (usuario is null)
            isUsuario = false;

        if (!isUsuario)
        {
            funcionario =
                await _unitOfWork.FuncionarioRepositorio.BuscarObjetoUnico(x => x.Id.ToString() == idUsuario);

            if (funcionario is null)
                throw new NaoEncontradoExecao("Usuário solicitante não encontrado.");
        }

        Empresa empresa = isUsuario ? usuario!.Empresa : funcionario!.Empresa;

        IQueryable<Funil> query = _unitOfWork.FunilRepositorio.Retorno();

        query = query.Where(x => x.Empresa.Id.Equals(empresa.Id));

        if (filtros.Id.HasValue)
            query = query.Where(x => x.Id == filtros.Id.Value);

        if (filtros.Tipo.HasValue)
            query = query.Where(x => x.Tipo == filtros.Tipo.Value);

        if (filtros.Nome is not null)
            query = query.Where(x => x.Nome.Primeiro.Contains(filtros.Nome));

        List<Funil> funils = await query.ToListAsync();

        List<FunilDto> funilDtos = funils
            .Select(MapeamentoDto)
            .ToList();

        return new FunisDto(funilDtos);
    }

    private static FunilDto MapeamentoDto(Funil funil)
        => new FunilDto(funil.Id, funil.Empresa.Id, funil.Tipo, funil.Nome.Completo, funil.Descricao.Texto);
}