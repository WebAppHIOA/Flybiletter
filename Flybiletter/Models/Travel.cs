using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public class Travel
    {
        public int Id { get; set; }

        [Required]
        public String SessionID { get; set; }

        [Required]
        public Airport From { get; set; }

        [Required]
        public Airport To { get; set; }

        [Required]
        public DateTime Date { get; set; }

    }
}