using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using System.Text;
using log4net;
using System.Reflection;
using System.Data.Entity;

namespace DAL
{

    public class DB : IDB
    {

        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public byte[] CreateHash(string password) //static
        {
            byte[] dataInn, dataOut;
            var algorithm = System.Security.Cryptography.SHA512.Create();
            dataInn = System.Text.Encoding.ASCII.GetBytes(password);
            dataOut = algorithm.ComputeHash(dataInn);
            return dataOut;
        }

        public bool initiateAdmin(Login login) //static
        {
            using (var db = new AirportContext())
            {
                byte[] usersPass = CreateHash(login.Password);
                User findUser = db.User.FirstOrDefault(
                    b => b.Password == usersPass && b.Username == login.Username);
                if (findUser == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public List<Airport> getAllAirports()
        {
            using (var db = new AirportContext())
            {
                List<Airport> allAirports = (from a in db.Airport
                                             select a).ToList();
                return allAirports;
            }
        }

        public List<Departure> getAllDepartures()
        {
            using (var db = new AirportContext()) {
                List<Departure> allDepartures = (from d in db.Departure
                                                 select d).ToList();
                return allDepartures;
        }
        }

        public List<Order> getAllOrders()
        {
            using(var db = new AirportContext())
            {
                List<Order> allOrders = (from o in db.Order
                                         select o).ToList();
                return allOrders;
            }
        }

        /* Deletes an airport and sets the foreign key value to null
         * 
         */
        public bool DeleteAirport(string id)
        {
            using (var db = new AirportContext())
            {
                try
                {

                    var airport = db.Airport.Include("Departure").FirstOrDefault(a => (a.AirportId == id));

                    db.Airport.Remove(airport);
                    db.SaveChanges();

                        return true;
                    
                }
                catch (Exception e)
                {
                    log.Error("Delete airport from database " + e);
                    return false;
                }
            }
        }

        /* Deletes a departure and sets the foreign key value to null
         * 
         */
        public bool DeleteDeparture(string id)
        {
            using (var db = new AirportContext())
            {
                try
                {
                    var departure = db.Departure.Include("Order").FirstOrDefault(a => (a.FlightId == id));

                    db.Departure.Remove(departure);
                    db.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    log.Error("Delete departure from database \n" + e);
                    return false;
                }
            }
        }

        /* Deletes an order
         * 
         */
        public bool DeleteOrder(string id)
        {
            using (var db = new AirportContext())
            {
                try
                {
                    var order = db.Order.Single(o => (o.OrderNumber == id));
                    db.Order.Remove(order);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    log.Error("Delete order from database \n" + e);
                    return false;
                }
            }
        }

        public bool AddOrder(Order order)
        {
            using (var db = new AirportContext())
            {
                try
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
                catch (Exception e)
                {
                    log.Error("Failed to add order to database \n" + e);
                    return false;
                }
            }
        }


        public bool AddAirport(Airport airport)
        {
            using (var db = new AirportContext())
            {
                try
                {
                    db.Airport.Add(airport);

                    return true;
                }
                catch (Exception e)
                {
                    log.Error("Failed to add departure to database \n" + e);
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

        public Airport FindAirport(string id)
        {
            using (var db = new AirportContext())
            {
                var airport = db.Airport.First(a => a.AirportId == id);
                return airport;
            }
        }

        public Boolean AddDeparture(Departure departure)
        {
            using (var db = new AirportContext())
            {
                try
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
                catch (Exception e)
                {
                    log.Error("Failed to add departure to database \n" + e);
                    return false;
                }
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

        /* Updates one table row, null values in incoming object just means the value remains the same. Other values are changed
         * 
         */
        public bool UpdateAirport(Airport changes)
        {
            using (var db = new AirportContext())
            {
                try
                {
                    var airport = db.Airport.First(row => row.AirportId == changes.AirportId);
              
                    airport.Name = changes.Name;
                    airport.City = changes.City;
                    airport.Country = changes.Country;
                    airport.Fee = changes.Fee;

                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    log.Error("Failed to update airport \n" + e);
                    return false;
                }
            }
        }

        /* Updates one table row, null values in incoming object just means the value remains the same. Other values are changed
         * 
         */
        public bool UpdateDeparture(Departure changes)
        {
            using (var db = new AirportContext())
            {
                try
                {
                    var departure = db.Departure.First(row => row.FlightId == changes.FlightId);
     
                    departure.From = changes.From;
                    departure.To = changes.To;
                    departure.Date = changes.Date;
                    departure.DepartureTime = changes.DepartureTime;

                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    log.Error("Failed to update departure \n" + e);
                    return false;
                }
            }
        }

        /* Updates one table row, null values in incoming object just means the value remains the same. Other values are changed
         * 
         */
        public bool UpdateOrder(Order changes)
        {
            using (var db = new AirportContext())
            {
                try
                {
                    var order = db.Order.First(row => row.OrderNumber == changes.OrderNumber);
                   
                    order.Firstname = changes.Firstname;
                    order.Surname = changes.Surname;
                    order.Tlf = changes.Tlf;
                    order.Email = changes.Email;
                    order.Price = changes.Price;
                    order.Cancelled = changes.Cancelled;

                    db.SaveChanges();
                    return false;
                }
                catch (Exception e)
                {
                    log.Error("Failed to update order \n" + e);
                    return false;
                }
            }
        }

        /* Counts all registered Airports
         * 
         */
        public int AirportCount()
        {
            using (var db = new AirportContext())
            {
                var total = db.Airport.Count();
                return total;
            }
        }

        /* Counts all registered Departures
        * 
        */
        public int DepartureCount()
        {
            using (var db = new AirportContext())
            {
                var total = db.Departure.Count();
                return total;
            }
        }

        /* Counts all registered Orders
         * 
         */
        public int OrderCount()
        {
            using (var db = new AirportContext())
            {
                var total = db.Departure.Count();
                return total;
            }
        }


        public Boolean IsFlightIdAvailable(string toTest)
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

        public Invoice getInvoiceInformation(string flightID, string orderNo)
        {

            using (var db = new AirportContext())
            {
          
                var invoice = (from order in db.Order
                               join departure in db.Departure
                               on order.Departure.FlightId equals departure.FlightId
                               where order.OrderNumber == orderNo
                               select new Invoice
                               {
                                   OrderReferance = order.OrderNumber,
                                   Date = order.Date,
                                   From = departure.From,
                                   Destination = departure.To,
                                   Price = order.Price,
                                   Email = order.Email
                               }).First();
                return invoice;
            }
        }
    }
}