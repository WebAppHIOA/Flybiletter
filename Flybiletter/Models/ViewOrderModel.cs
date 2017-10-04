using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public class ViewOrderModel
    {
        [Key]
        public string FlightId { get; set; }

        [Required(ErrorMessage = "Dato må oppgis")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Må ha opprinnelse sted")]
        public  Airport FromAirport { get; set; }

        [Required(ErrorMessage = "Må ha destinasjon")]
        public  Airport ToAirport { get; set; }

    }
}