using Microsoft.EntityFrameworkCore;
using SistemaChamados.Api.Application.Interfaces;
using SistemaChamados.Api.Application.Services;
using SistemaChamados.Api.Infrastructure.Data;
using SistemaChamados.Api.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar banco de dados - Usar In-Memory para testes
var environment = builder.Environment.EnvironmentName;
if (environment == "Development")
{
    builder.Services.AddDbContext<SistemaChamadosDbContext>(options =>
        options.UseInMemoryDatabase("SistemaChamados"));
}
else
{
    builder.Services.AddDbContext<SistemaChamadosDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

// Injeção de dependência
builder.Services.AddScoped<IChamadoRepository, ChamadoRepository>();
builder.Services.AddScoped<IChamadoService, ChamadoService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
