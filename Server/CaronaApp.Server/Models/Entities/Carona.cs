using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaronaApp.Server.Models.Entities
{
    public class Carona
    {
        public int Id { get; set; }

        public Guid UserGuid { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}