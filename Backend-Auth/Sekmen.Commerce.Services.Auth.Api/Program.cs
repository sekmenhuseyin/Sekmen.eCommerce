var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddControllers();
builder.AddInternalDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .UseHttpsRedirection()
    .UseCors(app.Environment.EnvironmentName.ToLower())
    .UseAuthentication()
    .UseAuthorization()
    .UseHeaders();
app.MapControllers();
app.ApplyMigrations();

app.Run();