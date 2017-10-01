using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public class Airport
    {
        public string FlyplassId { get; set; }
        public string Navn { get; set; }
        public string By { get; set; }
        public string Land { get; set; }
        public string Kontinent { get; set; }
        public string Avgift { get; set; }
        public virtual List<Avgang> Avgang { get; set; }
    }
}