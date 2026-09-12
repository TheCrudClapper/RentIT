using EquipmentService.API.Extensions;
using EquipmentService.API.Handlers;
using EquipmentService.API.Middleware;
using EquipmentService.Core;
using EquipmentService.Core.Policies.Contracts;
using EquipmentService.Core.Policies.Implementations;
using EquipmentService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region Service Registration
// -----------------------------
// API Controllers
// -----------------------------
builder.Services.AddControllers();

// -----------------------------
// Custom Services & Handlers
// -----------------------------
builder.Services
    .AddInfrastructureLayer(builder.Configuration)
    .AddCoreLayer()
    .AddAuthHeaderHandler();

// -----------------------------
// Resilience Policies
// -----------------------------
builder.Services.AddTransient<IPollyPolicies, PollyPolicies>();
builder.Services.AddTransient<IUsersMicroservicePolicies, UsersMicroservicePolicies>();
builder.Services.AddTransient<IRentalMicroservicePolicies, RentalMicroservicePolicies>();

// -----------------------------
// Open API and Swagger
// -----------------------------
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -----------------------------
// Http Context Accessor
// -----------------------------
builder.Services.AddHttpContextAccessor();

// -----------------------------
// Http Clients
// -----------------------------
builder.Services.AddInfrastructureHttpClients(builder.Configuration);

// -----------------------------
// JWT Based Token Auth
// -----------------------------
builder.Services.AddJwtTokenAuth(builder.Configuration);
#endregion
#region Middleware-Pipeline
var app = builder.Build();

// ---------------------------------
// Global Error Handling Middleware
// ---------------------------------
app.UseGlobalExceptionHandlingMiddleware();

// -----------------------------
// HTTPS Support
// -----------------------------
app.UseHsts();
//app.UseHttpsRedirection();


// -----------------------------
// Open API Docs
// -----------------------------
app.MapOpenApi();

await app.MigrateDatabaseAsync(builder.Services);

if (app.Environment.IsDevelopment())
{
    // -----------------------------
    // Use Swagger
    // -----------------------------
    app.UseSwagger();
    app.UseSwaggerUI();

    // -----------------------------
    // Seed Db with dummy data
    // -----------------------------
    await app.SeedDatabase();
}

// -----------------------------
// Use Routing
// -----------------------------
app.UseRouting();

// --------------------------------
// Authentication && Authorization
// --------------------------------
app.UseAuthentication();
app.UseAuthorization();

// -----------------------------
// Map Api Controllers
// -----------------------------
app.MapControllers();

// -----------------------------
// Run App
// -----------------------------
app.Run();
#endregion