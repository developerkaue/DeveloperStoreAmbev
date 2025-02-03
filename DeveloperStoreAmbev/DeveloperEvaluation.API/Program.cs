using DeveloperEvaluation.Infrastructure.Repositories;
using DeveloperEvaluation.Domain.Repositories;
using DeveloperEvaluation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DeveloperEvaluation.Application.Mappings;
using MediatR;
using System.Reflection;
using DeveloperEvaluation.Application.Features.Sales.Commands;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco de Dados PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Injeção de dependências para o repositório
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

// 🔹 Configuração correta do MediatR (REMOVIDA LINHA DUPLICADA)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(CreateSaleCommand).Assembly));

// AutoMapper para mapeamento de DTOs
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
