using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Model;

namespace Flybiletter.Models
{
    public class LoginModel 
    {
        [Required(ErrorMessage="Vennligst skriv brukernavn")]
        public String Username { get; set; }

        [Required(ErrorMessage="Vennligst skriv passord")]
        public String Password { get; set; }
    }
}