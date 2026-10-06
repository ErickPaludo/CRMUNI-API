using Microsoft.Extensions.DependencyInjection;

namespace CRMUNI.Infra.IoC

{
    public static class InjecaoServicos
    {
        public static void ConfigurarInjecaoServicos(this IServiceCollection services)
        {
            // 2. Contexto de Contas de Usuários (Vínculos)
            //services.AddScoped<IRequestHandler<AtualizarContaUsuarioCommand, Resultado<RetornaCadastroContasUsuariosDTO>>, AtualizarContaUsuarioHandler>();

            //// 3. Contexto de Usuários e Autenticação
            //services.AddScoped<IRequestHandler<CadastraUsuarioCommand, Resultado<string>>, CadastraUsuarioHandler>();
            //services.AddScoped<IRequestHandler<AutenticacaoCommand, Resultado<RetornaTokenDTO>>, AutenticacaoHandler>();
            //services.AddScoped<IRequestHandler<RetornaUsuarioPorIdQuery, Resultado<RetornaUsuarioDTO>>, RetornaUsuarioHandler>();
            //services.AddScoped<IValidaPermissao, ValidaPermissao>();

        }
    }
}
