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
    public class TipoEstudioRepositorio : ITipoEstudioRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string? tableName;
        private readonly IConfiguration configuration;

        public TipoEstudioRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaConexion").Value;
            tableName = "TipoEstudio";
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

        public async Task<bool> Insertar(TipoEstudio tipoEstudio)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tableName);
                await tablaCliente.UpsertEntityAsync(tipoEstudio);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Modificar(TipoEstudio tipoEstudio)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tableName);
                await tablaCliente.UpdateEntityAsync(tipoEstudio, tipoEstudio.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<TipoEstudio> ObtenerById(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tableName);
            var filtro = $"PartitionKey eq 'Educacion' and RowKey eq '{id}'";
            await foreach (TipoEstudio tipoEstudio in tablaCliente.QueryAsync<TipoEstudio>(filter: filtro))
            {
                return tipoEstudio;
            }
            return null;
        }

        public async Task<List<TipoEstudio>> ObtenerTodo()
        {
            List<TipoEstudio> lista = new List<TipoEstudio>();
            var tablaCliente = new TableClient(cadenaConexion, tableName);
            var filtro = $"PartitionKey eq 'Educacion'";
            // Realizar la consulta y esperar el resultado
            await foreach (var tipoEstudio in tablaCliente.QueryAsync<TipoEstudio>(filter: filtro))
            {
                lista.Add(tipoEstudio);
            }
            return lista;
        }
    }
}
