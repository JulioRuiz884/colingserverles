using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Curriculum.Endpoints
{
    public class InstitucionFunction
    {
        private readonly ILogger<InstitucionFunction> _logger;
        private readonly IInstitucionRepositorio repositorio;

        public InstitucionFunction(ILogger<InstitucionFunction> logger, IInstitucionRepositorio repositorio)
        {
            _logger = logger;
            this.repositorio = repositorio;
        }

        [Function("EliminarInstitucion")]
        public async Task<HttpResponseData> EliminarInstitucion([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var datos = await req.ReadFromJsonAsync<Institucion>() ?? throw new Exception("Debe ingresar una Institución con todos sus datos");
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
        [Function("InsertarInstitucion")]
        [OpenApiOperation("Listarespec", "InsertarInstitucion", Description = "Sirve para insertar una institucion")]
        [OpenApiRequestBody("application/json", typeof(Institucion), Description = "Institucion modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
                                 bodyType: typeof(Institucion), Description = "Mostrara la institucion creada")]
        public async Task<HttpResponseData> InsertarInstitucion([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<Institucion>() ?? throw new Exception("Debe ingresar una Institucion con todos sus datos");
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
        [Function("ModificarInstitucion")]
        public async Task<HttpResponseData> ModificarInstitucion([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var datos = await req.ReadFromJsonAsync<Institucion>() ?? throw new Exception("Debe ingresar una Institución con todos sus datos");

                // Aquí deberías validar que la entidad a modificar tenga una clave de partición y una clave de fila.
                if (string.IsNullOrEmpty(datos.PartitionKey) || string.IsNullOrEmpty(datos.RowKey))
                {
                    throw new Exception("La Institución debe tener una clave de partición y una clave de fila.");
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
        [Function("ListarInstitucionById")]
        public async Task<HttpResponseData> ListarInstitucionById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarInstitucionById/{id}")] HttpRequestData req, string id)
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
        [Function("ListarInstitucion")]
        [OpenApiOperation("Listarespec", "ListarInstitucion", Description = "Sirve para listar todas las instituciones")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
                                 bodyType: typeof(List<Institucion>), Description = "Mostrara una lista de Instituciones")]
        public async Task<HttpResponseData> ListarInstitucion([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
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
        [Function("ListarNombres")]
        [OpenApiOperation("Listarespec", "ListarNombres", Description = "Sirve para listar todas los Nombres")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
                                 bodyType: typeof(List<string>), Description = "Mostrara una lista de Nombres")]
        public async Task<HttpResponseData> ListarNombres([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                List<string> lista = new List<string>();
                lista.Add("UPDS");
                lista.Add("SARACHO");
                lista.Add("UNO");
                respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(lista);
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
