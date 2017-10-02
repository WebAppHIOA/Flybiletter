using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Flybiletter.Models;

namespace Flybiletter
{
    public class DB
    {
        // Må avgang listen legges til?
        public List<Airport> getAllAirports()
        {
            using (var db = new AirportContext())
            {
                /*
                List<Airport> allAirports = db.Airport.Select(a => new Airport
                {
                    AirportId = a.AirportId,
                    Name = a.Name,
                    City = a.City,
                    Country = a.Country,
                    Continent = a.Continent,
                    Fee = a.Fee,
                    Avgang = a.Avgang
                }).ToList();
                return allAirports;
                */
                List<Airport> allAirports = (from a in db.Airport
                                                 select a).ToList();
                return allAirports;
            }
        }

    }
}