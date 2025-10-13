using ReviewService.Core;
using ReviewService.Infrastructure;
using ReviewService.API.Middleware;
using ReviewService.API.Extensions;
var builder = WebApplication.CreateBuilder(args);

//Add Core and Infra Layer
builder.Services.AddCoreLayer(builder.Configuration);
builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var app = builder.Build();

//Add Global Exception handling middleware
app.UseExceptionHandlingMiddleware();

//Migrate Database
await app.MigrateDatabaseAsync(builder.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
