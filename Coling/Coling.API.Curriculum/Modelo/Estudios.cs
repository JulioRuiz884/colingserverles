using Azure;
using Azure.Data.Tables;
using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Modelo
{
    public class Estudios : IEstudios, ITableEntity
    {
        public string TipoEstudio { get; set; }
        public string NombreGrado { get; set; }
        public string TituloRecibido { get; set; }
        public string NombreInstitucion { get; set; }
        public int Anio { get; set; }
        public string Estado { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
