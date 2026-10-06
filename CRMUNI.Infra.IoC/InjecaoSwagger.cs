using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

namespace CRMUNI.Infra.IoC
{
    public static class InjecaoSwagger
    {
        public static IServiceCollection ConfigurarInjecaoSwagger(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = configuration["SwaggerInfo:Title"], Version = configuration["SwaggerInfo:Version"] });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    //definir configuracoes
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] " +
                    "and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });

              
            });
            return services;
        }
    }
}
