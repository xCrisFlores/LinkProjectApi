using API.Models;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registrar los controladores personalizados
builder.Services.AddControllers();

// Configurar el DbContext con la cadena de conexión definida
builder.Services.AddDbContext<LinkProjectBddContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar los servicios que has creado
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<StudentsService>();
builder.Services.AddScoped<AdvisersService>();
builder.Services.AddScoped<ScheduleServices>();
builder.Services.AddScoped<SkillsService>();

// Configurar Swagger/OpenAPI para documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline para entornos de desarrollo (Swagger UI)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinkProject API V1");
        c.RoutePrefix = string.Empty; // Hace que Swagger sea la página de inicio
    });
}

// Redireccionamiento HTTPS
app.UseHttpsRedirection();

// Habilitar el uso de controladores programados
app.UseRouting();

// Asegurarse de que se utilice la autorización si se implementa en tu API
app.UseAuthorization();

// Mapear los controladores a las rutas correctas
app.MapControllers();

app.Run();
