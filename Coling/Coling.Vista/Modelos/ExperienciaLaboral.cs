using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Azure;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Coling.Vista.Modelos
{
    public class ExperienciaLaboral : IExperienciaLaboral
    {
        public int CodigoAfiliado { get; set; }

        [Display(Name = "NombreInstitucion")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string? NombreInstitucion { get; set; }

        [Display(Name = "CargoTitulo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string? CargoTitulo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string? Estado { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public string ETag { get; set; }
    }
}
