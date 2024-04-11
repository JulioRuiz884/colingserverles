using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Coling.Shared;
using Coling.Utilitarios.Attributes;
using Coling.Utilitarios.Roles;
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
    public class EstudioFunction
    {
        private readonly ILogger<EstudioFunction> _logger;
        private readonly IEstudiosRepositorio repositorio;

        public EstudioFunction(ILogger<EstudioFunction> logger, IEstudiosRepositorio repositorio)
        {
            _logger = logger;
            this.repositorio = repositorio;
        }

        [Function("EliminarEstudio")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria)]
        [OpenApiOperation("Eliminarspec", "EliminarEstudio", Description = "Sirve para eliminar un Estudio por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del Estudio a eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la eliminación fue exitosa")]
        public async Task<HttpResponseData> EliminarEstudio([HttpTrigger(AuthorizationLevel.Anonymous, "delete")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var datos = await req.ReadFromJsonAsync<Estudios>() ?? throw new Exception("Debe ingresar un Estudio con los datos a eliminar");
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
        [Function("InsertarEstudio")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria)]
        [OpenApiOperation("Insertarspec", "InsertarEstudio", Description = "Sirve para insertar un Estudio")]
        [OpenApiRequestBody("application/json", typeof(Estudios), Description = "Estudio modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Estudios), Description = "Insertara un Estudio.")]
        public async Task<HttpResponseData> InsertarEstudio([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<Estudios>() ?? throw new Exception("Debe ingresar un Estudio con todos sus datos");
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
        [Function("ModificarEstudio")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria)]
        [OpenApiOperation("Modificarspec", "ModificarEstudio", Description = "Sirve para modificar un Estudio")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del Estudio a modificar")]
        [OpenApiRequestBody("application/json", typeof(Estudios), Description = "Estudio modelo con los campos actualizados")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la modificación fue exitosa")]
        public async Task<HttpResponseData> ModificarEstudio([HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var datos = await req.ReadFromJsonAsync<Estudios>() ?? throw new Exception("Debe ingresar los datos de un Estudio a modificar");

                // Aquí deberías validar que la entidad a modificar tenga una clave de partición y una clave de fila.
                if (string.IsNullOrEmpty(datos.PartitionKey) || string.IsNullOrEmpty(datos.RowKey))
                {
                    throw new Exception("El Estudio debe tener una clave de partición y una clave de fila.");
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
        [Function("ListarEstudioById")]
        [OpenApiOperation("ObtenerByIdspec", "ListarEstudioById", Description = "Sirve para obtener un Estudio por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del Estudio a obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Estudios), Description = "Devuelve el Estudio encontrada")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(string), Description = "No se encontró ningun Estudio con el ID proporcionado")]
        public async Task<HttpResponseData> ListarEstudioById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ListarEstudioById/{id}")] HttpRequestData req, string id)
        {
            HttpResponseData respuesta;
            try
            {
                var estudio = await repositorio.ObtenerById(id);

                if (estudio != null)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(estudio);
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
        [Function("ListarEstudio")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria)]
        [OpenApiOperation("Listarespec", "ListarEstudio", Description = "Sirve para listar todas los Estudios")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
                                 bodyType: typeof(List<Estudios>), Description = "Devuelve la lista de Estudios")]
        public async Task<HttpResponseData> ListarEstudio([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
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
