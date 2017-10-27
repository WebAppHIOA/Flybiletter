using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
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
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",
               ApplyFormatInEditMode = true)]
        [Display(Name = "Dato")]
        public string Date { get; set; }
        public string DepartureTime { get; set; }
        public bool Cancelled  { get; set; }

        public virtual Airport Airport { get; set; }
        public virtual List<Order> Order { get; set; }
        public virtual List<Departure> DepartureDetails { get; set; }
    }
}