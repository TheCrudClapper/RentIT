using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using UserService.API.Extensions;
using UserService.API.Middleware;
using UserService.Core;
using UserService.Core.Domain.Entities;
using UserService.Infrastructure;
using UserService.Infrastructure.DbContexts;
using UserService.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

//Add API Controllers
builder.Services.AddControllers();

//Add user-defined services
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddCoreLayer();

//Add Identity with it's own stores
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
})
.AddEntityFrameworkStores<UsersDbContext>()
.AddDefaultTokenProviders()
.AddUserStore<UserStore<User, Role, UsersDbContext, Guid>>()
.AddRoleStore<RoleStore<Role, UsersDbContext, Guid>>();

//Add OpenAPI support and Swagger
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Fluent Validations


var app = builder.Build();

// Global exception handling
app.UseGlobalExceptionHandlingMiddleware();

//Automatic migrations for docker
await app.MigrateDatabaseAsync(builder.Services);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    using var scope = app.Services.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    var context = scope.ServiceProvider.GetRequiredService<UsersDbContext>();

    await AppDbSeeder.Seed(context, userManager, roleManager);
}

// Security middlewares
app.UseHsts();
//app.UseHttpsRedirection();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Routing
app.UseRouting();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
