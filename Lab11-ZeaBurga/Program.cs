using Lab11_ZeaBurga.Application;
using Lab11_ZeaBurga.Infrastructure; // <-- 1. Importar el registro de Infrastructure

var builder = WebApplication.CreateBuilder(args);

// --- INICIO DE CONFIGURACIÓN DE SERVICIOS ---

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Registrar servicios de Application (MediatR y AutoMapper)
// Le decimos a MediatR y AutoMapper que escaneen el ensamblado de Application
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssemblies(typeof(ApplicationAssemblyMarker).Assembly)
);

builder.Services.AddAutoMapper(cfg => { }, typeof(ApplicationAssemblyMarker).Assembly);

// 3. Registrar servicios de Infrastructure (DbContext, UnitOfWork, Repos)
builder.Services.AddInfrastructureServices(builder.Configuration);


// --- FIN DE CONFIGURACIÓN DE SERVICIOS ---

var app = builder.Build();

// Configuración del pipeline de HTTP
if (app.Environment.IsDevelopment())
{
    // Usamos la UI de Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();