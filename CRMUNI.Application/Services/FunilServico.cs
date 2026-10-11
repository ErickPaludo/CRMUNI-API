using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.Funis;
using CRMUNI.Domain.ObjetosValor.Descricoes;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Servicos;

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
}