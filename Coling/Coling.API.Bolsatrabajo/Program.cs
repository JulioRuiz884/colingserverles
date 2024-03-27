using Coling.API.Bolsatrabajo.Contratos;
using Coling.API.Bolsatrabajo.Implementacion;
using Coling.BolsaTrabajo;
using Coling.Utilitarios.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<ISolicitudRepositorio, SolicitudRepositorio>();
    })
    .Build();

host.Run();
