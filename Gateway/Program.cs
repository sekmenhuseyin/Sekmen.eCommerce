var builder = WebApplication.CreateBuilder(args);

builder.AddInternalAuthentication();
builder.Services
    .AddEndpointsApiExplorer()
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();
