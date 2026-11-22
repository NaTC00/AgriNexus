using AgriNexus.Authorization.Infrastructure;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorizationInfrastructure(builder.Configuration);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
