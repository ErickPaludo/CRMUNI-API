using CRMUNI.Application.Interfaces;
using CRMUNI.Application.Interfaces.Autenticacao;
using CRMUNI.Application.Interfaces.Seguranca;
using CRMUNI.Application.Services;
using CRMUNI.Infra.Security.Servicos.Seguranca;
using Microsoft.Extensions.DependencyInjection;

namespace CRMUNI.Infra.IoC

{
    public static class InjecaoServicos
    {
        public static void ConfigurarInjecaoServicos(this IServiceCollection services)
        {
            services.AddScoped<IEmpresaServico, EmpresaServico>();
            services.AddScoped<IUsuarioServico, UsuarioServico>();
            services.AddScoped<IFuncionarioSevico, FuncionarioServico>();
            services.AddScoped<ISetorServico, SetorServico>();
            services.AddScoped<IFunilServico, FunilServico>();
            services.AddScoped<IEtapaServico, EtapaServico>();
            services.AddScoped<IAppAutenticacaoServico, AppAutenticacaoServico>();
            services.AddScoped<ISegurancaServico, SegurancaServico>();
            services.AddScoped<ITemporarioServico, TemporarioServico>();

        }
    }
}
