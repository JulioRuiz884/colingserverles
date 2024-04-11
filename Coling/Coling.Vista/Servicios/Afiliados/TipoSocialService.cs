using Coling.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public class TipoSocialService: ITipoSocialService
    {
        string url = "http://localhost:7169";
        string endPoint = "";
        HttpClient client = new HttpClient();

        public async Task<bool> EliminarTipoSocial(int id)
        {
            bool sw = false;
            endPoint = url + "/api/eliminartiposocial/" + id;
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> InsertarTipoSocial(TipoSocial tipoSocial)
        {
            bool sw = false;
            endPoint = url + "/api/insertartiposocial";
            string jsonBody = JsonConvert.SerializeObject(tipoSocial);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<TipoSocial>> ListarTipoSocial()
        {
            endPoint = "api/listartiposocial";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<TipoSocial> result = new List<TipoSocial>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<TipoSocial>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> ModificarTipoSocial(TipoSocial tipoSocial, int id)
        {
            bool sw = false;
            endPoint = url + "/api/modificartiposocial/" + id;
            string jsonBody = JsonConvert.SerializeObject(tipoSocial);
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
