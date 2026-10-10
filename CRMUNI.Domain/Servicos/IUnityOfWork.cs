using CRMUNI.Domain.Repositorios;

namespace CRMUNI.Domain.Servicos;

public interface IUnityOfWork
{
    IEmpresaRepositorio EmpresaRepositorio { get; }
    ISetorRepositorio SetorRepositorio { get; }
    IFunilRepositorio FunilRepositorio { get; }
    IEtapaRepositorio EtapaRepositorio { get; }
    IUsuarioRepositorio UsuarioRepositorio { get; }
    IFuncionarioRepositorio FuncionarioRepositorio { get; }
    IPlanoRepositorio PlanoRepositorio { get; }
    IAtendimentoRepositorio AtendimentoRepositorio { get; }
    IContatoRepositorio ContatoRepositorio { get; }
    IMensagemRepositorio MensagemRepositorio { get; }
    IAutenticacaoRepositorio AutenticacaoRepositorio { get; }
    
    Task Commit();
}