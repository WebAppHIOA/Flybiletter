using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public class Avgang
    {

        public string FlighId { get; set; }
        public string Flyselskap { get; set; }
        [Required(ErrorMessage = "Opprinnelig sted må oppgis")]
        public string Fra { get; set; }
        [Required(ErrorMessage = "Destinasjon må oppgis")]
        public string Til { get; set; }
        [Required(ErrorMessage = "Reisetype må velges")]
        public string ReiseType { get; set; }
        public string AvgangsTid { get; set; }
        public string Ankomst { get; set; }
        [Required(ErrorMessage = "Dato må oppgis")]
        public string Dato { get; set; }
        public string AntallBarn { get; set;}
        public string AntallVoksne { get; set; }
        //enum isteden??
        [Required(ErrorMessage = "Kabinklasse må velges")]
        public string Klasse { get; set; }
        public virtual Flyplass Flyplass { get; set; }
        public virtual List<Bestilling> bestilling { get; set; }
        public virtual List<PassasjerAvgang> passasjerAvgang { get; set; }
    }
   
    //if antallBarn==0 && antallVoksne==0, få opp advarsel


}