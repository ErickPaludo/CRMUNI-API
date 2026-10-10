using HeyChefe.Application.Interfaces.Segurança;
using HeyChefe.Application.Services.Segurança;
using HeyChefe.Infra.Security.Configurações.Segurança;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeyChefe.Infra.IoC
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
