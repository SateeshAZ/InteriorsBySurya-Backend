using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Survey.Repository;
using Survey.Services;


// Cosmos DB connection
string cosmosDbConnectionString = Environment.GetEnvironmentVariable("CosmosDBConnectionString") ?? throw new InvalidOperationException("CosmosDBConnection is not set.");
// Cosmos DB Configuration
string databaseId = "InteriorsBySurya";
string containerId = "Surveys";
string partitionKeyPath = "/ClientId";
// Initialize CosmosClient
CosmosClient cosmosClient = new CosmosClient(cosmosDbConnectionString);
try
{
    // Ensure the database and container exist
    await InitializeCosmosDBAsync(cosmosClient);

    Console.WriteLine("Database and Container created or verified successfully.");
}
catch (CosmosException ex)
{
    Console.WriteLine($"Cosmos DB error: {ex.StatusCode} - {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"General error: {ex.Message}");
}

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
builder.Services.AddSingleton((s) =>
{
    return new CosmosClient(cosmosDbConnectionString);
});
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<ISurveyRepository, SurveyRepository>();

builder.Build().Run();

async Task InitializeCosmosDBAsync(CosmosClient cosmosClient)
{
    // Create the database
    Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
    Console.WriteLine($"Database '{database.Id}' created or already exists.");

    // Create the container
    Container container = await database.CreateContainerIfNotExistsAsync(
        id: containerId,
        partitionKeyPath: partitionKeyPath,
        throughput: 400 // Provisioned throughput in RU/s
    );
    Console.WriteLine($"Container '{container.Id}' created or already exists.");
}