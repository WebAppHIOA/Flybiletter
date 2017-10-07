using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public enum Trip
    {
        Depart,
        Return
    }
    public class Order
    {

        [Key]
        public string OrderNumber { get; set; }
        public string Date { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Tlf { get; set; }
        public string Email { get; set; }
        public string Price { get; set; }

        public virtual Departure Departure { get; set; }
    }
}