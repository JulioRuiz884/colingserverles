using Coling.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public class IdiomaService: IIdiomaService
    {
        string url = "http://localhost:7169";
        string endPoint = "";
        HttpClient client = new HttpClient();

        public async Task<bool> EliminarIdioma(int id)
        {
            bool sw = false;
            endPoint = url + "/api/eliminaridioma/" + id;
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> InsertarIdioma(Idioma idioma)
        {
            bool sw = false;
            endPoint = url + "/api/insertaridioma";
            string jsonBody = JsonConvert.SerializeObject(idioma);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Idioma>> ListarIdioma()
        {
            endPoint = "api/listaridiomas";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Idioma> result = new List<Idioma>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Idioma>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> ModificarIdioma(Idioma idioma, int id)
        {
            bool sw = false;
            endPoint = url + "/api/modificaridioma/" + id;
            string jsonBody = JsonConvert.SerializeObject(idioma);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }
    }
}
