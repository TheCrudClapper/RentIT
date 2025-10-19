using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RentalService.API.Extensions;
using RentalService.API.Handlers;
using RentalService.API.Middleware;
using RentalService.Core;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.Policies.Contracts;
using RentalService.Core.Policies.Implementations;
using RentalService.Infrastructure;
using RentalService.Infrastructure.DbContexts;
using RentalService.Infrastructure.HttpClients;
using RentalService.Infrastructure.Seeders;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Api Controllers
// -----------------------------
builder.Services.AddControllers();

// --------------------------------------------
// Dependency Injection for user def. services
// --------------------------------------------
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddCoreLayer();
builder.Services.AddTransient<BearerTokenHandler>();

// -----------------------------
// Context Accessor
// -----------------------------
builder.Services.AddHttpContextAccessor();

// -----------------------------
// Resilience Policies
// -----------------------------
builder.Services.AddTransient<IPollyPolicies, PollyPolicies>();
builder.Services.AddTransient<IUsersMicroservicePolicies, UsersMicroservicePolicies>();
builder.Services.AddTransient<IEquipmentMicroservicePolicies, EquipmentMicroservicePolicies>();

// -----------------------------
// Http Clients
// -----------------------------
builder.Services.AddHttpClient<IUsersMicroserviceClient, UsersMicroserviceClient>(options =>
{
    options.BaseAddress = new Uri($"http://{builder.Configuration["USERS_MICROSERVICE_NAME"]}:" +
        $"{builder.Configuration["USERS_MICROSERVICE_PORT"]}");
})
    .AddHttpMessageHandler<BearerTokenHandler>()
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
    .AddHttpMessageHandler<BearerTokenHandler>()
    .AddPolicyHandler((serviceProvider, request) =>
    {
        var policies = serviceProvider.GetRequiredService<IEquipmentMicroservicePolicies>();
        return policies.GetCombinedPolicy();
    });

// -----------------------------
// OpenAPI && Swagger
// -----------------------------
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

// -----------------------------
// Token based auth 
// -----------------------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)),
        NameClaimType = ClaimTypes.Name,
        RoleClaimType = "Roles",
    };
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