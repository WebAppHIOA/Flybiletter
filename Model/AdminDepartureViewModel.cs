using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Model
{
    public class AdminDepartureViewModel
    {
        [Key]
        
        public string FlightId { get; set; }
        [Required(ErrorMessage = "Opprinnelig sted må oppgis")]
        public string From { get; set; }
        [Required(ErrorMessage = "Destinasjon må oppgis")]
        public string To { get; set; }
        [Required(ErrorMessage = "Dato må oppgis")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Avgangstid må oppgis")]
        public string DepartureTime { get; set; }
        public bool Cancelled  { get; set; }

        public virtual List<Airport> Airport { get; set; }
        public virtual List<Order> Order { get; set; }
        public virtual List<Departure> DepartureDetails { get; set; }
    }
}