using System.Reflection.Emit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configurar el servicio de conexión a la base de datos
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configurar la inyección de dependencias para los servicios y repositorios
builder.Services.AddScoped<CPCore.Interfaces.IEventoRepository>(sp => new CPInfraestructura.Repositorios.EventoRepository(connectionString));
builder.Services.AddScoped<CPAplicacion.Servicios.EventoService>();

// Configurar los controladores
builder.Services.AddControllers();

// Configurar Swagger para la documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
