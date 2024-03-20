using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Coling.Repositorio;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Coling.Autentificacion.Modelo;
using System.Net;
using Coling.Repositorio.Contratos;
using Microsoft.Azure.Functions.Worker.Http;
using System.ComponentModel.DataAnnotations;

namespace Coling.Autentificacion
{
    public class AccountFunction
    {
        private readonly ILogger<AccountFunction> _logger;

        public AccountFunction(ILogger<AccountFunction> logger)
        {
            _logger = logger;
        }

        [Function("Login")]
        [OpenApiOperation("Accountespec", "Account", Description = "Se obtiene el token si las credenciales son validas")]
        [OpenApiRequestBody("application/json", typeof(Credenciales), Description = "Introduzca los datos de credenciales modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ITokenData), Description = "El token es")]
        public async Task<HttpResponseData> Login([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            HttpResponseData? respuesta = null;
            var login = await req.ReadFromJsonAsync<Credenciales>() ?? throw new ValidationException("Sus credenciales deben ser completas");

        }
    }
}
