using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using RentIT.ApiGateway.Extensions;

#region Service Registration
var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Ocelot Config File
// -----------------------------
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// -----------------------------
// Services & Handlers
// -----------------------------
builder.Services
    .AddOcelot()
    .AddPolly();

// -----------------------------
// JWT Based Token Auth
// -----------------------------
builder.Services.AddJwtTokenAuth(builder.Configuration);

// -----------------------------
// CORS Rules && Policies
// -----------------------------
builder.Services.ConfigureCors();
#endregion
#region Middleware Pipeline
var app = builder.Build();

// --------------------------------
// Cors
// --------------------------------
app.UseCors("AllowAngular");

// --------------------------------
// Authentication && Authorization
// --------------------------------
app.UseAuthentication();
app.UseAuthorization();

// --------------------------------
// Using Ocelot
// --------------------------------
await app.UseOcelot();

app.Run();
#endregion