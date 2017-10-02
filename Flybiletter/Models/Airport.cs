using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public class Airport
    {
        public string AirportId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Continent { get; set; }
        public string Fee { get; set; }
        public virtual List<Departure> Departure { get; set; }
    }
}