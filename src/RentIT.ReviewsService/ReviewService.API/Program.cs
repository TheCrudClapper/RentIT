using ReviewServices.Core;
using ReviewServices.Infrastructure;
using ReviewServices.API.Middleware;
using ReviewServices.API.Extensions;
var builder = WebApplication.CreateBuilder(args);

//Add Core and Infra Layer
builder.Services.AddCoreLayer(builder.Configuration);
builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

//Add Global Exception handling middleware
app.UseExceptionHandlingMiddleware();

//Migrate Database
await app.MigrateDatabaseAsync(builder.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
