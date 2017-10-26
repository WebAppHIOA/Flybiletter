using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Model
{
    [TrackChanges]
    public class Departure
    {
        [Key]
        public string FlightId { get; set; }
        [Required(ErrorMessage = "Opprinnelig sted må oppgis")]
        public string From { get; set; }
        [Required(ErrorMessage = "Destinasjon må oppgis")]
        public string To { get; set; }
        [Required(ErrorMessage = "Dato må oppgis")]
        public string Date { get; set; }
        public string DepartureTime { get; set; }
        public bool Cancelled  { get; set; }

        public virtual Airport Airport { get; set; }
        public virtual List<Order> Order { get; set; }
    }
}