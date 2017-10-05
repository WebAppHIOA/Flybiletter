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
        public string Email { get; set; }
        public string Price { get; set; }
        public string PassengerCount { get; set; }
        public Trip Trip { get; set; }

        public virtual List<Customer> Customer { get; set; }
    }
}