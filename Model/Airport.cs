using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Model
{
    [TrackChanges]
    public class Airport
    {

        public string AirportId { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Navn må oppgis")]
        public string Name { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "By må oppgis")]
        public string City { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Land må oppgis")]
        public string Country { get; set; }
        [Display(Name = "Continent")]
        [Required(ErrorMessage = "Kontinent må oppgis")]
        public string Continent { get; set; }
        [Display(Name = "Fee")]
        [Required(ErrorMessage = "Avgift må oppgis")]
        [RegularExpression(@"^([0-9]*|\d*\.\d{2}?\d*)$", ErrorMessage = "Vennligst skriv inn et gyldig beløp")]
        [DataType(DataType.Currency)]
        public string Fee { get; set; }
        public virtual List<Departure> Departure { get; set; }
    }
}