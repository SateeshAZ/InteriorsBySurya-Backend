using Google.Protobuf.WellKnownTypes;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CompanyManagement.data_base_context;
using Microsoft.Extensions.Configuration;
using CompanyManagement.Services;
using CompanyManagement.Repository;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

builder.Services.AddDbContext<CompanyDbContext>(options =>
            options.UseSqlServer(connectionString).EnableSensitiveDataLogging());



builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();



var app = builder.Build();




using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();

    // Check for pending migrations
    var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
    if (pendingMigrations.Any())
    {
        await dbContext.Database.MigrateAsync(); // Apply migrations
    }
}


app.Run();

