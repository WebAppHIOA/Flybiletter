using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    class LoginBLL
    {

        IDB _DB;
        public LoginBLL()
        {
            _DB = new DB();
        }

        public LoginBLL(IDB stub)
        {
            _DB = stub;
        }


    }
}
