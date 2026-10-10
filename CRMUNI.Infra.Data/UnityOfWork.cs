using CRMUNI.DOMAIN.Repositorios;
using CRMUNI.DOMAIN.Servicos;
using CRMUNI.Infra.Data.Contexto;
using CRMUNI.Infra.Data.Repositorios;

namespace CRMUNI.Infra.Data;

public class UnityOfWork : IUnityOfWork
{
    private readonly AppDbContext _contexto;

    public UnityOfWork(AppDbContext contexto)
        => _contexto = contexto;


    private IEmpresaRepositorio? _empresaRepositorio;
    private ISetorRepositorio? _setorRepositorio;
    private IFunilRepositorio? _funilRepositorio;
    private IEtapaRepositorio? _etapaRepositorio;
    private IUsuarioRepositorio? _usuarioRepositorio;
    private IFuncionarioRepositorio? _funcionarioRepositorio;
    private IPlanoRepositorio? _planoRepositorio;
    private IAtendimentoRepositorio? _atendimentoRepositorio;
    private IContatoRepositorio? _contatoRepositorio;
    private IMensagemRepositorio? _mensagemRepositorio;

    public IEmpresaRepositorio EmpresaRepositorio => _empresaRepositorio ??= new EmpresaRepositorio(_contexto);
    public ISetorRepositorio SetorRepositorio => _setorRepositorio ??= new SetorRepositorio(_contexto);
    public IFunilRepositorio FunilRepositorio => _funilRepositorio ??= new FunilRepositorio(_contexto);
    public IEtapaRepositorio EtapaRepositorio => _etapaRepositorio ??= new EtapaRepositorio(_contexto);
    public IUsuarioRepositorio UsuarioRepositorio => _usuarioRepositorio ??= new UsuarioRepositorio(_contexto);

    public IFuncionarioRepositorio FuncionarioRepositorio =>
        _funcionarioRepositorio ??= new FuncionarioRepositorio(_contexto);

    public IPlanoRepositorio PlanoRepositorio => _planoRepositorio ??= new PlanoRepositorio(_contexto);

    public IAtendimentoRepositorio AtendimentoRepositorio =>
        _atendimentoRepositorio ??= new AtendimentoRepositorio(_contexto);

    public IContatoRepositorio ContatoRepositorio => _contatoRepositorio ??= new ContatoRepositorio(_contexto);
    public IMensagemRepositorio MensagemRepositorio => _mensagemRepositorio ??= new MensagemRepositorio(_contexto);

    public IUsuarioRepositorio UsuarioRepostorio
    {
        get { return _usuarioRepositorio = _usuarioRepositorio ?? new UsuarioRepositorio(_contexto); }
    }


    public async Task Commit()
        => await _contexto.SaveChangesAsync();
}