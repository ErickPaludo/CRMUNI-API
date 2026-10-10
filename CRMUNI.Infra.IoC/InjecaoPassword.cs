
using CRMUNI.Application.Interfaces.Seguranca;
using CRMUNI.Infra.Security.Servicos.Seguranca;
using CRMUNI.Infra.Seguranca.Configuracoes.Seguranca;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRMUNI.Infra.IoC
{
    public static class InjecaoPassword
    {
        public static void ConfigurarInjecaoPassword(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.Configure<SegurancaConfig>(configuration.GetSection("Auth"));
            services.AddScoped<ISegurancaServico, SegurancaServico>();
        }
    }
}
