using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public class Passenger
    {
   	public string PassengerId { get; set; }

        [Required(ErrorMessage = "Fornavn må oppgis")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Etternavn må oppgis")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Telefonnummer må oppgis")]
        [RegularExpression(@"[0-9]{8}", ErrorMessage="Telefonnummer må være 8 siffer")]
        public int Tlf { get; set; }

        [Required(ErrorMessage = "E-post adresse må oppgis")]
        public string Email { get; set; }

        public string Class { get; set; }

        // Barn eller voksen 
        public string Category { get; set; }

        public virtual Order Order { get; set; }
        public virtual Departure Departure { get; set; }
    }
}