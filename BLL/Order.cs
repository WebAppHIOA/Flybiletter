using System;
using System.Collections.Generic;
using Model;
using DAL;
using System.Web;

namespace BLL
{
    public class Order
    {
        IDB _DB;

        public Order()
        {
            _DB = new DB();
        }
        public Order(IDB stub)
        {
            _DB = stub;
        }

        public List<Airport> getAllAirports()
        {
            var airports = _DB.getAllAirports();
 
            return airports;
        }

        public string GenerateFlightID()
        {
            var flightId = Model.GenerateDepartures.GenerateFlightId();
            if (_DB.IsFlightIdAvailable(flightId))
            {
                return GenerateDepartures.GenerateFlightId();
            }
            else
            {
                return flightId;
            }
        }

        public List<Departure> CreateDepartures(string from, string to, string date)
        {

            List<Departure> departures = GenerateDepartures.CreateDepartures(from, to, date);
            return departures;            
        }

        public int[] GeneratePrice(int total)
        {
            return GenerateDepartures.GeneratePrice(total);
        }

        public Airport FindAirport(string destination)
        {
            var airport = _DB.FindAirport(destination);
            return airport;
        }

        public void AddDeparture(Departure departure)
        {
            _DB.AddDeparture(departure);
        }

        public void AddOrder(Model.Order order)
        {
            _DB.AddOrder(order);
        }

        public Invoice GetInvoiceInformation(string flightId, string orderNumber)
        {
           return _DB.getInvoiceInformation(flightId, orderNumber);
        }

    }
}
