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
        IDB _DB;
        public Administrator()
        {
            _DB = new DB();
        }

        public Administrator(DBstub stub)
        {
            _DB = stub;
        }

        public bool GetUser(Login login)
        {
            return _DB.initiateAdmin(login);
            
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

        public bool DeleteOrder(string id)
        {
            _DB.DeleteOrder(id);
            return true;
        }

        public bool UpdateAirport(Airport airport)
        {
            _DB.UpdateAirport(airport);
            return true;
        }

        public bool UpdateDeparture(Departure departure)
        {         
            _DB.UpdateDeparture(departure);
            return true;
        }

        public bool UpdateOrder(Model.Order order)
        {
            _DB.UpdateOrder(order);
            return true;
        }

        public Dictionary<string, int> TableCounts()
        {

            var count = new Dictionary<string, int>();

            count.Add("Departure", _DB.OrderCount());
            count.Add("Order", _DB.DepartureCount());
            count.Add("Airport", _DB.AirportCount());
            
            return count;
        }

        public List<Airport> GetAllAirports()
        {
            var allAirports = _DB.getAllAirports();
            return allAirports;
        }

        public List<Departure> GetAllDepartures()
        {
            var allDepartures = _DB.getAllDepartures();
            return allDepartures;
        }

        public List<Model.Order> GetAllOrders()
        {
            var allOrders = _DB.getAllOrders();
            return allOrders;
        }

        public Departure GetDeparture(string id)
        {
            String allowed = @"[SK]+\d{6}$";
            if (id != null) {
                if (Regex.IsMatch(id, allowed)) return _DB.FindDeparture(id);
                else return null;
            }
            else return null;
        }

        public Airport GetAirport(string id)
        {
            return _DB.FindAirport(id);
        }

        public Model.Order GetOrder(string id)
        {
            return _DB.FindOrder(id);
        }

        public bool AddDeparture(AdminDepartureViewModel dep)
        {
            var db = new DB();

            Departure departure = new Departure
            {
                FlightId = GenerateDepartures.GenerateFlightId(),
                From = dep.From,
                To = dep.To,
                Date = dep.Date,
                DepartureTime = dep.DepartureTime,
                Airport = GetAirport(dep.From)
            };

            return db.AddDeparture(departure);
        }

        public bool AddAirport(Airport airport)
        {
            var db = new DB();

            Airport newAirport = new Airport
            {
                AirportId = "STO",
                Name = airport.Name,
                City = airport.City,
                Country = airport.Country,
                Continent = airport.Continent,
                Fee = airport.Fee
            };

            return db.AddAirport(newAirport);
        }

        public bool AddOrder(AdminOrderViewModel order)
        {
            var db = new DB();
            var depOrder = db.FindDeparture(order.FlightId);

            Model.Order ord = new Model.Order
            {
                OrderNumber = GenerateInvoice.UniqueReference(),
                Date = order.Date,
                Firstname = order.Firstname,
                Surname = order.Surname,
                Tlf = order.Tlf,
                Email = order.Email,
                Price = order.Price,
                Departure = depOrder
            };
            return db.AddOrder(ord);
        }
    }
    }

