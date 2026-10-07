using CRMUNI.Infra.Data.Contexto;
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
            
            // services.AddScoped<IUnitOfWork, UnitOfWork>();
            //  services.AddScoped<IAutenticacoesRepositorio, AutenticacoesRepositorio>();
        }
    }
}