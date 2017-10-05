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
                    Date = departure.Date,
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


        public Boolean AddCustomer(Customer passenger)
        {
            using (var db = new AirportContext())
            {
                Customer insertCustomer = new Customer
                {
                    CustomerId = passenger.CustomerId,
                    Firstname = passenger.Firstname,
                    Surname = passenger.Surname,
                    Tlf = passenger.Tlf,
                    Email = passenger.Email
                };

                var order = db.Order.Where(o => o.OrderNumber == passenger.Order.OrderNumber).First();
                order.Customer.Add(insertCustomer);

                var departure = db.Departure.Where(d => d.FlightId == passenger.Departure.FlightId).First();
                departure.Passenger.Add(insertCustomer);

                db.SaveChanges();

                return true;
            }

        }

   
        /* Ligger for øyeblikket i HomeController i stedenfor
        public string UniqueReference()
        {
            var guid = System.Guid.NewGuid().ToString();

            return guid;
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