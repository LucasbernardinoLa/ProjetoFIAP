using Microsoft.EntityFrameworkCore;
using ProjetoFIAP.Api.Configurations;
using ProjetoFIAP.Api.Infra.Data;
using ProjetoFIAP.Api.Infra.Extensions;
using ProjetoFIAP.Api.Infra.Middleware;

GenericConfigurations.ConfigureCultureInfo();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMyAuthentication(builder.Configuration);
builder.Services.AddMyAuthorization();
builder.Services.AddMySwaggerGen();

builder.Services.AddControllers();

builder.Services.AddMyInfra(builder.Configuration);

var app = builder.Build();

// Adicionar essa parte para executar o seeder
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await DatabaseSeeder.SeedData(context);
}

app.UseMyErrorHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
