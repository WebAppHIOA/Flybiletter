using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AdminOrderViewModel
    {

        public string OrderNumber { get; set; }
        public string Date { get; set; }

        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        public string Firstname { get; set; }

        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        public string Surname { get; set; }

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Telefonnummer må oppgis")]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = "Vennligst skriv en gyldig telefonnummer")]
        [DataType(DataType.PhoneNumber)]
        public string Tlf { get; set; }

        [Display(Name = "E-post")]
        [Required(ErrorMessage = "E-post adresse må oppgis")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Vennligst skriv en gyldig e-post")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Price { get; set; }

        public bool Cancelled { get; set; }
        public string FlightId { get; set; }

        public virtual List<Departure> Departure { get; set; }
        public virtual List<Order> Order { get; set; }
    }
}
