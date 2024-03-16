using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public interface IProfesion
    {
        string NombreProfesion { get; set; }
        string NombreGrado { get; set; }
        string Estado { get; set; }
    }
}
