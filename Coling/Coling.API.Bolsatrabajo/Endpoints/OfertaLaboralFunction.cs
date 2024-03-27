using Coling.API.Bolsatrabajo.Contratos;
using Coling.API.Bolsatrabajo.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Net;

namespace Coling.API.Bolsatrabajo.Endpoints
{
    public class OfertaLaboralFunction
    {
        private readonly ILogger<OfertaLaboralFunction> _logger;
        private readonly IOfertaLaboralRepositorio repos;

        public OfertaLaboralFunction(ILogger<OfertaLaboralFunction> logger, IOfertaLaboralRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("EliminarOfertaLaboral")]
        [OpenApiOperation("Eliminarspec", "EliminarOfertaLaboral", Description = "Sirve para eliminar una Oferta Laboral por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la Oferta Laboral a eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la eliminación fue exitosa")]
        public async Task<HttpResponseData> EliminarOfertaLaboral([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "ofertaLaboral/{id}")] HttpRequestData req, string id)
        {
            try
            {
                bool success = await repos.EliminarOfertaLaboral(id);
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

        [Function("InsertarOfertaLaboral")]
        [OpenApiOperation("Insertarspec", "InsertarOfertaLaboral", Description = "Sirve para insertar una OfertaLaboral")]
        [OpenApiRequestBody("application/json", typeof(OfertaLaboral), Description = "OfertaLaboral modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Solicitud), Description = "Insertara una OfertaLaboral.")]
        public async Task<HttpResponseData> InsertarOfertaLaboral([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            try
            {
                var registro = await req.ReadFromJsonAsync<OfertaLaboral>() ?? throw new Exception("Debe ingresar una OfertaLaboral con todos sus datos");
                bool sw = await repos.InsertarOfertaLaboral(registro);
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

        [Function("ListarOfertaLaboral")]
        [OpenApiOperation("ListarSolicitudespec", "ListarOfertaLaboral", Description = "Sirve para listar todas las OfertaLaborales")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OfertaLaboral>), Description = "Devuelve la lista de OfertaLaborales")]
        public async Task<HttpResponseData> ListarOfertaLaboral([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ofertaLaboral")] HttpRequestData req)
        {
            try
            {
                var solicitudes = await repos.ListarOfertaLaboral();
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

        [Function("ModificarOfertaLaboral")]
        [OpenApiOperation("Modificarspec", "ModificarOfertaLaboral", Description = "Sirve para modificar una OfertaLaboral")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la OfertaLaboral a modificar")]
        [OpenApiRequestBody("application/json", typeof(OfertaLaboral), Description = "OfertaLaboral modelo con los campos actualizados")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la modificación fue exitosa")]
        public async Task<HttpResponseData> ModificarOfertaLaboral([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ofertaLaboral/{id}")] HttpRequestData req, string id)
        {
            try
            {
                var solicitud = await req.ReadFromJsonAsync<OfertaLaboral>() ?? throw new Exception("Debe proporcionar una OfertaLaboral válida para modificar");
                bool success = await repos.ModificarOfertaLaboral(solicitud, id);
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

        [Function("ObtenerById")]
        [OpenApiOperation("ObtenerByIdspec", "ObtenerById", Description = "Sirve para obtener una OfertaLaboral por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la OfertaLaboral a obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Solicitud), Description = "Devuelve la OfertaLaboral encontrada")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(string), Description = "No se encontró ninguna OfertaLaboral con el ID proporcionado")]
        public async Task<HttpResponseData> ObtenerById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ofertaLaboral/{id}")] HttpRequestData req, string id)
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
                    await response.WriteStringAsync("No se encontró ninguna OfertaLaboral con el ID proporcionado");
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
