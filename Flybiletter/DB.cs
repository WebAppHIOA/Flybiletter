using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Flybiletter.Models;
using System.Text;

namespace Flybiletter
{

    public static class DB
    {

        public static List<Airport> getAllAirports()
        {
            using (var db = new AirportContext())
            {
                List<Airport> allAirports = (from a in db.Airport
                                             select a).ToList();
                return allAirports;
            }
        }


        public static Boolean AddOrder(Order order)
        {
            using (var db = new AirportContext())
            {
                var departure = db.Departure.Where(d => d.FlightId == order.Departure.FlightId).First();
                departure.Order.Add(new Order
                {
                    OrderNumber = order.OrderNumber,
                    Date = order.Date,
                    Firstname = order.Firstname,
                    Surname = order.Surname,
                    Tlf = order.Tlf,
                    Email = order.Email,
                    Price = order.Price,
                });

                db.SaveChanges();

                return true;
            }
        }


        public static Order FindOrder(string id)
        {
            using (var db = new AirportContext())
            {
                var order = db.Order.First(o => o.OrderNumber == id);
                return order;
            }
        }

        public static Airport FindAirport(string id)
        {
            using (var db = new AirportContext())
            {
                var airport = db.Airport.First(a => a.AirportId == id);
                return airport;
            }
        }

        public static Boolean AddDeparture(Departure departure)
        {
            using (var db = new AirportContext())
            {

                var airport = db.Airport.Where(c => c.AirportId == departure.Airport.AirportId).First();
                airport.Departure.Add(new Departure
                {
                    FlightId = departure.FlightId,
                    From = departure.From,
                    To = departure.To,
                    Date = departure.Date,
                    DepartureTime = departure.DepartureTime,
                });

                db.SaveChanges();

                return true;
            }
        }


        public static Departure FindDeparture(string id)
        {
            using (var db = new AirportContext())
            {
                var departure = db.Departure.First(d => d.FlightId == id);
                return departure;
            }
        }

        public static Boolean IsFlightIdAvailable(string toTest)
        {
            using (var db = new AirportContext())
            {
                var available = db.Departure.Any(row => row.FlightId == toTest);
                if (available)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}