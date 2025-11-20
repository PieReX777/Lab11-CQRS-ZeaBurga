using Lab11_ZeaBurga.Application;
using Lab11_ZeaBurga.Infrastructure; // <-- 1. Importar el registro de Infrastructure

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssemblies(typeof(ApplicationAssemblyMarker).Assembly)
);

builder.Services.AddAutoMapper(cfg => { }, typeof(ApplicationAssemblyMarker).Assembly);

builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();