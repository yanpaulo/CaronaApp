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
                    Carona c = new Carona
                    {
                        Location = new Geopoint(new BasicGeoposition()
                        {
                            Latitude = item["Latitude"].GetNumber(),
                            Longitude = item["Longitude"].GetNumber()
                        })
                    };
                    caronas.Add(c);
                }
            }
            return caronas;
        }
    }
}
