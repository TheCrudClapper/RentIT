using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.API.Middleware;
using UserService.Core;
using UserService.Core.Domain.Entities;
using UserService.Infrastructure;
using UserService.Infrastructure.DbContexts;

var builder = WebApplication.CreateBuilder(args);

//Add API Controllers
builder.Services.AddControllers();
//Add DB context
builder.Services.AddDbContext<UsersDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDB"),
        x => x.MigrationsAssembly("UserService.Infrastructure"));
});

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


//Add user-defined services
builder.Services.AddInfrastructureLayer();
builder.Services.AddCoreLayer();

//Add OpenAPI support and Swagger
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Fluent Validations


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Global exception handling
app.UseGlobalExceptionHandlingMiddleware();

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
