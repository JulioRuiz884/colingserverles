using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public interface IEstudios
    {
        string TipoEstudio { get; set; }
        string NombreGrado { get; set; }
        string TituloRecibido { get; set; }
        string NombreInstitucion { get; set; }
        int Anio { get; set; }
        string Estado { get; set; }
    }
}
