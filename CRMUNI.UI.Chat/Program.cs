using CRMUNI.Application.Interfaces;
using CRMUNI.Application.Services;
using CRMUNI.Infra.IoC;
using CRMUNI.UI.Chat.Atendimentos;
using CRMUNI.UI.Chat.Extensao;
using CRMUNI.UI.Chat.Hubs;
using Microsoft.AspNetCore.SignalR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Banco: validacao do JWT do funcionario (IAutenticacaoRepositorio), identificacao do contato e abertura do atendimento.
builder.Services.ConfigurarInjecaoInfraestrutura(builder.Configuration);
builder.Services.ConfigurarInjecaoAutenticaoJWT(builder.Configuration);
builder.Services.ConfigurarInjecaoBibliotecas();
builder.Host.UseSerilog();

builder.Services.AddAuthorization();
// Somente os servicos usados pelo hub (ConfigurarInjecaoServicos exigiria toda a cadeia de autenticacao da API).
builder.Services.AddScoped<IAtendimentoChatServico, AtendimentoChatServico>();
builder.Services.AddScoped<IMensagemServico, MensagemServico>();
builder.Services.AddSingleton<GerenciadorAtendimentos>();
builder.Services.AddSingleton<FiltroExcecaoHub>();
builder.Services.AddSignalR(options =>
{
    // Em desenvolvimento o cliente recebe a causa real de erros (ex.: falha ao instanciar o hub).
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
    options.AddFilter<FiltroExcecaoHub>();
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/hubs/chat");

app.Run();
