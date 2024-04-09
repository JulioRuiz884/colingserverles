using Azure.Data.Tables;
using Azure;
using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Coling.Vista.Modelos
{
    public class Estudio : IEstudios
    {
        [Display(Name = "TipoEstudio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string TipoEstudio { get; set; }

        [Display(Name = "NombreGrado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string NombreGrado { get; set; }

        [Display(Name = "TituloRecibido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string TituloRecibido { get; set; }
        public string NombreInstitucion { get; set; }
        public int Anio { get; set; }
        public string Estado { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public string ETag { get; set; }
    }
}
