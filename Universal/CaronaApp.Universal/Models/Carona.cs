using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace CaronaApp.Universal.Models
{
    public class Carona
    {
        public string DisplayName { get; set; } = "Ôme da carona";
        public Geopoint Location { get; set; }

        //public Uri ImageSourceUri { get; set; }
        //public string MoreInfo { get; set; }
        //public Point NormalizedAnchorPoint { get; set; }
    }
}
