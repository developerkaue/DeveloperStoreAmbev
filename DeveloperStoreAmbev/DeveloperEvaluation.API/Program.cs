using DeveloperEvaluation.Infrastructure.Repositories;
using DeveloperEvaluation.Domain.Repositories;
using DeveloperEvaluation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DeveloperEvaluation.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do Banco de Dados PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Inje��o de depend�ncias para o reposit�rio
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

// Configura��o do MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplicar migra��es automaticamente
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
