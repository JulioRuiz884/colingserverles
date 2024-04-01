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
    public class GradoAcademicoFunction
    {
        private readonly ILogger<GradoAcademicoFunction> _logger;
        private readonly IGradoAcademicoRepositorio repositorio;

        public GradoAcademicoFunction(ILogger<GradoAcademicoFunction> logger, IGradoAcademicoRepositorio repositorio)
        {
            _logger = logger;
            this.repositorio = repositorio;
        }

        [Function("EliminarGradoAcademico")]
        [OpenApiOperation("Eliminarspec", "EliminarGradoAcademico", Description = "Sirve para eliminar un GradoAcademico por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del GradoAcademico a eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la eliminación fue exitosa")]
        public async Task<HttpResponseData> EliminarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var datos = await req.ReadFromJsonAsync<GradoAcademico>() ?? throw new Exception("Debe ingresar un Grado Academico con todos sus datos");
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
        [Function("InsertarGradoAcademico")]
        [OpenApiOperation("Insertarspec", "InsertarGradoAcademico", Description = "Sirve para insertar un GradoAcademico")]
        [OpenApiRequestBody("application/json", typeof(Estudios), Description = "GradoAcademico modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Estudios), Description = "Insertara un GradoAcademico.")]
        public async Task<HttpResponseData> InsertarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<GradoAcademico>() ?? throw new Exception("Debe ingresar un Grado Academico con todos sus datos");
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
        [Function("ModificarGradoAcademico")]
        [OpenApiOperation("Modificarspec", "ModificarGradoAcademico", Description = "Sirve para modificar un GradoAcademico")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del GradoAcademico a modificar")]
        [OpenApiRequestBody("application/json", typeof(GradoAcademico), Description = "GradoAcademico modelo con los campos actualizados")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica si la modificación fue exitosa")]
        public async Task<HttpResponseData> ModificarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var datos = await req.ReadFromJsonAsync<GradoAcademico>() ?? throw new Exception("Debe ingresar un Grado Academico con todos sus datos");

                // Aquí deberías validar que la entidad a modificar tenga una clave de partición y una clave de fila.
                if (string.IsNullOrEmpty(datos.PartitionKey) || string.IsNullOrEmpty(datos.RowKey))
                {
                    throw new Exception("El Grado Academico debe tener una clave de partición y una clave de fila.");
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
        [Function("ListarGradoAcademicoById")]
        [OpenApiOperation("ObtenerByIdspec", "ListarGradoAcademicoById", Description = "Sirve para obtener un GradoAcademico por su ID")]
        [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "ID del GradoAcademico a obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GradoAcademico), Description = "Devuelve el GradoAcademico encontrada")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(string), Description = "No se encontró ningun GradoAcademico con el ID proporcionado")]
        public async Task<HttpResponseData> ListarGradoAcademicoById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarGradoAcademicoById/{id}")] HttpRequestData req, string id)
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
        [Function("ListarGradoAcademico")]
        [OpenApiOperation("Listarespec", "ListarGradoAcademico", Description = "Sirve para listar todas los GradoAcademicos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<GradoAcademico>), Description = "Devuelve la lista de GradoAcademicos")]
        public async Task<HttpResponseData> ListarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
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
