using CRMUNI.UI.WebSocket;

var builder = WebApplication.CreateBuilder(args);

// Add console logging (important for debugging)
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add Kestrel listening on port 8080
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

builder.Services.AddSignalR();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.MapHub<ChatHub>("/chatHub");

app.Run();
