using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Devices.Geolocation;

namespace CaronaApp.Universal.Models
{
    public class CaronaService
    {
        public CaronaService()
        {

        }

        public static async Task<List<Carona>> GetCaronas()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://carona.yanscorp.com/api/caronas");
            string responseText = await response.Content.ReadAsStringAsync();
            JsonArray jsArray = JsonArray.Parse(responseText);
            List<Carona> caronas = new List<Carona>();
            foreach (var jsArrayItem in jsArray)
            {
                if (jsArrayItem != null && jsArrayItem.ValueType == JsonValueType.Object)
                {
                    var item = jsArrayItem.GetObject();
                    Carona c = FromJsonObject(item);
                    caronas.Add(c);
                }
            }
            return caronas;
        }

        public static async Task<Carona> GetCarona(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"http://carona.yanscorp.com/api/caronas/{id}");
            string responseText = await response.Content.ReadAsStringAsync();
            JsonObject jsObject = JsonObject.Parse(responseText);
            Carona c = FromJsonObject(jsObject);

            return c;
        }

        private static Carona FromJsonObject(JsonObject jsObject)
        {
            return new Carona
            {
                Id = (int)jsObject["Id"].GetNumber(),
                DisplayName = jsObject["Nome"].ToString(),
                Location = new Geopoint(new BasicGeoposition
                {
                    Latitude = jsObject["Latitude"].GetNumber(),
                    Longitude = jsObject["Longitude"].GetNumber()
                })
                //, Passageiros = PassageirosFromJsonArray(jsObject["Passageiros"].GetArray())
            };
        }


        private static List<PassageiroCarona> PassageirosFromJsonArray(JsonArray array)
        {
            List<PassageiroCarona> passageiros = new List<PassageiroCarona>();
            foreach (var jsArrayItem in array)
            {
                if (jsArrayItem != null && jsArrayItem.ValueType == JsonValueType.Object)
                {
                    var item = jsArrayItem.GetObject();
                    PassageiroCarona p = PassageiroFromJsonObject(item);
                    passageiros.Add(p);
                }

            }
            return passageiros;
        }

        private static PassageiroCarona PassageiroFromJsonObject(JsonObject jsObject)
        {
            return new PassageiroCarona
            {
                Id = (int)jsObject["Id"].GetNumber(),
                Nome = jsObject["Nome"].GetString()
            };
        }
    }

}
}
