var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.UseRouting();
app.MapControllers();

await app.UseOcelot();
