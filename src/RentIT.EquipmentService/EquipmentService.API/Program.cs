using EquipmentService.API.Extensions;
using EquipmentService.API.Middleware;
using EquipmentService.Core;
using EquipmentService.Core.Domain.HtppClientContracts;
using EquipmentService.Core.Policies.Contracts;
using EquipmentService.Core.Policies.Implementations;
using EquipmentService.Infrastructure;
using EquipmentService.Infrastructure.DbContexts;
using EquipmentService.Infrastructure.HttpClients;
using EquipmentService.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add API Controllers
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Add user-defined services
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddCoreLayer();

//Add resilience policies
builder.Services.AddTransient<IPollyPolicies, PollyPolicies>();
builder.Services.AddTransient<IUsersMicroservicePolicies, UsersMicroservicePolicies>();
builder.Services.AddTransient<IRentalMicroservicePolicies, RentalMicroservicePolicies>();

//Add OpenAPI support and Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpClient<IUsersMicroserviceClient, UsersMicroserviceClient>(client =>
{
    client.BaseAddress = new Uri($"http://{builder.Configuration["USERS_MICROSERVICE_NAME"]}:{builder.Configuration["USERS_MICROSERVICE_PORT"]}");
})
.AddPolicyHandler((serviceProvider, request) =>
{
    var policies = serviceProvider.GetRequiredService<IUsersMicroservicePolicies>();
    return policies.GetCombinedPolicy();
});

builder.Services.AddHttpClient<IRentalMicroserviceClient, RentalMicroserviceClient>(client =>
{
    client.BaseAddress = new Uri($"http://{builder.Configuration["RENTAL_MICROSERVICE_NAME"]}:{builder.Configuration["RENTAL_MICROSERVICE_PORT"]}");
})
.AddPolicyHandler((serviceProvider, request) =>
{
    var policies = serviceProvider.GetRequiredService<IRentalMicroservicePolicies>();
    return policies.GetCombinedPolicy();
});

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
    var context = scope.ServiceProvider.GetRequiredService<EquipmentContext>();
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