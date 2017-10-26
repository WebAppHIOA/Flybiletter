using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using System.Text.RegularExpressions;

namespace BLL
{
    public class Administrator
    {
        public bool DeleteAirport(string id)
        {
            var db = new DB();
            db.DeleteAirport(id);
            return true;
        }

        public bool DeleteDeparture(string id)
        {
            var db = new DB();
            db.DeleteDeparture(id);
            return true;
        }

        public bool DeleteOrder(string id)
        {
            var db = new DB();
            db.DeleteOrder(id);
            return true;
        }

        public bool UpdateAirport(Airport airport)
        {
            var db = new DB();
          
            db.UpdateAirport(airport);
            return true;
        }

        public bool UpdateDeparture(Departure departure)
        {
            var db = new DB();
            
            db.UpdateDeparture(departure);
            return true;
        }

        public bool UpdateOrder(Model.Order order)
        {
            var db = new DB();
            db.UpdateOrder(order);
            return true;
        }

        public Dictionary<string, int> TableCounts()
        {
            var db = new DB();

            var count = new Dictionary<string, int>();

            count.Add("Departure", db.OrderCount());
            count.Add("Order", db.DepartureCount());
            count.Add("Airport", db.AirportCount());
            
            return count;
        }

        public List<Airport> GetAllAirports()
        {
            var db = new DB();
            var allAirports = db.getAllAirports();
            return allAirports;
        }

        public List<Departure> GetAllDepartures()
        {
            var db = new DB();
            var allDepartures = db.getAllDepartures();
            return allDepartures;
        }

        public List<Model.Order> GetAllOrders()
        {
            var db = new DB();
            var allOrders = db.getAllOrders();
            return allOrders;
        }

        public Departure GetDeparture(string id)
        {
            var db = new DB();

            String allowed = @"[SK]+\d{6}$";
            if (id != null) {
                if (Regex.IsMatch(id, allowed)) return db.FindDeparture(id);
                else return null;
            }
            else return null;
        }

        public Airport GetAirport(string id)
        {
            var db = new DB();

            return db.FindAirport(id);
        }

        public Model.Order GetOrder(string id)
        {
            var db = new DB();

            return db.FindOrder(id);
        }
    }
}
