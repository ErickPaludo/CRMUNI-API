using CRMUNI.Domaidd.Repositorios;

namespace CRMUNI.Domaidd.Servicos;

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
    
    Task Commit();
}