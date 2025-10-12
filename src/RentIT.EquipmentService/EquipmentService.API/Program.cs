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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// API Controllers
// -----------------------------
builder.Services.AddControllers();

// -----------------------------
// Custom Services
// -----------------------------
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddCoreLayer();

// -----------------------------
// Resilience
// -----------------------------
builder.Services.AddTransient<IPollyPolicies, PollyPolicies>();
builder.Services.AddTransient<IUsersMicroservicePolicies, UsersMicroservicePolicies>();
builder.Services.AddTransient<IRentalMicroservicePolicies, RentalMicroservicePolicies>();

// -----------------------------
// Open API and Swagger
// -----------------------------
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

// -----------------------------
// Http Clients
// -----------------------------
builder.Services.AddHttpContextAccessor();
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

// -----------------------------
// JWT Bearer Verification
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

// ---------------------------------
// Global Error Handling Middleware
// --------------------------------
app.UseGlobalExceptionHandlingMiddleware();

// -----------------------------
// HTTPS Support
// -----------------------------
app.UseHsts();
//app.UseHttpsRedirection();

await app.MigrateDatabaseAsync(builder.Services);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<EquipmentContext>();
    await AppDbSeeder.Seed(context);
}

// -----------------------------
// Use Swagger
// -----------------------------
app.UseSwagger();
app.UseSwaggerUI();


// -----------------------------
// Use Routing
// -----------------------------
app.UseRouting();


// -----------------------------
// Map Api Controllers
// -----------------------------
app.MapControllers();


// --------------------------------
// Authentication && Authorization
// --------------------------------
app.UseAuthentication();
app.UseAuthorization();

// -----------------------------
// Run App
// -----------------------------
app.Run();