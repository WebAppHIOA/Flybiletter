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

    public class DB
    {

        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public List<Airport> getAllAirports()
        {
            try
            {
                using (var db = new AirportContext())
                {
                    List<Airport> allAirports = (from a in db.Airport
                                                 select a).ToList();
                    return allAirports;
                }
            }
            catch(Exception e)
            {
                log.Error("Failed to get Airports" + e);
                return null;
            }
        }

        public List<Departure> getAllDepartures()
        {
            try
            {
                using (var db = new AirportContext())
                {
                    List<Departure> allDepartures = (from d in db.Departure
                                                     select d).ToList();
                    return allDepartures;

                }
            }
            catch (Exception e)
            {
                log.Error("Failed to get Departures" + e);
                return null;
            }
        }

        public List<Order> getAllOrders()
        {
            try
            {
                using (var db = new AirportContext())
                {
                    List<Order> allOrders = (from o in db.Order
                                             select o).ToList();
                    return allOrders;
                }
            }
            catch (Exception e)
            {
                log.Error("Failed to get orders" + e);
                return null;
            }
        }

        public bool DeleteAirport(string id)
        {
            try
            {
                using (var db = new AirportContext())
                {

                    CancelDeparture(id);

                    var airport = db.Airport.Include("Departure").FirstOrDefault(a => (a.AirportId == id));

                    db.Airport.Remove(airport);
                    db.SaveChanges();

                    return true;

                }
            }
            catch (Exception e)
            {
                log.Error("Delete airport from database " + e);
                return false;
            }
        }

        public bool CancelDeparture(string id)
        {
            try
            {
                using (var db = new AirportContext())
                {

                    var cancel = db.Departure.Where(d => d.Airport.AirportId == id).ToList();
                    cancel.ForEach(c => c.Cancelled = true);
                    db.SaveChanges();

                    CancelOrder(cancel);
                    return true;

                }
            }
            catch (Exception e)
            {
                log.Error("Cancel departure" + e);
                return false;
            
            }
        }

        /* Overload 1
         * 
         */
        public bool CancelOrder(List<Departure> id)
        {
            try
            {
                using (var db = new AirportContext())
                {

                    foreach (var departure in id)
                    {
                        var cancel = db.Order.Where(d => d.Departure.FlightId == departure.FlightId).ToList();
                        cancel.ForEach(c => c.Cancelled = true);

                    }

                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                log.Error("Cancel order list" + e);
                return false;
            }
        }

        /* Overload 2
         * 
         */
        public bool CancelOrder(Departure departure)
        {
            try
            {
                using (var db = new AirportContext())
                {

                    var cancel = db.Order.Where(d => d.Departure.FlightId == departure.FlightId).ToList();
                    cancel.ForEach(c => c.Cancelled = departure.Cancelled);

                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                log.Error("Cancel order string" + e);
                return false;
            }
        }

        public bool DeleteDeparture(string id)
        {
            try
            {
                using (var db = new AirportContext())
                {


                    var departure = db.Departure.Include("Order").FirstOrDefault(a => (a.FlightId == id));

                    CancelOrder(departure);

                    db.Departure.Remove(departure);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                log.Error("Delete departure from database" + e);
                return false;
            
            }
        }

        public bool DeleteOrder(string id)
        {
            try
            {
                using (var db = new AirportContext())
                {

                    var order = db.Order.Single(o => (o.OrderNumber == id));
                    db.Order.Remove(order);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                log.Error("Delete order from database" + e);
                return false;
            }
        }

        public bool AddOrder(Order order)
        {
            try
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
            catch (Exception e)
            {
                log.Error("Failed to add order to database" + e);
                return false;
            }
        }


        public bool AddAirport(Airport airport)
        {
            try
            {
                using (var db = new AirportContext())
                {

                    db.Airport.Add(airport);

                    return true;
                }
            }
            catch (Exception e)
            {
                log.Error("Failed to add departure to database" + e);
                return false;
            }
        }

        public Order FindOrder(string id)
        {
            try
            {
                using (var db = new AirportContext())
                {
                    var order = db.Order.First(o => o.OrderNumber == id);
                    return order;
                }
            }
            catch (Exception e)
            {
                log.Error("Failed to get Find" + e);
                return null;
            }
        }

        public Airport FindAirport(string id)
        {
            var airport = new Airport();
            using (var db = new AirportContext())
            {
                try
                {
                    airport = db.Airport.First(a => a.AirportId == id);
                    return airport;
                }
                catch (Exception e)
                {
                    log.Error(e + " " + e.InnerException);
                    return null;
                }
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
                    log.Error("Failed to add departure to database" + e);
                    return false;
                }
            }
        }


        public Departure FindDeparture(string id)
        {
            var departure = new Departure();
            using (var db = new AirportContext())
            {
                try
                {

                    departure = db.Departure.First(d => d.FlightId == id);
                    return departure;
                }

                catch (Exception e)
                {
                    log.Error("Failed to find departure" + e);
                    return departure;
                }
            }
        }


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
                    airport.Continent = changes.Continent;
                    airport.Fee = changes.Fee;

                    db.SaveChanges();

                    return true;

                }
                catch (Exception e)
                {
                    log.Error("Failed to update airport" + e);
                    return false;
                }
            }
        }

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
                    departure.Cancelled = changes.Cancelled;

                    db.SaveChanges();

                    CancelOrder(changes);

                    return true;
                }
                catch (Exception e)
                {
                    log.Error("Failed to update departure" + e);
                    return false;
                }
            }
        }


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
                    log.Error("Failed to update order" + e);
                    return false;
                }
            }
        }


        public int AirportCount()
        {

            using (var db = new AirportContext())
            {
                try
                {
                    var total = db.Airport.Count();
                    return total;
                }
                catch (Exception e)
                {
                    log.Error("Failed to count Airports" + e);
                    return -1;
                }
            }
    }


        public int DepartureCount()
        {
            using (var db = new AirportContext())
            {
                try { 
                var total = db.Departure.Count();
                return total;
                }
                catch (Exception e)
                {
                    log.Error("Failed to count Departures" + e);
                    return -1;
                }
            }
    }

        public int OrderCount()
        {

            using (var db = new AirportContext())
            {
                try
                {
                    var total = db.Departure.Count();
                    return total;
                }
                catch (Exception e)
                {
                    log.Error("Failed to count Orders" + e);
                    return -1;
                }
            }
        }


        public Boolean IsFlightIdAvailable(string toTest)
        {

            using (var db = new AirportContext())
            {
                try
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
                catch (Exception e)
                {
                    log.Error("Failed to check for available flightID" + e);
                    return false;
                }
            }
            }

        public Invoice getInvoiceInformation(string flightID, string orderNo)
        {

                using (var db = new AirportContext())
                {
                var invoice = new Invoice();
                try
                {
                   invoice = (from order in db.Order
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
                }
                catch (Exception e)
                {
                    log.Error("Failed to get invoice information" + e);
                }
                return invoice;
        }
            
        }

     
    }
}