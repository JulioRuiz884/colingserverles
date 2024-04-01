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
    public class PersonaTipoSocialFunction
    {
        private readonly ILogger<PersonaTipoSocialFunction> _logger;
        private readonly IPersonaTipoSocialLogic personaTipoSocialLogic;

        public PersonaTipoSocialFunction(ILogger<PersonaTipoSocialFunction> logger, IPersonaTipoSocialLogic personaTipoSocialLogic)
        {
            _logger = logger;
            this.personaTipoSocialLogic = personaTipoSocialLogic;
        }

        [Function("ListarPersonaTipoSocial")]
        [OpenApiOperation("ListarSolicitudespec", "ListarPersonaTipoSocial", Description = "Sirve para listar todas las PersonaTipoSocial")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<PersonaTipoSocial>), Description = "Devuelve la lista de PersonaTipoSocial")]
        public async Task<HttpResponseData> ListarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarPersonaTipoSocial")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para insertar PersonaTipoSocial");
            try
            {
                var listaPersonaTipoSocial = personaTipoSocialLogic.ListarPersonaTipoSocialTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaPersonaTipoSocial.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("InsertarPersonaTipoSocial")]
        [OpenApiOperation("Insertarspec", "InsertarPersonaTipoSocial", Description = "Sirve para insertar una PersonaTipoSocial")]
        [OpenApiRequestBody("application/json", typeof(PersonaTipoSocial), Description = "PersonaTipoSocial modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PersonaTipoSocial), Description = "Insertara una PersonaTipoSocial.")]
        public async Task<HttpResponseData> InsertarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarPersonaTipoSocial")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para insertar PersonaTipoSocial");
            try
            {
                var dir = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar una PersonaTipoSocial");
                bool seGuardo = await personaTipoSocialLogic.InsertarPersonaTipoSocial(dir);
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
        [Function("ModificarPersonaTipoSocial")]
        [OpenApiOperation("Modificarspec", "ModificarPersonaTipoSocial", Description = "Sirve para modificar una PersonaTipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la PersonaTipoSocial a modificar")]
        [OpenApiRequestBody("application/json", typeof(PersonaTipoSocial), Description = "PersonaTipoSocial modelo con los campos actualizados")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la modificación fue exitosa")]
        public async Task<HttpResponseData> ModificarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarPersonaTipoSocial/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para modificar PersonaTipoSocial");
            try
            {
                var personaTipoSocial = await req.ReadFromJsonAsync<PersonaTipoSocial>();
                if (personaTipoSocial == null)
                {
                    throw new Exception("Debe ingresar una PersonaTipoSocial con todos sus datos a modificar");
                }
                bool seModifico = await personaTipoSocialLogic.ModificarPersonaTipoSocial(personaTipoSocial, id);

                if (seModifico)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    await respuesta.WriteAsJsonAsync("La PersonaTipoSocial no fue encontrada para modificar");
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
        [Function("EliminarPersonaTipoSocial")]
        [OpenApiOperation("Eliminarspec", "EliminarPersonaTipoSocial", Description = "Sirve para eliminar una PersonaTipoSocial por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la PersonaTipoSocial a eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la eliminación fue exitosa")]
        public async Task<HttpResponseData> EliminarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarPersonaTipoSocial/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para eliminar PersonaTipoSocial");

            try
            {
                bool seElimino = await personaTipoSocialLogic.EliminarPersonaTipoSocial(id);
                if (seElimino)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    await respuesta.WriteAsJsonAsync("La PersonaTipoSocial no fue encontrada para eliminar");
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
        [Function("ObtenerPersonaTipoSocialId")]
        [OpenApiOperation("ObtenerByIdspec", "ObtenerPersonaTipoSocialId", Description = "Sirve para obtener una PersonaTipoSocial por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la PersonaTipoSocial a obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PersonaTipoSocial), Description = "Devuelve la PersonaTipoSocial encontrada")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(string), Description = "No se encontró ninguna PersonaTipoSocial con el ID proporcionado")]
        public async Task<HttpResponseData> ObtenerPersonaTipoSocialId([HttpTrigger(AuthorizationLevel.Function, "get/{id}", Route = "obtenerPersonaTipoSocial/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para obtener PersonaTipoSocial por ID");

            try
            {
                var personaTipoSocial = await personaTipoSocialLogic.ObtenerPersonaTipoSocialById(id);
                if (personaTipoSocial != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(personaTipoSocial);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    await respuesta.WriteAsJsonAsync("La PersonaTipoSocial no fue encontrada");
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
