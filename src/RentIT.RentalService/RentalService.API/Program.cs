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


var builder = WebApplication.CreateBuilder(args);

#region Service Registration
// -----------------------------
// Api Controllers
// -----------------------------
builder.Services.AddControllers();

// -----------------------------
// Custom Services & Handlers
// -----------------------------
builder.Services
    .AddCoreLayer()
    .AddInfrastructureLayer(builder.Configuration)
    .AddAuthHeaderHandler();

// -----------------------------
// Http Context Accessor
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
builder.Services.AddInfrastructureHttpClients(builder.Configuration);

// -----------------------------
// OpenAPI && Swagger
// -----------------------------
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// ---------------------------------
// Migrations
// ---------------------------------
await app.MigrateDatabaseAsync(builder.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // -----------------------------
    // Use Swagger
    // -----------------------------
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<RentalDbContext>();
    await AppDbSeeder.Seed(context);
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

app.Run();
#endregion