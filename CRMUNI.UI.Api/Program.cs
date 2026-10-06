using CRMUNI.Infra.IoC;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.ConfigurarInjecaoSwagger(builder.Configuration);
//builder.Services.ConfigurarInjecaoPassword(builder.Configuration);
builder.Services.ConfigurarInjecaoAutenticaoJWT(builder.Configuration);
builder.Services.ConfigurarInjecaoInfraestrutura(builder.Configuration);
builder.Services.ConfigurarInjecaoServicos();
builder.Services.ConfigurarInjecaoBibliotecas();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Host.UseSerilog();

var app = builder.Build();
// app.UseExceptionHandler();
// builder.Services.AddProblemDetails();

app.UseSwagger();
app.UseSwaggerUI();
app.MapOpenApi();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}