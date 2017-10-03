using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Flybiletter.Models;

namespace Flybiletter
{
    /** Klassen skal kanskje ikke være med senere
     * 
     * 
     * */
    public class DB
    {
    
        
        public List<Airport> getAllAirports()
        {
            using (var db = new AirportContext())
            {
                List<Airport> allAirports = (from a in db.Airport
                                                 select a).ToList();
                return allAirports;
            }
        }

    }
   
}