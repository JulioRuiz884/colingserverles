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
    public class PersonaFunction
    {
        private readonly ILogger<PersonaFunction> _logger;
        private readonly IPersonaLogic personaLogic;

        public PersonaFunction(ILogger<PersonaFunction> logger, IPersonaLogic personaLogic)
        {
            _logger = logger;
            this.personaLogic = personaLogic;
        }

        [Function("ListarPersonas")]
        [OpenApiOperation("ListarSolicitudespec", "ListarPersonas", Description = "Sirve para listar todas las Personas")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Persona>), Description = "Devuelve la lista de Personas")]
        public async Task<HttpResponseData> ListarPersonas([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listarpersonas")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para insertar personas");
            try
            {
                var listaPersonas = personaLogic.ListarPersonaTodos();
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
        [Function("InsertarPersona")]
        [OpenApiOperation("Insertarspec", "InsertarPersona", Description = "Sirve para insertar una Persona")]
        [OpenApiRequestBody("application/json", typeof(Persona), Description = "Persona modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Persona), Description = "Insertara una Persona.")]
        public async Task<HttpResponseData> InsertarPersona([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarpersona")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para insertar personas");
            try
            {
                var per = await req.ReadFromJsonAsync<Persona>() ?? throw new Exception("Debe ingresar una persona con todos sus datos");
                bool seGuardo = await personaLogic.InsertarPersona(per);
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
        [Function("ModificarPersona")]
        [OpenApiOperation("Modificarspec", "ModificarPersona", Description = "Sirve para modificar una Persona")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la Persona a modificar")]
        [OpenApiRequestBody("application/json", typeof(Persona), Description = "Persona modelo con los campos actualizados")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la modificación fue exitosa")]
        public async Task<HttpResponseData> ModificarPersona([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarpersona/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para modificar personas");
            try
            {
                var persona = await req.ReadFromJsonAsync<Persona>();
                if (persona == null)
                {
                    throw new Exception("Debe ingresar una persona con todos sus datos a modificar");
                }
                bool seModifico = await personaLogic.ModificarPersona(persona, id);

                if (seModifico)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    await respuesta.WriteAsJsonAsync("La persona no fue encontrada para modificar");
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
        [Function("EliminarPersona")]
        [OpenApiOperation("Eliminarspec", "EliminarPersona", Description = "Sirve para eliminar una Persona por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la Persona a eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la eliminación fue exitosa")]
        public async Task<HttpResponseData> EliminarPersona([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarpersona/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para eliminar personas");

            try
            {
                bool seElimino = await personaLogic.EliminarPersona(id);
                if (seElimino)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    await respuesta.WriteAsJsonAsync("La persona no fue encontrada para eliminar");
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
        [Function("ObtenerPersonaId")]
        [OpenApiOperation("ObtenerByIdspec", "ObtenerPersonaId", Description = "Sirve para obtener una Persona por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la Persona a obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Persona), Description = "Devuelve la Persona encontrada")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(string), Description = "No se encontró ninguna Persona con el ID proporcionado")]
        public async Task<HttpResponseData> ObtenerPersonaId([HttpTrigger(AuthorizationLevel.Function, "get/{id}", Route = "obtenerpersona/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para obtener persona por ID");

            try
            {
                var persona = await personaLogic.ObtenerPersonaById(id);
                if (persona != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(persona);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    await respuesta.WriteAsJsonAsync("La persona no fue encontrada");
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
