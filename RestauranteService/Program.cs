using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestauranteService.Clients;
using RestauranteService.Configuration;
using RestauranteService.Data;
using RestauranteService.Producers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("RestaurantConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddSingleton<RabbitMqConnection>();
builder.Services.AddHostedService<RabbitMqInitializer>();

builder.Services.AddScoped<RestaurantProducer>();

builder.Services.AddHttpClient<IItemServiceHttpClient, ItemServiceHttpClient>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestauranteService", Version = "v1" });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var maxTries = 10;
var tries = 0;
bool connected = false;

while (!connected && tries < maxTries)
{
    try
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.CanConnect();
        context.Database.Migrate();
        connected = true;
    }
    catch
    {
        tries++;
        Console.WriteLine($"Tentativa {tries} de conexão com o banco...");
        Thread.Sleep(2000);
    }
}

app.Run();
