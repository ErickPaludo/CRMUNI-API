
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRMUNI.Infra.IoC
{
    public static class InjectInfraestructure
    {
        public static void ConfigurarInjecaoInfraestrutura(this IServiceCollection services, IConfiguration configure)
        {
           // services.AddDbContext<AppDbContext>(op => op.UseSqlServer(configure.GetConnectionString("SqlServer"), b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))); //variavel b diz aonde gerar as migrations, pois o contexto esta em outro projeto
           // services.AddScoped<IUnitOfWork, UnitOfWork>();
          //  services.AddScoped<IAutenticacoesRepositorio, AutenticacoesRepositorio>();
        }
    }
}
