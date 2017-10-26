using System;
using System.Collections.Generic;
using Model;
using DAL;
using System.Web;

namespace BLL
{
    public class Order
    {
        public List<Airport> getAllAirports()
        {
            var db = new DB();
            var airports = db.getAllAirports();
 
            return airports;
        }

        public string GenerateFlightID()
        {
            var db = new DB();
            var flightId = GenerateDepartures.GenerateFlightId();
            if (db.IsFlightIdAvailable(flightId))
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
            var db = new DB();
            var airport = db.FindAirport(destination);
            return airport;
        }

        public void AddDeparture(Departure departure)
        {
            var db = new DB();
            db.AddDeparture(departure);
        }

        public void AddOrder(Model.Order order)
        {
            var db = new DB();
            db.AddOrder(order);
        }

        public Invoice GetInvoiceInformation(string flightId, string orderNumber)
        {
            var db = new DB();
            return db.getInvoiceInformation(flightId, orderNumber);
        }

    }
}
