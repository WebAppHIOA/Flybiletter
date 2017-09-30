using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public class Passasjer
    {
   	public string PassasjerID { get; set; }

        [Required(ErrorMessage = "Fornavn må oppgis")]
        public string Fornavn { get; set; }

        [Required(ErrorMessage = "Etternavn må oppgis")]
        public string Etternavn { get; set; }

        [Required(ErrorMessage = "Nasjonalitet må oppgis")]
        public string Nasjonalitet { get; set; }

        [Required(ErrorMessage = "Telefonnummer må oppgis")]
        [RegularExpression(@"[0-9]{8}", ErrorMessage="Telefonnummer må være 8 siffer")]
        public int tlf { get; set; }

        [Required(ErrorMessage = "E-post adresse må oppgis")]
        public string email { get; set; }
        public virtual Bestilling bestilling { get; set; }
        public virtual List<PassasjerAvgang> passasjerAvgang { get; set; }
    }
}