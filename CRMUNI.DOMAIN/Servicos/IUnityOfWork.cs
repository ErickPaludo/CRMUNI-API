using CRMUNI.DOMAIN.Repositorios;

namespace CRMUNI.DOMAIN.Servicos;

public interface IUnityOfWork
{
    IEmpresaRepositorio empresaRepositorio { get; }
    ISetorRepositorio setorRepositorio { get; }
    IFunilRepositorio funilRepositorio { get; }
    IEtapaRepositorio etapaRepositorio { get; }
    IUsuarioRepositorio usuarioRepositorio { get; }
    IFuncionarioRepositorio funcionarioRepositorio { get; }
    IPlanoRepositorio planoRepositorio { get; }
    IAtendimentoRepositorio atendimentoRepositorio { get; }
    IContatoRepositorio contatoRepositorio { get; }
    IMensagemRepositorio mensagemRepositorio { get; }
    
    Task Commit();
}