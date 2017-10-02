using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public class Order
    {
        [Key]
        public string OrderNumber { get; set; }
        public string Date { get; set; }
        public string Price { get; set; }

        //Antall reisende 
        public string Travellers { get; set; }
        
        public string RoundTrip { get; set; }
        public virtual List<Passenger> Passenger { get; set; }
    }
}