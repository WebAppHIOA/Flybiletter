using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Login
    {
        [Display(Name = "Brukernavn")]
        [Required(ErrorMessage = "Brukernavn må oppgis")]
        public string Username { get; set; }

        [Display(Name = "Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    
    public class User
    {
        [Key]
        public string Username { get; set; }
        public byte[] Password { get; set; }
    }
}
