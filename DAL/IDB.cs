using System.Collections.Generic;
using Model;

namespace DAL
{
    public interface IDB
    {
        bool AddAirport(Airport airport);
        bool AddDeparture(Departure departure);
        bool AddOrder(Order order);
        int AirportCount();
        bool DeleteAirport(string id);
        bool DeleteDeparture(string id);
        bool DeleteOrder(string id);
        int DepartureCount();
        Airport FindAirport(string id);
        Departure FindDeparture(string id);
        Order FindOrder(string id);
        List<Airport> getAllAirports();
        List<Departure> getAllDepartures();
        List<Order> getAllOrders();
        Invoice getInvoiceInformation(string flightID, string orderNo);
        bool IsFlightIdAvailable(string toTest);
        int OrderCount();
        bool UpdateAirport(Airport changes);
        bool UpdateDeparture(Departure changes);
        bool UpdateOrder(Order changes);
    }
}