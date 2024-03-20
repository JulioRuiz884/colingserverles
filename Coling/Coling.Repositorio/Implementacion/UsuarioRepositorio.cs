using Coling.Repositorio.Contratos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Repositorio.Implementacion
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private readonly IConfiguration configuration;

        public UsuarioRepositorio(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<ITokenData> ConstruirToken(string usuarioname, string password)
        {
            var claims = new List<Claim>()
            {
                new Claim("usuario", usuarioname),
                new Claim("rol", "Admin"),
                new Claim("estado", "Activo")
            };

            var SecretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["LlaveSecreta"] ?? ""));
            TokenData respuestaToken = new TokenData();
            return respuestaToken;
        }

        public async Task<string> EncriptarPassword(string password)
        {
            string Encriptado = "";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                Encriptado = Convert.ToBase64String(bytes);
            }
            return Encriptado;
        }

        public Task<bool> ValidarToken(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VerificarCredenciales(string usuariox, string passwordx)
        {
            bool sw = false;
            string passEncriptado = await EncriptarPassword(passwordx);
            string consulta = "select count(idusuario) from usuario where nombreuser='" + usuariox + "' and password='" + passEncriptado + "'";
            int Existe = Conexion.EjecutarEscalar(consulta);
            if (Existe > 0)
            {
                sw = true;
            }
            return sw;
        }
    }
}
