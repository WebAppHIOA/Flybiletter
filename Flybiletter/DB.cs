using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Flybiletter.Models;
using System.Text;

namespace Flybiletter
{

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


        public Boolean AddOrder(Order order)
        {
            using (var db = new AirportContext())
            {
                try
                {
                    db.Order.Add(order);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public Order FindOrder(string id)
        {
            using (var db = new AirportContext())
            {
                var order = db.Order.First(o => o.OrderNumber == id);
                return order;
            }
        }


        public Boolean AddDeparture(Departure departure)
        {
            using (var db = new AirportContext())
            {

                var airport = db.Airport.Where(c => c.AirportId == departure.Airport.AirportId).First();
                airport.Departure.Add(new Departure
                {
                    FlightId = departure.FlightId,
                    From = departure.From,
                    To = departure.To,
                    Arrival = departure.Arrival,
                    DepartureTime = departure.DepartureTime,
                });


                db.SaveChanges();

                return true;
            }

        }


        public Departure FindDeparture(string id)
        {
            using (var db = new AirportContext())
            {
                var departure = db.Departure.First(d => d.FlightId == id);
                return departure;
            }
        }


        public Boolean AddPassenger(Passenger passenger)
        {
            using (var db = new AirportContext())
            {
                Passenger insertPassenger = new Passenger
                {
                    PassengerId = passenger.PassengerId,
                    Firstname = passenger.Firstname,
                    Surname = passenger.Surname,
                    Tlf = passenger.Tlf,
                    Class = passenger.Class,
                    Category = passenger.Category,
                };

                var order = db.Order.Where(o => o.OrderNumber == passenger.Order.OrderNumber).First();
                order.Passenger.Add(insertPassenger);

                var departure = db.Departure.Where(d => d.FlightId == passenger.Departure.FlightId).First();
                departure.Passenger.Add(insertPassenger);

                db.SaveChanges();

                return true;
            }

        }
/*
        public List<Departure> GenerateDepartures(String to, String from)
        {
            Random random = new Random();
            List<Departure> departures = new List<Departure>();

            var times = GenerateTimes(random.Next(8));

            foreach (string i in times)
            {
                departures.Add(new Departure
                {
                    FlightId = GenerateFlightId(),
                    From = from,
                    To = to,
                    Arrival = "TBD",
                    DepartureTime = i
                });
            }

            return departures;
        }

       
        public string[] GenerateTimes(int departures)
        {
            string[] time = new string[departures];
            Random random = new Random();

            for (int i = 0; i < departures; i++)
            {
                TimeSpan start = new TimeSpan(random.Next(4, 23), random.Next(0, 59), 0);
                time[i] = start.ToString();
            }

            Array.Sort(time);

            return time;
        }
   
        /* Ligger for øyeblikket i HomeController i stedenfor
        public string UniqueReference()
        {
            var guid = System.Guid.NewGuid().ToString();

            return guid;
        }
        

        public string GenerateFlightId()
        {
            Random random = new Random();

            StringBuilder builder = new StringBuilder();
            builder.Append("SK");
            for (int i = 0; i < 6; i++)
            {
                builder.Append(random.Next(0,9));
            }
            var flightId = builder.ToString();

            if (IsFlightIdAvailable(flightId))
            {
                return GenerateFlightId();
            }
            else
            {
                return flightId;
            }
        }
        */
        public Boolean IsFlightIdAvailable(string toTest)
        {
            using(var db = new AirportContext())
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