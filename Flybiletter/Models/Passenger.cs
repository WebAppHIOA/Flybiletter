using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public class Passenger
    {
        [Key]
   	    public string PassengerId { get; set; }

        [Display(Name = "FORNAVN")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        public string Firstname { get; set; }

        [Display(Name = "ETTERNAVN")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        public string Surname { get; set; }

        [Display(Name = "TELEFON")]
        [Required(ErrorMessage = "Telefonnummer må oppgis")]
        [RegularExpression(@"[0-9]{8}", ErrorMessage="Telefonnummer må være 8 siffer")]
        [DataType(DataType.PhoneNumber)]
        public int Tlf { get; set; }

        [Display(Name = "E-POST")]
        [Required(ErrorMessage = "E-post adresse må oppgis")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Vennlig skriv en gyldig e-postaddresse")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //Enum? - Sondre
        public string Class { get; set; }

        // Barn eller voksen 
        public string Category { get; set; }

        public virtual Order Order { get; set; }
        public virtual Departure Departure { get; set; }
    }
}