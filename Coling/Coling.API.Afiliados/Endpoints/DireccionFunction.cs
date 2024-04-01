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
    public class DireccionFunction
    {
        private readonly ILogger<DireccionFunction> _logger;
        private readonly IDireccionLogic direccionLogic;

        public DireccionFunction(ILogger<DireccionFunction> logger, IDireccionLogic direccionLogic)
        {
            _logger = logger;
            this.direccionLogic = direccionLogic;
        }

        [Function("ListarDireccion")]
        [OpenApiOperation("ListarSolicitudespec", "ListarDireccion", Description = "Sirve para listar todas las Direcciones")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Direccion>), Description = "Devuelve la lista de Direcciones")]
        public async Task<HttpResponseData> ListarDireccion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listardireccion")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para insertar direccion");
            try
            {
                var listaDireccion = direccionLogic.ListarDireccionTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaDireccion.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("InsertarDireccion")]
        [OpenApiOperation("Insertarspec", "InsertarDireccion", Description = "Sirve para insertar una Direccion")]
        [OpenApiRequestBody("application/json", typeof(Direccion), Description = "Direccion modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Direccion), Description = "Insertara una Direccion.")]
        public async Task<HttpResponseData> InsertarDireccion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertardireccion")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para insertar direccion");
            try
            {
                var dir = await req.ReadFromJsonAsync<Direccion>() ?? throw new Exception("Debe ingresar una direccion");
                bool seGuardo = await direccionLogic.InsertarDireccion(dir);
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
        [Function("ModificarDireccion")]
        [OpenApiOperation("Modificarspec", "ModificarDireccion", Description = "Sirve para modificar una Direccion")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la Direccion a modificar")]
        [OpenApiRequestBody("application/json", typeof(Direccion), Description = "Direccion modelo con los campos actualizados")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la modificación fue exitosa")]
        public async Task<HttpResponseData> ModificarDireccion([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificardireccion/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para modificar direccion");
            try
            {
                var direccion = await req.ReadFromJsonAsync<Direccion>();
                if (direccion == null)
                {
                    throw new Exception("Debe ingresar una direccion con todos sus datos a modificar");
                }
                bool seModifico = await direccionLogic.ModificarDireccion(direccion, id);

                if (seModifico)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    await respuesta.WriteAsJsonAsync("La direccion no fue encontrada para modificar");
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
        [Function("EliminarDireccion")]
        [OpenApiOperation("Eliminarspec", "EliminarDireccion", Description = "Sirve para eliminar una Direccion por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la Direccion a eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la eliminación fue exitosa")]
        public async Task<HttpResponseData> EliminarDireccion([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminardireccion/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para eliminar direccion");

            try
            {
                bool seElimino = await direccionLogic.EliminarDireccion(id);
                if (seElimino)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    await respuesta.WriteAsJsonAsync("La direccion no fue encontrada para eliminar");
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
        [Function("ObtenerDireccionId")]
        [OpenApiOperation("ObtenerByIdspec", "ObtenerDireccionId", Description = "Sirve para obtener una Direccion por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la Direccion a obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Direccion), Description = "Devuelve la Direccion encontrada")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(string), Description = "No se encontró ninguna Direccion con el ID proporcionado")]
        public async Task<HttpResponseData> ObtenerDireccionId([HttpTrigger(AuthorizationLevel.Function, "get/{id}", Route = "obtenerdireccion/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para obtener direccion por ID");

            try
            {
                var direccion = await direccionLogic.ObtenerDireccionById(id);
                if (direccion != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(direccion);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    await respuesta.WriteAsJsonAsync("La direccion no fue encontrada");
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
