using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public interface IExperienciaLaboral
    {
        int CodigoAfiliado { get; set; }
        string NombreInstitucion { get; set; }
        string CargoTitulo { get; set; }
        DateTime FechaInicio { get; set; }
        DateTime FechaFinal { get; set; }
        string Estado { get; set; }
    }
}
