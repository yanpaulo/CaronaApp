using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Web.Http;
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
            HttpResponseMessage response = await client.GetAsync(new Uri("http://carona.yanscorp.com/api/caronas"));
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
            HttpResponseMessage response = await client.GetAsync(new Uri($"http://carona.yanscorp.com/api/caronas/{id}"));
            string responseText = await response.Content.ReadAsStringAsync();
            JsonObject jsObject = JsonObject.Parse(responseText);
            Carona c = FromJsonObject(jsObject);

            return c;
        }

        public static async void CreateCarona(Carona carona)
        {
            HttpClient client = new HttpClient();
            
            JsonObject jsObject = new JsonObject();
            jsObject["QuantidadeVagas"] = JsonValue.CreateNumberValue(carona.QuantidadeVagas);
            jsObject["Latitude"] = JsonValue.CreateNumberValue(carona.Location.Position.Latitude);
            jsObject["Longitude"] = JsonValue.CreateNumberValue(carona.Location.Position.Longitude);
            jsObject["Nome"] = JsonValue.CreateStringValue(carona.DisplayName);
            HttpStringContent content = new HttpStringContent(jsObject.Stringify(), Windows.Storage.Streams.UnicodeEncoding.Utf8, "text/json");
            HttpResponseMessage response = await client.PostAsync(new Uri("http://carona.yanscorp.com/api/caronas"), content);
            string responseText = await response.Content.ReadAsStringAsync();
            responseText += "";
            int i = 0;
            i++;
        }

        private static Carona FromJsonObject(JsonObject jsObject)
        {
            return new Carona
            {
                Id = (int)jsObject["Id"].GetNumber(),
                DisplayName = jsObject["Nome"].ValueType == JsonValueType.String ? jsObject["Nome"].GetString() : string.Empty,
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

