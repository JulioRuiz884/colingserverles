using Azure;
using Coling.API.Bolsatrabajo.Contratos;
using Coling.API.Bolsatrabajo.Modelo;
using Coling.BolsaTrabajo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Coling.API.Bolsatrabajo.Endpoints
{
    public class SolicitudFunction
    {
        private readonly ILogger<SolicitudFunction> _logger;
        private readonly ISolicitudRepositorio repos;

        public SolicitudFunction(ILogger<SolicitudFunction> logger, ISolicitudRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;

        }

        [Function("EliminarSolicitud")]
        [OpenApiOperation("Eliminarspec", "EliminarSolicitud", Description = "Sirve para eliminar una Solicitud por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la Solicitud a eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la eliminación fue exitosa")]
        public async Task<HttpResponseData> EliminarSolicitud([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "solicitudes/{id}")] HttpRequestData req, string id)
        {
            try
            {
                bool success = await repos.EliminarSolicitud(id);
                if (success)
                {
                    var response = req.CreateResponse(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    var response = req.CreateResponse(HttpStatusCode.NotFound);
                    return response;
                }
            }
            catch (Exception)
            {
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [Function("InsertarSolicitud")]
        [OpenApiOperation("Insertarspec", "InsertarSolicitud", Description = "Sirve para insertar una Solicitud")]
        [OpenApiRequestBody("application/json", typeof(Solicitud), Description = "Solicitud modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Solicitud), Description = "Insertara una Solicitud.")]
        public async Task<HttpResponseData> InsertarSolicitud([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            try
            {
                var registro = await req.ReadFromJsonAsync<Solicitud>() ?? throw new Exception("Debe ingresar una institucion con todos sus datos");
                bool sw = await repos.InsertarSolicitud(registro);
                if (sw)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }

            }
            catch (Exception)
            {

                var respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }

        [Function("ListarSolicitudes")]
        [OpenApiOperation("ListarSolicitudespec", "ListarSolicitudes", Description = "Sirve para listar todas las solicitudes")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Solicitud>), Description = "Devuelve la lista de solicitudes")]
        public async Task<HttpResponseData> ListarSolicitudes([HttpTrigger(AuthorizationLevel.Function, "get", Route = "solicitudes")] HttpRequestData req)
        {
            try
            {
                var solicitudes = await repos.ListarSolicitudes();
                var json = JsonConvert.SerializeObject(solicitudes);
                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(json);
                return response;
            }
            catch (Exception)
            {
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [Function("ModificarSolicitud")]
        [OpenApiOperation("Modificarspec", "ModificarSolicitud", Description = "Sirve para modificar una Solicitud")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la Solicitud a modificar")]
        [OpenApiRequestBody("application/json", typeof(Solicitud), Description = "Solicitud modelo con los campos actualizados")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la modificación fue exitosa")]
        public async Task<HttpResponseData> ModificarSolicitud([HttpTrigger(AuthorizationLevel.Function, "put", Route = "solicitudes/{id}")] HttpRequestData req, string id)
        {
            try
            {
                var solicitud = await req.ReadFromJsonAsync<Solicitud>() ?? throw new Exception("Debe proporcionar una solicitud válida para modificar");
                bool success = await repos.ModificarSolicitud(solicitud, id);
                if (success)
                {
                    var response = req.CreateResponse(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    var response = req.CreateResponse(HttpStatusCode.NotFound);
                    return response;
                }
            }
            catch (Exception)
            {
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [Function("ObtenerOfertaLaboralById")]
        [OpenApiOperation("ObtenerByIdspec", "ObtenerOfertaLaboralById", Description = "Sirve para obtener una Solicitud por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la Solicitud a obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Solicitud), Description = "Devuelve la solicitud encontrada")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(string), Description = "No se encontró ninguna solicitud con el ID proporcionado")]
        public async Task<HttpResponseData> ObtenerById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "solicitudes/{id}")] HttpRequestData req, string id)
        {
            try
            {
                var solicitud = await repos.ObtenerById(id);
                if (solicitud != null)
                {
                    var json = JsonConvert.SerializeObject(solicitud);
                    var response = req.CreateResponse(HttpStatusCode.OK);
                    await response.WriteAsJsonAsync(json);
                    return response;
                }
                else
                {
                    var response = req.CreateResponse(HttpStatusCode.NotFound);
                    await response.WriteStringAsync("No se encontró ninguna solicitud con el ID proporcionado");
                    return response;
                }
            }
            catch (Exception)
            {
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }
    }
}
