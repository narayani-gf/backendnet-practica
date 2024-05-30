using Microsoft.EntityFrameworkCore;
using backendnet.Data;

var builder = WebApplication.CreateBuilder(args);

// Agrega el soporte para MySQL
var connectionString = builder.Configuration.GetConnectionString("DataContext");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Agrega el soporte para CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3001", "http://localhost:8080")
                .AllowAnyHeader()
                .WithMethods("GET", "POST", "PUT", "DELETE");
        }
    );
});

// Agrega la funcionalidad de controladores
builder.Services.AddControllers();

// Agrega la documentación de la API
builder.Services.AddSwaggerGen();

// Construye la aplicación web
var app = builder.Build();

// Mostraremos la documentación solo en ambiente de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Utiliza rutas para los endpoints de los controladores
app.UseRouting();

// Usa Cors con la policy definida anteriormente
app.UseCors();

// Establece el uso de rutas sin especificar una por default
app.MapControllers();

app.Run();
