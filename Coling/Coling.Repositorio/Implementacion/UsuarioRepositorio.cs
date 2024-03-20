using Coling.Repositorio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Repositorio.Implementacion
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        public Task<string> EncriptarPassword(string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerificarCredenciales(string usuariox, string passwordx)
        {
            throw new NotImplementedException();
        }
    }
}
