using Azure.Data.Tables;
using Azure;
using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Modelos
{
    public class ExperienciaLaborales : IExperienciaLaboral
    {
        public int CodigoAfiliado { get; set; }
        public string NombreInstitucion { get; set; }
        public string CargoTitulo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Estado { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public string ETag { get; set; }
    }
}
