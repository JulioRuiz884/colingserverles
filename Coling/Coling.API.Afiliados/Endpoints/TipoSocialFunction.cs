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
    public class TipoSocialFunction
    {
        private readonly ILogger<TipoSocialFunction> _logger;
        private readonly ITipoSocialLogic tipoSocialLogic;

        public TipoSocialFunction(ILogger<TipoSocialFunction> logger, ITipoSocialLogic tipoSocialLogic)
        {
            _logger = logger;
            this.tipoSocialLogic = tipoSocialLogic;
        }

        [Function("ListarTipoSocial")]
        [OpenApiOperation("ListarSolicitudespec", "ListarTipoSocial", Description = "Sirve para listar todos los tipo social")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<TipoSocial>), Description = "Devuelve la lista de tipo social")]
        public async Task<HttpResponseData> ListarTipoSocial([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listartiposocial")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para insertar tipo social");
            try
            {
                var listaTipoSocial = tipoSocialLogic.ListarTipoSocialTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaTipoSocial.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("InsertarTipoSocial")]
        [OpenApiOperation("Insertarspec", "InsertarTipoSocial", Description = "Sirve para insertar un tipo social")]
        [OpenApiRequestBody("application/json", typeof(TipoSocial), Description = "tipo social modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(TipoSocial), Description = "Insertara un tipo social.")]
        public async Task<HttpResponseData> InsertarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertartiposocial")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para insertar tipo social");
            try
            {
                var tiposoc = await req.ReadFromJsonAsync<TipoSocial>() ?? throw new Exception("Debe ingresar un tipo social con todos sus datos");
                bool seGuardo = await tipoSocialLogic.InsertarTipoSocial(tiposoc);
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
        [Function("ModificarTipoSocial")]
        [OpenApiOperation("Modificarspec", "ModificarTipoSocial", Description = "Sirve para modificar un Tipo Social")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del Tipo Social a modificar")]
        [OpenApiRequestBody("application/json", typeof(TipoSocial), Description = "Tipo Social modelo con los campos actualizados")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la modificación fue exitosa")]
        public async Task<HttpResponseData> ModificarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificartiposocial/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para modificar tipo social");
            try
            {
                var tiposo = await req.ReadFromJsonAsync<TipoSocial>();
                if (tiposo == null)
                {
                    throw new Exception("Debe ingresar un tipo social con todos sus datos a modificar");
                }
                bool seModifico = await tipoSocialLogic.ModificarTipoSocial(tiposo, id);

                if (seModifico)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    await respuesta.WriteAsJsonAsync("El tipo social no fue encontrada para modificar");
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
        [Function("EliminarTipoSocial")]
        [OpenApiOperation("Eliminarspec", "EliminarTipoSocial", Description = "Sirve para eliminar un Tipo Social por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del Tipo Social a eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la eliminación fue exitosa")]
        public async Task<HttpResponseData> EliminarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminartiposocial/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para eliminar tipo social");

            try
            {
                bool seElimino = await tipoSocialLogic.EliminarTipoSocial(id);
                if (seElimino)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    await respuesta.WriteAsJsonAsync("El tipo social no fue encontrada para eliminar");
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
        [Function("ObtenerTipoSocialId")]
        [OpenApiOperation("ObtenerByIdspec", "ObtenerTipoSocialId", Description = "Sirve para obtener un Tipo Social por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del Tipo Social a obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(TipoSocial), Description = "Devuelve el Tipo Social encontrada")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(string), Description = "No se encontró ningun Tipo Social con el ID proporcionado")]
        public async Task<HttpResponseData> ObtenerTipoSocialId([HttpTrigger(AuthorizationLevel.Function, "get/{id}", Route = "obtenertiposocial/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para obtener tipo social por ID");

            try
            {
                var tiposoc = await tipoSocialLogic.ObtenerTipoSocialById(id);
                if (tiposoc != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(tiposoc);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    await respuesta.WriteAsJsonAsync("El tipo social no fue encontrada");
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
