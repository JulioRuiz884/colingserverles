using Azure.Data.Tables;
using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Coling.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Implementacion.Repositorios
{
    public class EstudioRepositorio : IEstudiosRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string? tableName;
        private readonly IConfiguration configuration;

        public EstudioRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaConexion").Value;
            tableName = "EstudioRepositorio";
        }

        public async Task<bool> Eliminar(string partitiokey, string rowkey, string etag)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tableName);
                await tablaCliente.DeleteEntityAsync(partitiokey, rowkey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Insertar(Estudios estudios)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tableName);
                await tablaCliente.UpsertEntityAsync(estudios);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Modificar(Estudios estudios)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tableName);
                await tablaCliente.UpdateEntityAsync(estudios, estudios.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Estudios> ObtenerById(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tableName);
            var filtro = $"PartitionKey eq 'Educacion' and RowKey eq '{id}'";
            await foreach (Estudios estudios in tablaCliente.QueryAsync<Estudios>(filter: filtro))
            {
                return estudios;
            }
            return null;
        }

        public async Task<List<Estudios>> ObtenerTodo()
        {
            List<Estudios> lista = new List<Estudios>();
            var tablaCliente = new TableClient(cadenaConexion, tableName);
            var filtro = "$PartitionKey eq 'Educacion'";
            // Realizar la consulta y esperar el resultado
            await foreach (var estudios in tablaCliente.QueryAsync<Estudios>(filter: filtro))
            {
                lista.Add(estudios);
            }
            return lista;
        }
    }
}
