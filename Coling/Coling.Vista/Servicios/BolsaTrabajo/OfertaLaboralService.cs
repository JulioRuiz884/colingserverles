using Coling.Shared;
using Coling.Vista.Modelos;
using Coling.Vista.Servicios.BolsaTrabajo.Contratos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.BolsaTrabajo
{
    public class OfertaLaboralService:IOfertaLaboralService
    {
        string url = " http://localhost:7060";
        string endPoint = "";
        HttpClient client = new HttpClient();

        public async Task<bool> EliminarOferta(string id, string token)
        {
            bool sw = false;
            endPoint = url + "/api/EliminarOfertaLaboral/" + id;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> InsertarOferta(OfertaLaboral oLaboral, string token)
        {
            bool sw = false;
            endPoint = url + "/api/InsertarOfertas";
            string jsonBody = JsonConvert.SerializeObject(oLaboral);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<OfertaLaboral>> Listarofertas(string token)
        {
			endPoint = "/api/ListarOfertas";

			// Asegúrate de que BaseAddress se establezca antes de realizar la solicitud
			if (client.BaseAddress == null)
			{
				client.BaseAddress = new Uri(url);
			}

			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

			HttpResponseMessage response = await client.GetAsync(endPoint);
			List<OfertaLaboral> result = new List<OfertaLaboral>();
			if (response.IsSuccessStatusCode)
			{
				string respuestaCuerpo = await response.Content.ReadAsStringAsync();
				result = JsonConvert.DeserializeObject<List<OfertaLaboral>>(respuestaCuerpo);
			}
			return result;
		}

		public async Task<bool> ModificarOferta(OfertaLaboral oLaboral, string id, string token)
        {
            bool sw = false;
            endPoint = url + "/api/ModificarOferta/" + id;
            string jsonBody = JsonConvert.SerializeObject(oLaboral);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<OfertaLaboral> ObtenerOfertaById(string id, string token)
        {
            endPoint = "/api/ObtenerOfertaById/" + id;
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(url);
            }
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.GetAsync(endPoint);
            OfertaLaboral oLaboral = new OfertaLaboral();
            if (respuesta.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await respuesta.Content.ReadAsStringAsync();
                oLaboral = JsonConvert.DeserializeObject<OfertaLaboral>(respuestaCuerpo)!;
            }
            return oLaboral;
        }
    }
}
