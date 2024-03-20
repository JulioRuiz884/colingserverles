using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Repositorio.Contratos
{
    public interface IUsuarioRepositorio
    {
        public Task<bool> VerificarCredenciales(string usuariox, string passwordx);
        public Task<string> EncriptarPassword(string password);
        //public Task<string> DesencriptarPassword(string password);
    }
}
