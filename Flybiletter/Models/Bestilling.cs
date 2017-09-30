using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public class Bestilling
    {
       
        public string Referanse { get; set; }
        public string Dato { get; set; }
        public string Pris { get; set; }
        public string Antall { get; set; }
        public string Retur { get; set; }
        public virtual List<Passasjer> passasjer { get; set; }
    }
}