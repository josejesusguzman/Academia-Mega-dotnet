using PrimeraAPI.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ProductoService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
