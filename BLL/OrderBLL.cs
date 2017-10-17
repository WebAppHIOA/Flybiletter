using System;
using System.Collections.Generic;
using Model;
using DAL;
using System.Web;

namespace BLL
{
    public class OrderBLL
    {
        public List<Airport> getAllAirports()
        {
            var airports = DB.getAllAirports();
 
            return airports;
        }

        public string GenerateFlightID()
        {
            var flightId = Model.GenerateDepartures.GenerateFlightId();
            if (DB.IsFlightIdAvailable(flightId))
            {
                return Model.GenerateDepartures.GenerateFlightId();
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
            var airport = DB.FindAirport(destination);
            return airport;
        }

        public void AddDeparture(Departure departure)
        {
            DB.AddDeparture(departure);
        }

        public void AddOrder(Order order)
        {
            DB.AddOrder(order);
        }

        public Invoice GetInvoiceInformation(string flightId, string orderNumber)
        {
           return DB.getInvoiceInformation(flightId, orderNumber);
        }

    }
}
