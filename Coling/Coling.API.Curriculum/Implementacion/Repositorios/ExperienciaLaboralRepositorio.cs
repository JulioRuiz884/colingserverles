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
    public class ExperienciaLaboralRepositorio : IExperienciaLaboralRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string? tableName;
        private readonly IConfiguration configuration;

        public ExperienciaLaboralRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaConexion").Value;
            tableName = "ExperienciaLaboral";
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

        public async Task<bool> Insertar(ExperienciaLaboral experienciaLaboral)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tableName);
                await tablaCliente.UpsertEntityAsync(experienciaLaboral);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Modificar(ExperienciaLaboral experienciaLaboral)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tableName);
                await tablaCliente.UpdateEntityAsync(experienciaLaboral, experienciaLaboral.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ExperienciaLaboral> ObtenerById(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tableName);
            var filtro = $"PartitionKey eq 'Educacion' and RowKey eq '{id}'";
            await foreach (ExperienciaLaboral explab in tablaCliente.QueryAsync<ExperienciaLaboral>(filter: filtro))
            {
                return explab;
            }
            return null;
        }

        public async Task<List<ExperienciaLaboral>> ObtenerTodo()
        {
            List<ExperienciaLaboral> lista = new List<ExperienciaLaboral>();
            var tablaCliente = new TableClient(cadenaConexion, tableName);
            var filtro = $"PartitionKey eq 'Educacion'";
            // Realizar la consulta y esperar el resultado
            await foreach (var explab in tablaCliente.QueryAsync<ExperienciaLaboral>(filter: filtro))
            {
                lista.Add(explab);
            }
            return lista;
        }
    }
}
