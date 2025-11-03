using Microsoft.AspNetCore.Identity;
using UserService.API.Extensions;
using UserService.API.Middleware;
using UserService.Core;
using UserService.Core.Domain.Entities;
using UserService.Infrastructure;
using UserService.Infrastructure.DbContexts;
using UserService.Infrastructure.Seeders;

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
    .AddCoreLayer();

// -----------------------------
// Identity Config
// ------------------------- ---
builder.Services.ConfigureIdentity();

// -----------------------------
// Open API and Swagger
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

app.MapOpenApi();

// ---------------------------------
// Migrations
// ---------------------------------
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

app.Run();
#endregion