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
    public class IdiomaFunction
    {
        private readonly ILogger<IdiomaFunction> _logger;
        private readonly IIdiomaLogic idiomaLogic;

        public IdiomaFunction(ILogger<IdiomaFunction> logger, IIdiomaLogic idiomaLogic)
        {
            _logger = logger;
            this.idiomaLogic = idiomaLogic;
        }

        [Function("ListarIdiomas")]
        [OpenApiOperation("ListarSolicitudespec", "ListarIdiomas", Description = "Sirve para listar todos los Idiomas")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Idioma>), Description = "Devuelve la lista de Idiomas")]
        public async Task<HttpResponseData> ListarIdiomas([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listaridiomas")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para insertar personas");
            try
            {
                var listaPersonas = idiomaLogic.ListarIdiomaTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaPersonas.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("InsertarIdioma")]
        [OpenApiOperation("Insertarspec", "InsertarIdioma", Description = "Sirve para insertar un Idioma")]
        [OpenApiRequestBody("application/json", typeof(Idioma), Description = "Idioma modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Idioma), Description = "Insertara un Idioma.")]
        public async Task<HttpResponseData> InsertarIdioma([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertaridioma")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para insertar idioma");
            try
            {
                var per = await req.ReadFromJsonAsync<Idioma>() ?? throw new Exception("Debe ingresar un idioma con todos sus datos");
                bool seGuardo = await idiomaLogic.InsertarIdioma(per);
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
        [Function("ModificarIdioma")]
        [OpenApiOperation("Modificarspec", "ModificarIdioma", Description = "Sirve para modificar un Idioma")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del Idioma a modificar")]
        [OpenApiRequestBody("application/json", typeof(Idioma), Description = "Idioma modelo con los campos actualizados")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la modificación fue exitosa")]
        public async Task<HttpResponseData> ModificarIdioma([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificaridioma/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para modificar idioma");
            try
            {
                var idioma = await req.ReadFromJsonAsync<Idioma>();
                if (idioma == null)
                {
                    throw new Exception("Debe ingresar un idioma con todos sus datos a modificar");
                }
                bool seModifico = await idiomaLogic.ModificarIdioma(idioma, id);

                if (seModifico)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    await respuesta.WriteAsJsonAsync("El idioma no fue encontrada para modificar");
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
        [Function("EliminarIdioma")]
        [OpenApiOperation("Eliminarspec", "EliminarIdioma", Description = "Sirve para eliminar un idioma por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del idioma a eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la eliminación fue exitosa")]
        public async Task<HttpResponseData> EliminarIdioma([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminaridioma/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para eliminar idioma");

            try
            {
                bool seElimino = await idiomaLogic.EliminarIdioma(id);
                if (seElimino)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    await respuesta.WriteAsJsonAsync("El idioma no fue encontrada para eliminar");
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
        [Function("ObtenerIdiomaId")]
        [OpenApiOperation("ObtenerByIdspec", "ObtenerIdiomaId", Description = "Sirve para obtener un idioma por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del idioma a obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Idioma), Description = "Devuelve el idioma encontrada")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(string), Description = "No se encontró ningun idioma con el ID proporcionado")]
        public async Task<HttpResponseData> ObtenerIdiomaId([HttpTrigger(AuthorizationLevel.Function, "get/{id}", Route = "obteneridioma/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para obtener idioma por ID");

            try
            {
                var persona = await idiomaLogic.ObtenerIdiomaById(id);
                if (persona != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(persona);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    await respuesta.WriteAsJsonAsync("El idioma no fue encontrada");
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
