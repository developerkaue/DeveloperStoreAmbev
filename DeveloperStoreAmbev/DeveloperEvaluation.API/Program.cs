using DeveloperEvaluation.Infrastructure.Repositories;
using DeveloperEvaluation.Domain.Repositories;
using DeveloperEvaluation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DeveloperEvaluation.Application.Mappings;
using MediatR;
using System.Reflection;
using DeveloperEvaluation.Application.Features.Sales.Commands;
using DeveloperEvaluation.Application.Features.Sales.Events;
using DeveloperEvaluation.Application.Handlers;


var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco de Dados PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configuração do MediatR para processar eventos
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SaleCreatedEventHandler).Assembly));

// Injeção de dependências para o repositório
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

// Configuração correta do MediatR 
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(CreateSaleCommand).Assembly));

// AutoMapper para mapeamento de DTOs
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();


var app = builder.Build();

// Aplicar migrações automaticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
