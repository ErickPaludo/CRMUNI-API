using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CRMUNI.Infra.IoC;

public static class InjecaoAutenticacaoJWT
{
    public static IServiceCollection ConfigurarInjecaoAutenticaoJWT(this IServiceCollection services,
        IConfiguration configuration)
    {
        // services.AddScoped<IAutenticacaoServico, AutenticacaoServico>();
        //   services.Configure<AutenticaoConfig>(configuration.GetSection("TokenJWT"));
        var secretKey = configuration.GetValue<string>("TokenJWT:SecretKeyJWT");
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userId = context.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;
                        var sid = context.Principal.FindFirst("sid")?.Value;

                        // var repo = context.HttpContext.RequestServices
                        // //    .GetRequiredService<IAutenticacoesRepositorio>();
                        //
                        // var usuario = await repo.BuscarObjetoUnico(x => x.Usuario.Id.ToString() == userId);
                        //
                        // if (usuario == null || usuario.RefreshToken != sid || usuario.Revoke)
                        // {
                        //     context.Fail("Sessão inválida");
                        // }
                        // if (usuario!.ExpirationRefresh < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                        // {
                        //     context.Fail("Sessão expirada");
                        // }
                    }
                };
            });

        return services;
    }
}