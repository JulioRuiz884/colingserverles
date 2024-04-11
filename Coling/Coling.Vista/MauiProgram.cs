using Coling.Vista.Servicios.Afiliados;
using Coling.Vista.Servicios.Curriculum;
using Microsoft.Extensions.Logging;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Coling.Shared;
using Coling.Vista.Modelos;
using Coling.Vista.Servicios.Afiliados.Contratos;
using Coling.Vista.Servicios.BolsaTrabajo.Contratos;
using Coling.Vista.Servicios.BolsaTrabajo;

namespace Coling.Vista
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            builder.Services.AddSweetAlert2();
            builder.Services.AddHttpClient();
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<IPersonaService, PersonaService>();
            builder.Services.AddScoped<IInstitucionService, InstitucionService>();
            builder.Services.AddScoped<IEstudioService, EstudioService>();
            builder.Services.AddScoped<IExperienciaLaboralsService, ExperienciaLaboralService>();
            builder.Services.AddScoped<IIdiomaService, IdiomaService>();
            builder.Services.AddScoped<ITipoSocialService, TipoSocialService>();
            builder.Services.AddScoped<IAfiliadoService, AfiliadoService>();
            builder.Services.AddScoped<ITelefonoService, TelefonoService>();
            builder.Services.AddScoped<IOfertaLaboralService, OfertaLaboralService>();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
