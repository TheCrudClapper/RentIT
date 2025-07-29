using Microsoft.EntityFrameworkCore;
using RentIT.Infrastructure.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
        x => x.MigrationsAssembly("RentIT.Infrastructure"));
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Enables swagger to read the endpoints of application
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "RentItApi.xml"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHsts();
app.UseHttpsRedirection();

//Enables endpoint for swagger.json file (openAPI specification)
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
