using RentalService.API.Extensions;
using RentalService.API.Middleware;
using RentalService.Core;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.Policies.Contracts;
using RentalService.Core.Policies.Implementations;
using RentalService.Infrastructure;
using RentalService.Infrastructure.DbContexts;
using RentalService.Infrastructure.HttpClients;
using RentalService.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add API Controllers
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Add user-defined services
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddCoreLayer();

//Add custom resilience policies
builder.Services.AddTransient<IPollyPolicies, PollyPolicies>();
builder.Services.AddTransient<IUsersMicroservicePolicies, UsersMicroservicePolicies>();
builder.Services.AddTransient<IEquipmentMicroservicePolicies, EquipmentMicroservicePolicies>();

builder.Services.AddHttpClient<IUsersMicroserviceClient, UsersMicroserviceClient>(options =>
{
    options.BaseAddress = new Uri($"http://{builder.Configuration["USERS_MICROSERVICE_NAME"]}:" +
        $"{builder.Configuration["USERS_MICROSERVICE_PORT"]}");
})
    .AddPolicyHandler((serviceProvider, request) =>
    {
        var policies = serviceProvider.GetRequiredService<IUsersMicroservicePolicies>();
        return policies.GetCombinedPolicy();
    });

builder.Services.AddHttpClient<IEquipmentMicroserviceClient, EquipmentMicroserviceClient>(options =>
{
    options.BaseAddress = new Uri($"http://{builder.Configuration["EQUIPMENT_MICROSERVICE_NAME"]}:" +
        $"{builder.Configuration["EQUIPMENT_MICROSERVICE_PORT"]}");
})
    .AddPolicyHandler((serviceProvider, request) =>
    {
        var policies = serviceProvider.GetRequiredService<IEquipmentMicroservicePolicies>();
        return policies.GetCombinedPolicy();
    });

//Add OpenAPI support and Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

//Add global exception handling middleware
app.UseGlobalExceptionHandlingMiddleware();

//Https supports
app.UseHsts();
//app.UseHttpsRedirection();

await app.MigrateDatabaseAsync(builder.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<RentalDbContext>();
    await AppDbSeeder.Seed(context);
}

//Use Swagger
app.UseSwagger();
app.UseSwaggerUI();

//Use Routing
app.UseRouting();

//Mapping API controllers 
app.MapControllers();

//Authentication && Authorization
app.UseAuthentication();
app.UseAuthorization();

app.Run();