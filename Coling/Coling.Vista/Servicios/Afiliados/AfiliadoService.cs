using Coling.Vista.Modelos;
using Coling.Vista.Servicios.Afiliados.Contratos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public class AfiliadoService: IAfiliadoService
    {
        string url = "http://localhost:7169";
        string endPoint = "";
        HttpClient client = new HttpClient();
        public async Task<bool> InsertarAfiliado(Afiliado afiliado, string token)
        {
            bool sw = false;
            endPoint = url + "/api/insertarAfiliado";
            string jsonBody = JsonConvert.SerializeObject(afiliado);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }
        public async Task<bool> ModificarAfiliado(Afiliado afiliado, int id, string token)
        {
            bool sw = false;
            endPoint = url + "/api/modificarAfiliado/" + id;
            string jsonBody = JsonConvert.SerializeObject(afiliado);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }
        public async Task<bool> EliminarAfiliado(int id, string token)
        {
            bool sw = false;
            endPoint = url + "/api/eliminarAfiliado/" + id;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }
        public async Task<Afiliado> ObtenerAfiliadoById(int id, string token)
        {
            endPoint = "/api/obtenerAfiliadoById/" + id;
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(url);
            }
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.GetAsync(endPoint);
            Afiliado afiliado = new Afiliado();
            if (respuesta.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await respuesta.Content.ReadAsStringAsync();
                afiliado = JsonConvert.DeserializeObject<Afiliado>(respuestaCuerpo)!;
            }
            return afiliado;
        }
        public async Task<List<Afiliado>> ListarAfiliados(string token)
        {
            endPoint = "/api/listarafiliados";
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(url);
            }
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Afiliado> result = new List<Afiliado>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Afiliado>>(respuestaCuerpo)!;
            }

            return result;
        }

        public async Task<bool> InsertarAfiliadoCompleto(AfiliadoDTO afiliado, string token)
        {
            bool sw = false;
            endPoint = url + "/api/InsertarAfiliadoCompleto";
            string jsonBody = JsonConvert.SerializeObject(afiliado);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }
    }
}
