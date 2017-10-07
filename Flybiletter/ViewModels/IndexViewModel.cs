using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Flybiletter.Models;

namespace Flybiletter.ViewModels
{
    public class IndexViewModel
    {
        [Required(ErrorMessage ="Du må oppgi dato")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",
               ApplyFormatInEditMode = true)]
        [Display(Name = "Dato")]
        public string TravelDate { get; set; }
        
        public List<Airport> ToAirport { get; set; }

        [Required(ErrorMessage = "Du må oppgi avreise")]
        [Display(Name= "Til")]
        public string ToAirportID { get; set; }

        public List<Airport> FromAirport { get; set; }

        [Required(ErrorMessage = "Du må oppgi avreise")]
        [Display(Name = "Fra")]
        public string FromAirportID { get; set; }

    }
}