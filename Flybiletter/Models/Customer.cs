using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public class Customer
    {
        [Key]
   	    public string CustomerId { get; set; }

        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        public string Firstname { get; set; }

        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        public string Surname { get; set; }

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Telefonnummer må oppgis")]
        [RegularExpression(@"[0-9]", ErrorMessage="Vennlig skriv en gyldig tlfnummer")]
        [DataType(DataType.PhoneNumber)]
        public int Tlf { get; set; }

        [Display(Name = "E-post")]
        [Required(ErrorMessage = "E-post adresse må oppgis")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Vennlig skriv en gyldig e-post")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Class { get; set; }

        // Barn eller voksen 
        public string Category { get; set; }

        public virtual Order Order { get; set; }
        public virtual Departure Departure { get; set; }
    }
}