using CRMUNI.Infra.IoC;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigurarInjecaoSwagger(builder.Configuration);
builder.Services.ConfigurarInjecaoPassword(builder.Configuration);
builder.Services.ConfigurarInjecaoAutenticaoJWT(builder.Configuration);
builder.Services.ConfigurarInjecaoInfraestrutura(builder.Configuration);
builder.Services.ConfigurarInjecaoServicos();
builder.Services.ConfigurarInjecaoBibliotecas();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Host.UseSerilog();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();