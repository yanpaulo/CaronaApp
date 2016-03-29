using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.Devices.Geolocation
{
    public static class GeopositionExtensions
    {
        public static Geopoint AsGeopoint(this Geoposition g)
        {
            return new Geopoint(new BasicGeoposition() { Latitude = g.Coordinate.Latitude, Longitude = g.Coordinate.Longitude });
        }
    }
}
