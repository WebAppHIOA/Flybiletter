using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    class DBstub : IDB
    {
        public bool AddAirport(Airport airport)
        {
            return true;
        }

        public bool AddDeparture(Departure departure)
        {
            return true;
        }

        public bool AddOrder(Order order)
        {
            return true;
        }

        public int AirportCount()
        {
            return 10;
        }

        public bool DeleteAirport(string id)
        {
            return true;
        }

        public bool DeleteDeparture(string id)
        {
            return true;
        }

        public bool DeleteOrder(string id)
        {
            return true;
        }

        public int DepartureCount()
        {
            return 10;
        }

        public Airport FindAirport(string id)
        {
            Airport airport = new Airport
            {
                AirportId = "ATL",
                Name = "Hartsfield Jackson International Airport",
                City = "Atlanta",
                Country = "USA",
                Continent = "North America",
                Fee = "45.12",
                Departure = new List<Departure>()
            };

            return airport;
        }

        public Departure FindDeparture(string id)
        {
            var order = new Order
            {
                OrderNumber = GenerateInvoice.UniqueReference(),
                Date = "17.10.2017",
                Firstname = "Tormine",
                Surname = "Krattebø",
                Tlf = "12345678",
                Email = "test@gmail.com",
                Price = "3400"
            };

            List<Order> orderList = new List<Order>();
            orderList.Add(order);

            var departure = new Departure
            {
                FlightId = "SK323234",
                From = "CAN",
                To = "OSL",
                Date = "29.10.2017",
                DepartureTime = "13:10:00",
                Order = orderList
            };

            return departure;
        }

        public Order FindOrder(string id)
        {
            var order = new Order
            {
                OrderNumber = GenerateInvoice.UniqueReference(),
                Date = "17.10.2017",
                Firstname = "Tormine",
                Surname = "Krattebø",
                Tlf = "12345678",
                Email = "test@gmail.com",
                Price = "3400"
            };

            return order;
        }

        public List<Airport> getAllAirports()
        {
            List<Airport> allAirports = new List<Airport>();

            allAirports.Add(new Airport
            {
                AirportId = "AMS",
                Name = "Amsterdam Schipol International Airport",
                City = "Amsterdam",
                Country = "Netherlands",
                Continent = "Europe",
                Fee = "32.79",
                Departure = new List<Departure>()
            });

            return allAirports;
        }

        public List<Departure> getAllDepartures()
        {
            var order = new Order
            {
                OrderNumber = GenerateInvoice.UniqueReference(),
                Date = "17.10.2017",
                Firstname = "Tormine",
                Surname = "Krattebø",
                Tlf = "12345678",
                Email = "test@gmail.com",
                Price = "3400"
            };

            List<Order> orderList = new List<Order>();
            orderList.Add(order);

            var departure = new Departure
            {
                FlightId = "SK323234",
                From = "CAN",
                To = "OSL",
                Date = "29.10.2017",
                DepartureTime = "13:10:00",
                Order = orderList
            };

            List<Departure> allDepartures = new List<Departure>();
            allDepartures.Add(departure);
            return allDepartures;
        }

        public List<Order> getAllOrders()
        {
            var order = new Order
            {
                OrderNumber = GenerateInvoice.UniqueReference(),
                Date = "17.10.2017",
                Firstname = "Tormine",
                Surname = "Krattebø",
                Tlf = "12345678",
                Email = "test@gmail.com",
                Price = "3400"
            };

            List<Order> orderList = new List<Order>();
            orderList.Add(order);

            return orderList;
        }

        public Invoice getInvoiceInformation(string flightID, string orderNo)
        {
            throw new NotImplementedException();
        }

        public bool IsFlightIdAvailable(string toTest)
        {
            throw new NotImplementedException();
        }

        public int OrderCount()
        {
            throw new NotImplementedException();
        }

        public bool UpdateAirport(Airport changes)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDeparture(Departure changes)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrder(Order changes)
        {
            throw new NotImplementedException();
        }
    }
}
