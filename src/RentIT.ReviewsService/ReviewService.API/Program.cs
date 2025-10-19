using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ReviewService.API.Handlers;
using ReviewService.Core.Domain.HttpClientContracts;
using ReviewService.Infrastructure.HttpClients;
using ReviewService.Infrastructure.Seeders;
using ReviewServices.API.Extensions;
using ReviewServices.API.Middleware;
using ReviewServices.Core;
using ReviewServices.Infrastructure;
using ReviewServices.Infrastructure.DbContexts;
using System.Security.Claims;
using System.Text;
var builder = WebApplication.CreateBuilder(args);


//Add Controllers
builder.Services.AddControllers();

//Add Core and Infra Layer
builder.Services.AddCoreLayer(builder.Configuration);
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddHttpContextAccessor();

//Add Http Client Auht Handlers
builder.Services.AddTransient<BearerTokenHandler>();

//Add Http Clients 
builder.Services.AddHttpClient<IUsersMicroserviceClient, UsersMicroserviceClient>(options =>
{
    options.BaseAddress = new Uri($"http://{builder.Configuration["USERS_MICROSERVICE_NAME"]}" +
        $":{builder.Configuration["USERS_MICROSERVICE_PORT"]}");
})
    .AddHttpMessageHandler<BearerTokenHandler>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

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

//Add Global Exception handling middleware
app.UseExceptionHandlingMiddleware();

//Migrate Database
await app.MigrateDatabaseAsync(builder.Services);

app.MapOpenApi();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //Seed Data
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ReviewsDbContext>();
    await ReviewDbSeeder.Seed(context);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
