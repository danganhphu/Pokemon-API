using Microsoft.EntityFrameworkCore;
using PokemonApp;
using PokemonApp.Data;
using PokemonApp.Utils;

var builder = WebApplication.CreateBuilder(args);

EnviConfig.Config(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();

#region dependency injection

builder.Services.AddTransient<Seed>();

#endregion dependency injection

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region EntityFramework Core

builder.Services.AddDbContext<DataContext>(option =>
{
    option.UseSqlServer(EnviConfig.DevConnectionString);
});

#endregion EntityFramework Core

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    if (scopedFactory == null)
    {
        // Use logging to record the error
        // Handle the case where IServiceScopeFactory is not available
        Console.WriteLine("IServiceScopeFactory is not registered.");
        return;
    }
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        if (service == null)
        {
            // Handle the case where Seed service is not available
            Console.WriteLine("Seed service is not registered.");
            return;
        }

        service.SeedDataContext();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();