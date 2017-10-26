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
        public bool DeleteAirport(string id)
        {
            DB.DeleteAirport(id);
            return true;
        }

        public bool DeleteDeparture(string id)
        {
            DB.DeleteDeparture(id);
            return true;
        }

        public bool UpdateAirport(Airport airport)
        {
            DB.UpdateAirport(airport);
            return true;
        }
        
        public bool UserLogin (Login login)
        {
            DB.initiateAdmin(login);
            return true;
        }
    }
}
