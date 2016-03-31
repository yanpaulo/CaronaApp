using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaronaApp.Server.Models.Entities
{
    public class PassageiroCarona
    {
        public int Id { get; set; }

        public int CaronaId { get; set; }

        //public Carona Carona { get; set; }

        public string Nome { get; set; }
    }
}