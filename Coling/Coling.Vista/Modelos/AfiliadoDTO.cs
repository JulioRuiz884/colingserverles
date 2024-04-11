using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Modelos
{
    public class AfiliadoDTO
    {

        [Required(ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo Apellidos es requerido")]
        public string Apellidos { get; set; } = null!;

        [Required(ErrorMessage = "El campo Fecha Nacimiento es requerido")]
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;
        public string? Foto { get; set; }
        public string EstadoPersona { get; set; } = null!;


        [Required(ErrorMessage = "El campo Fecha Afilacion es requerido")]
        public DateTime FechaAfilacion { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El campo Codigo Afiliado es requerido")]
        public string CodigoAfiliado { get; set; } = null!;

        [Required(ErrorMessage = "El campo Nro Titulo Provisional es requerido")]
        public string NroTituloProvisional { get; set; } = null!;
        public string Estado { get; set; } = null!;
        [Required(ErrorMessage = "El campo Direccion es requerido")]
        public string Direccion { get; set; } = null!;
        [Required(ErrorMessage = "El campo Telefono es requerido")]
        public int NroTelefono { get; set; } = null!;
        public List<string>? idiomasLista { get; set; }

        public List<string>? socialesLista { get; set; }
    }
}
