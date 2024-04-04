using Coling.Repositorio.Contratos;
using Coling.Repositorio.Implementacion;
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
        services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
        //services.AddSingleton<JwtMiddleware>();
    })
    //.ConfigureFunctionsWebApplication(x=> 
    //{
    //    x.UseMiddleware<JwtMiddleware>();
    //})
    .Build();

host.Run();
