using CRMUNI.Domain.Repositorios;
using CRMUNI.Domain.Servicos;
using CRMUNI.Infra.Data;
using CRMUNI.Infra.Data.Contexto;
using CRMUNI.Infra.Data.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRMUNI.Infra.IoC
{
    public static class InjectInfraestructure
    {
        public static void ConfigurarInjecaoInfraestrutura(this IServiceCollection services, IConfiguration configure)
        {
            services.AddDbContext<AppDbContext>(op
                => op.UseSqlServer(configure.GetConnectionString("SqlServer"),
                    b
                        => b.MigrationsAssembly(typeof(AppDbContext).Assembly
                            .FullName)));

            services.AddScoped<IUnityOfWork, UnityOfWork>();
            services.AddScoped<IAutenticacaoRepositorio, AutenticacaoRepositorio>();
        }
    }
}