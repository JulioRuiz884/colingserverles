using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Coling.API.Curriculum.Endpoints
{
    public class ProfesionFunction
    {
        private readonly ILogger<ProfesionFunction> _logger;
        private readonly IProfesionRepositorio repositorio;

        public ProfesionFunction(ILogger<ProfesionFunction> logger, IProfesionRepositorio repositorio)
        {
            _logger = logger;
            this.repositorio = repositorio;
        }

        [Function("EliminarProfesion")]
        [OpenApiOperation("Eliminarspec", "EliminarProfesion", Description = "Sirve para eliminar una Profesion por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la Profesion a eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la eliminación fue exitosa")]
        public async Task<HttpResponseData> EliminarProfesion([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var datos = await req.ReadFromJsonAsync<Profesion>() ?? throw new Exception("Debe ingresar una Profesion con todos sus datos");
                string partitiokey = datos.PartitionKey;
                string rowkey = datos.RowKey;

                bool eliminado = await repositorio.Eliminar(partitiokey, rowkey, null);

                if (eliminado)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
        [Function("InsertarProfesion")]
        [OpenApiOperation("Insertarspec", "InsertarProfesion", Description = "Sirve para insertar una Profesion")]
        [OpenApiRequestBody("application/json", typeof(Profesion), Description = "Profesion modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Profesion), Description = "Insertara una Profesion.")]
        public async Task<HttpResponseData> InsertarProfesion([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<Profesion>() ?? throw new Exception("Debe ingresar una Profesion con todos sus datos");
                registro.RowKey = Guid.NewGuid().ToString();
                registro.Timestamp = DateTime.UtcNow;
                bool sw = await repositorio.Insertar(registro);
                if (sw)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
        [Function("ModificarProfesion")]
        [OpenApiOperation("Modificarspec", "ModificarProfesion", Description = "Sirve para modificar una Profesion")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la Profesion a modificar")]
        [OpenApiRequestBody("application/json", typeof(Profesion), Description = "Profesion modelo con los campos actualizados")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la modificación fue exitosa")]
        public async Task<HttpResponseData> ModificarProfesion([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var datos = await req.ReadFromJsonAsync<Profesion>() ?? throw new Exception("Debe ingresar una Profesion con todos sus datos");

                // Aquí deberías validar que la entidad a modificar tenga una clave de partición y una clave de fila.
                if (string.IsNullOrEmpty(datos.PartitionKey) || string.IsNullOrEmpty(datos.RowKey))
                {
                    throw new Exception("La Profesion debe tener una clave de partición y una clave de fila.");
                }

                bool modificado = await repositorio.Modificar(datos);

                if (modificado)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
        [Function("ListarProfesionById")]
        [OpenApiOperation("ObtenerByIdspec", "ListarProfesionById", Description = "Sirve para obtener una Profesion por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID de la Profesion a obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Estudios), Description = "Devuelve la Profesion encontrada")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(string), Description = "No se encontró ninguna Profesion con el ID proporcionado")]
        public async Task<HttpResponseData> ListarProfesionById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarProfesionById/{id}")] HttpRequestData req, string id)
        {
            HttpResponseData respuesta;
            try
            {
                var institucion = await repositorio.ObtenerById(id);

                if (institucion != null)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(institucion);
                    return respuesta;
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    return respuesta;
                }
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
        [Function("ListarProfesion")]
        [OpenApiOperation("Listarespec", "ListarProfesion", Description = "Sirve para listar todas los Estudios")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Estudios>), Description = "Devuelve la lista de Estudios")]
        public async Task<HttpResponseData> ListarProfesion([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var lista = repositorio.ObtenerTodo();
                respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(lista.Result);
                return respuesta;

            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
    }
}
