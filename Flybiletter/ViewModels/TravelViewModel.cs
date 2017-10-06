using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Flybiletter.Models;

namespace Flybiletter.ViewModels
{
    public class TravelViewModel
    {
        [Required]
        public String SessionID { get; set; }

        [Required]
        public List<Airport> From {
            get;
            set;
        }

        [Required]
        public List<Airport> To { get; set; }

        [Required]
        public DateTime Date { get; set; }

    }
}