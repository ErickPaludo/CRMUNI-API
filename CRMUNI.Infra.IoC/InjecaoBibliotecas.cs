using Microsoft.Extensions.DependencyInjection;
using NetDevPack.SimpleMediator;
using Serilog;

namespace CRMUNI.Infra.IoC;

public static class InjecaoBibliotecas
{
    public static void ConfigurarInjecaoBibliotecas(this IServiceCollection services )
    {
        // services.AddSimpleMediator();
        // services.AddScoped<IMediator, Mediator>();

        var outputTemplate = "{Timestamp} [{Level}] {Message}{NewLine}{Exception}{NewLine}";
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
            .WriteTo.Console(outputTemplate: outputTemplate)
            .Enrich.FromLogContext()
            .CreateLogger();
    }
}