using Coling.API.Afiliados.Contratos;
using Coling.Shared;
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
    public class AfiliadoIdiomaFunction
    {
        private readonly ILogger<AfiliadoIdiomaFunction> _logger;
        private readonly IAfiliadoIdiomaLogic afiliadoIdiomaLogic;

        public AfiliadoIdiomaFunction(ILogger<AfiliadoIdiomaFunction> logger, IAfiliadoIdiomaLogic afiliadoIdiomaLogic)
        {
            _logger = logger;
            this.afiliadoIdiomaLogic = afiliadoIdiomaLogic;
        }

        [Function("ListarAfiliadoIdioma")]
        [OpenApiOperation("ListarSolicitudespec", "ListarAfiliadoIdioma", Description = "Sirve para listar todas las afiliado idiomas")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<AfiliadoIdioma>), Description = "Devuelve la lista de afiliados idiomas")]
        public async Task<HttpResponseData> ListarAfiliadoIdioma([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarafiliadoIdioma")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para insertar afiliadoIdioma");
            try
            {
                var listaafiliadoIdioma = afiliadoIdiomaLogic.ListarAfiliadoIdiomaTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaafiliadoIdioma.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("InsertarAfiliadoIdioma")]
        [OpenApiOperation("Insertarspec", "InsertarAfiliadoIdioma", Description = "Sirve para insertar un AfiliadoIdioma")]
        [OpenApiRequestBody("application/json", typeof(AfiliadoIdioma), Description = "AfiliadoIdioma modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AfiliadoIdioma), Description = "Insertara un AfiliadoIdioma.")]
        public async Task<HttpResponseData> InsertarAfiliadoIdioma([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarafiliadoIdioma")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para insertar afiliadoIdioma");
            try
            {
                var dir = await req.ReadFromJsonAsync<AfiliadoIdioma>() ?? throw new Exception("Debe ingresar un afiliadoIdioma");
                bool seGuardo = await afiliadoIdiomaLogic.InsertarAfiliadoIdioma(dir);
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
        [Function("ModificarAfiliadoIdioma")]
        [OpenApiOperation("Modificarspec", "ModificarAfiliadoIdioma", Description = "Sirve para modificar un AfiliadoIdioma")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del AfiliadoIdioma a modificar")]
        [OpenApiRequestBody("application/json", typeof(AfiliadoIdioma), Description = "AfiliadoIdioma modelo con los campos actualizados")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la modificación fue exitosa")]
        public async Task<HttpResponseData> ModificarAfiliadoIdioma([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarafiliadoIdioma/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para modificar afiliadoIdioma");
            try
            {
                var afiliadoIdioma = await req.ReadFromJsonAsync<AfiliadoIdioma>();
                if (afiliadoIdioma == null)
                {
                    throw new Exception("Debe ingresar un datos afiliadoIdioma a modificar");
                }
                bool seModifico = await afiliadoIdiomaLogic.ModificarAfiliadoIdioma(afiliadoIdioma, id);

                if (seModifico)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    await respuesta.WriteAsJsonAsync("El afiliadoIdioma no fue encontrado para modificar");
                    return respuesta;
                }
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }
        }
        [Function("EliminarAfiliadoIdioma")]
        [OpenApiOperation("Eliminarspec", "EliminarAfiliadoIdioma", Description = "Sirve para eliminar un AfiliadoIdioma por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del AfiliadoIdioma a eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la eliminación fue exitosa")]
        public async Task<HttpResponseData> EliminarAfiliadoIdioma([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarafiliadoIdioma/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para eliminar afiliadoIdioma");

            try
            {
                bool seElimino = await afiliadoIdiomaLogic.EliminarAfiliadoIdioma(id);
                if (seElimino)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    await respuesta.WriteAsJsonAsync("El afiliadoIdioma no fue encontrada para eliminar");
                    return respuesta;
                }
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }
        }
        [Function("ObtenerAfiliadoIdiomaId")]
        [OpenApiOperation("ObtenerByIdspec", "ObtenerAfiliadoIdiomaId", Description = "Sirve para obtener un AfiliadoIdioma por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del AfiliadoIdioma a obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AfiliadoIdioma), Description = "Devuelve la AfiliadoIdioma encontrada")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(string), Description = "No se encontró ningun AfiliadoIdioma con el ID proporcionado")]
        public async Task<HttpResponseData> ObtenerAfiliadoIdiomaId([HttpTrigger(AuthorizationLevel.Function, "get/{id}", Route = "obtenerafiliadoIdioma/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para obtener afiliadoIdioma por ID");

            try
            {
                var telefono = await afiliadoIdiomaLogic.ObtenerAfiliadoIdiomaById(id);
                if (telefono != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(telefono);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    await respuesta.WriteAsJsonAsync("El afiliadoIdioma no fue encontrado");
                    return respuesta;
                }
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
