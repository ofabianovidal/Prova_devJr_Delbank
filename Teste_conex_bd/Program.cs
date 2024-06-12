using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Teste_conex_bd.Data; 
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
builder.Services.AddControllers();

// Configurar o DbContext para usar MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(connectionString));

// Adicionar serviço de cache em memória
builder.Services.AddMemoryCache();

// Adicionar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar o pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
