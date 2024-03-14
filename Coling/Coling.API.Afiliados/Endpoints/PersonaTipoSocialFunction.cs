using Coling.API.Afiliados.Contratos;
using Coling.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
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
