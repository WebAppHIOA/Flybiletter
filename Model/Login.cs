using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Login
    {
        [Key]
        public String Username { get; set; }

        public byte[] Password { get; set; }
    }
}
