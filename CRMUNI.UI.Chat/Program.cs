using CRMUNI.Infra.IoC;
using CRMUNI.UI.Chat.Atendimentos;
using CRMUNI.UI.Chat.Extensao;
using CRMUNI.UI.Chat.Hubs;
using Microsoft.AspNetCore.SignalR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Banco usado apenas pela validacao do JWT do funcionario (IAutenticacaoRepositorio).
builder.Services.ConfigurarInjecaoInfraestrutura(builder.Configuration);
builder.Services.ConfigurarInjecaoAutenticaoJWT(builder.Configuration);
builder.Services.ConfigurarInjecaoBibliotecas();
builder.Host.UseSerilog();

builder.Services.AddAuthorization();
builder.Services.AddSingleton<GerenciadorAtendimentos>();
builder.Services.AddSingleton<FiltroExcecaoHub>();
builder.Services.AddSignalR(options =>
{
    options.AddFilter<FiltroExcecaoHub>();
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/hubs/chat");

app.Run();
