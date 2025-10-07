using ReviewService.Core;
using ReviewService.Infrastructure;
using ReviewService.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add Core and Infra Layer
builder.Services.AddCoreLayer(builder.Configuration);
builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var app = builder.Build();

//Add Global Exception handling middleware
app.UseExceptionHandlingMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
