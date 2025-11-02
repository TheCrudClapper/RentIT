using ReviewService.API.Extensions;
using ReviewService.API.Handlers;
using ReviewService.Infrastructure.Seeders;
using ReviewServices.API.Extensions;
using ReviewServices.API.Middleware;
using ReviewServices.Core;
using ReviewServices.Infrastructure;
using ReviewServices.Infrastructure.DbContexts;


var builder = WebApplication.CreateBuilder(args);

#region Service Registration
// -----------------------------
// API Controllers
// -----------------------------
builder.Services.AddControllers();

// -----------------------------
// Custom Services && Handlers
// -----------------------------
builder.Services
    .AddCoreLayer(builder.Configuration)
    .AddInfrastructureLayer(builder.Configuration)
    .AddAuthHeaderHandler();

// -----------------------------
// Http Context Accessor
// -----------------------------
builder.Services.AddHttpContextAccessor();

// -----------------------------
// Http Clients
// -----------------------------
builder.Services.AddInfrastructureHttpClients(builder.Configuration);

// -----------------------------
// Open API && Swagger
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
app.UseHttpsRedirection();

// -----------------------------
// Open API Docs
// -----------------------------
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

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ReviewsDbContext>();
    await ReviewDbSeeder.Seed(context);
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