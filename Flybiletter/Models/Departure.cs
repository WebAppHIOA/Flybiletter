using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public class Departure
    {
        [Key]
        public string FlightId { get; set; }
        [Required(ErrorMessage = "Opprinnelig sted må oppgis")]
        public string From { get; set; }
        [Required(ErrorMessage = "Destinasjon må oppgis")]
        public string To { get; set; }
        [Required(ErrorMessage = "Reisetype må velges")]
        public string Arrival { get; set; }

        // Tidspunkt for avgang, ikke dato
        [Required(ErrorMessage = "Dato må oppgis")]
        public string DepartureTime { get; set; }
     

        public virtual Airport Airport { get; set; }
        public virtual List<Passenger> Passenger { get; set; }
    }
}