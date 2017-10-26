using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class Administrator
    {
        IDB _DB;
        public Administrator()
        {
            _DB = new DB();
        }

        public Administrator(IDB stub)
        {
            _DB = stub;
        }
        public bool DeleteAirport(string id)
        {
            _DB.DeleteAirport(id);
            return true;
        }

        public bool DeleteDeparture(string id)
        {
            _DB.DeleteDeparture(id);
            return true;
        }

        public bool UpdateAirport(Airport airport)
        {
            _DB.UpdateAirport(airport);
            return true;

        }
    }
}
