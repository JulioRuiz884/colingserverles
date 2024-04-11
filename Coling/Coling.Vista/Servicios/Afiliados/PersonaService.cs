using Coling.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public class PersonaService: IPersonaService
    {
        string url = "http://localhost:7169";
        string endPoint = "";
        HttpClient client = new HttpClient();

        public async Task<bool> EliminarPersonas(int id)
        {
            bool sw = false;
            endPoint = url + "/api/eliminarpersona/" + id;
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> InsertarPersonas(Persona persona)
        {
            bool sw = false;
            endPoint = url + "/api/insertarpersona";
            string jsonBody = JsonConvert.SerializeObject(persona);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Persona>> ListarPersonas()
        {
            endPoint = "api/listarpersonas";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Persona> result = new List<Persona>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Persona>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> ModificarPersonas(Persona persona, int id)
        {
            bool sw = false;
            endPoint = url + "/api/modificarpersona/" + id;
            string jsonBody = JsonConvert.SerializeObject(persona);
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
