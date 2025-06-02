using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using UserManagement.data_base_context;
using UserManagement.repo;
using UserManagement.services;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

builder.Services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(connectionString).EnableSensitiveDataLogging());

builder.Services.AddIdentity<IdentityUser,IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
})
        .AddEntityFrameworkStores<UserDbContext>()
        .AddDefaultTokenProviders();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Environment.GetEnvironmentVariable("Jwt_Issuer"),
            ValidAudience = Environment.GetEnvironmentVariable("Jwt_Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Jwt_SecretKey")
            ))
        };
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});




var app = builder.Build();
// Run migrations during startup
using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("Done");
    var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
