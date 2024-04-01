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
    public class ProfesionAfiliadoFunction
    {
        private readonly ILogger<ProfesionAfiliadoFunction> _logger;
        private readonly IProfesionAfiliadoLogic profesionAfiliadoLogic;

        public ProfesionAfiliadoFunction(ILogger<ProfesionAfiliadoFunction> logger, IProfesionAfiliadoLogic profesionAfiliadoLogic)
        {
            _logger = logger;
            this.profesionAfiliadoLogic = profesionAfiliadoLogic;
        }

        [Function("ListarProfesionAfiliado")]
        [OpenApiOperation("ListarSolicitudespec", "ListarProfesionAfiliado", Description = "Sirve para listar todas las ProfesionAfiliado")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Direccion>), Description = "Devuelve la lista de ProfesionAfiliado")]
        public async Task<HttpResponseData> ListarProfesionAfiliadoLogic([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarProfesionAfiliado")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para insertar ProfesionAfiliado");
            try
            {
                var listaProfesionAfiliado = profesionAfiliadoLogic.ListarProfesionAfiliadoTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaProfesionAfiliado.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("InsertarProfesionAfiliado")]
        [OpenApiOperation("Insertarspec", "InsertarProfesionAfiliado", Description = "Sirve para insertar una ProfesionAfiliado")]
        [OpenApiRequestBody("application/json", typeof(ProfesionAfiliado), Description = "ProfesionAfiliado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ProfesionAfiliado), Description = "Insertara una ProfesionAfiliado.")]
        public async Task<HttpResponseData> InsertarProfesionAfiliadoLogic([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarProfesionAfiliado")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para insertar ProfesionAfiliado");
            try
            {
                var dir = await req.ReadFromJsonAsync<ProfesionAfiliado>() ?? throw new Exception("Debe ingresar un ProfesionAfiliado");
                bool seGuardo = await profesionAfiliadoLogic.InsertarProfesionAfiliado(dir);
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
        [Function("ModificarProfesionAfiliado")]
        [OpenApiOperation("Modificarspec", "ModificarProfesionAfiliado", Description = "Sirve para modificar una ProfesionAfiliado")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la ProfesionAfiliado a modificar")]
        [OpenApiRequestBody("application/json", typeof(ProfesionAfiliado), Description = "ProfesionAfiliado modelo con los campos actualizados")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la modificación fue exitosa")]
        public async Task<HttpResponseData> ModificarAfiliadoIdioma([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarProfesionAfiliado/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para modificar ProfesionAfiliado");
            try
            {
                var profesionAfiliado = await req.ReadFromJsonAsync<ProfesionAfiliado>();
                if (profesionAfiliado == null)
                {
                    throw new Exception("Debe ingresar un datos ProfesionAfiliado a modificar");
                }
                bool seModifico = await profesionAfiliadoLogic.ModificarProfesionAfiliado(profesionAfiliado, id);

                if (seModifico)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    await respuesta.WriteAsJsonAsync("La ProfesionAfiliado no fue encontrado para modificar");
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
        [Function("EliminarProfesionAfiliado")]
        [OpenApiOperation("Eliminarspec", "EliminarProfesionAfiliado", Description = "Sirve para eliminar una ProfesionAfiliado por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la ProfesionAfiliado a eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la eliminación fue exitosa")]
        public async Task<HttpResponseData> EliminarAfiliadoIdioma([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarProfesionAfiliado/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para eliminar ProfesionAfiliado");

            try
            {
                bool seElimino = await profesionAfiliadoLogic.EliminarProfesionAfiliado(id);
                if (seElimino)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    await respuesta.WriteAsJsonAsync("El ProfesionAfiliado no fue encontrada para eliminar");
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
        [Function("ObtenerProfesionAfiliadoId")]
        [OpenApiOperation("ObtenerByIdspec", "ObtenerProfesionAfiliadoId", Description = "Sirve para obtener una ProfesionAfiliado por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la ProfesionAfiliado a obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ProfesionAfiliado), Description = "Devuelve la ProfesionAfiliado encontrada")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(string), Description = "No se encontró ninguna ProfesionAfiliado con el ID proporcionado")]
        public async Task<HttpResponseData> ObtenerProfesionAfiliadoId([HttpTrigger(AuthorizationLevel.Function, "get/{id}", Route = "obtenerProfesionAfiliado/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            _logger.LogInformation("Ejecutando Azure Function para obtener ProfesionAfiliado por ID");

            try
            {
                var profesionAfiliado = await profesionAfiliadoLogic.ObtenerProfesionAfiliadoById(id);
                if (profesionAfiliado != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(profesionAfiliado);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    await respuesta.WriteAsJsonAsync("El ProfesionAfiliado no fue encontrado");
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
