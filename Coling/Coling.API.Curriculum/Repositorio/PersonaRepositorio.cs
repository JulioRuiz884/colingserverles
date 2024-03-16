using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Repositorio
{
    public class PersonaRepositorio
    {
        public string cadenaConexion;
        private string tablaName;

        public PersonaRepositorio(string cadena, string tabla)
        {
            this.cadenaConexion = cadena;
            this.tablaName = tabla;
        }

    }
}
