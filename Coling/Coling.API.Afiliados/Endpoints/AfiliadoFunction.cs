using Coling.API.Afiliados.Contratos;
using Coling.API.Afiliados.DTO;
using Coling.API.Afiliados.Implementacion;
using Coling.Shared;
using Coling.Utilitarios.Attributes;
using Coling.Utilitarios.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Coling.API.Afiliados.Endpoints
{
    public class AfiliadoFunction
    {
        private readonly ILogger<AfiliadoFunction> _logger;
        private readonly IAfiliadoLogic afiliadoLogic;

        public AfiliadoFunction(ILogger<AfiliadoFunction> logger, IAfiliadoLogic afiliadoLogic)
        {
            _logger = logger;
            this.afiliadoLogic = afiliadoLogic;
        }

        [Function("ListarAfiliados")]
		[ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Secretaria)]
		[OpenApiOperation("Listarspec", "ListarAfiliados", Description = "Sirve para listar todas las Afiliados")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Afiliado>),
            Description = "Mostrara una lista de Afiliados")]
       
        public async Task<HttpResponseData> ListarAfiliados([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listarafiliados")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para Listar Idiomas");
            try
            {
                var listaAfiliado = afiliadoLogic.ListarAfiliadoTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaAfiliado.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarAfiliado")]
		[ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Secretaria)]
		[OpenApiOperation("Insertarspec", "InsertarAfiliado", Description = "Sirve para Insertar una Afiliado")]
        [OpenApiRequestBody("application/json", typeof(Afiliado), Description = "Afiliado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Afiliado), Description = "Mostrara la Afiliado Creada")]
        public async Task<HttpResponseData> InsertarAfiliado([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "insertarafiliado")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para Insertar afiliado");
            try
            {
                var af = await req.ReadFromJsonAsync<Afiliado>() ?? throw new Exception("Debe ingresar un idioma con todos sus datos");
                bool seGuardo = await afiliadoLogic.InsertarAfiliado(af);
                if (seGuardo)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);

            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ObtenerAfiliadoById")]
		[ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Secretaria)]
		[OpenApiOperation("Obtenerspec", "ObtenerAfiliadoById", Description = "Sirve para obtener un Afiliado")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Afiliado), Description = "Mostrara una Afiliado")]
        public async Task<HttpResponseData> ObtenerAfiliadoById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "obtenerAfiliadobyid/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation("Ejecutando Azure Function para Obtener a una Afiliado");
            try
            {
                var idi = afiliadoLogic.ObtenerAfiliadoById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(idi.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("ModificarAfiliado")]
		[ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Secretaria)]
		[OpenApiOperation("Modificarspec", "ModificarAfiliado", Description = "Sirve para Modificar un Afiliado")]
        [OpenApiRequestBody("application/json", typeof(Afiliado), Description = "Institucion modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Afiliado),
            Description = "Mostrara la Afiliado modificada")]
        public async Task<HttpResponseData> ModificarAfiliado([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarafiliado/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation("Ejecutando Azure Function para Modificar Afiliado");
            try
            {
                var af = await req.ReadFromJsonAsync<Afiliado>() ?? throw new Exception("Debe ingresar un Afiliado con todos sus datos");
                bool seModifico = await afiliadoLogic.ModificarAfiliado(af, id);
                if (seModifico)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);

            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("EliminarAfiliado")]
		[ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Secretaria)]
		[OpenApiOperation("Eliminarspec", "EliminarAfiliado", Description = "Sirve para Eliminar un Afiliado")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(int))]
        public async Task<HttpResponseData> EliminarAfiliado([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarafiliado/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation("Ejecutando Azure Function para Eliminar Afiliado");
            try
            {
                bool seElimino = await afiliadoLogic.EliminarAfiliado(id);
                if (seElimino)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);

            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
  
       
    }
}
