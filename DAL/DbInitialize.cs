using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model;
using System.Text;

namespace DAL
{
    public class DbInitialize : DropCreateDatabaseAlways<AirportContext>
    {
        protected override void Seed(AirportContext context)
        {
            List<Airport> allAirports = new List<Airport>();
            var pass = DB.CreateHash("k1g2s3a4");

            var admin = new User
            {
                Username = "airzureadmin",
                Password = pass
            };

            var order1 = new Order
            {
                OrderNumber = GenerateInvoice.UniqueReference(),
                Date = "17.10.2017",
                Firstname = "Tor",
                Surname = "Krattebø",
                Tlf = "12345678",
                Email = "tor.krattebol@hioa.no",
                Price = "3999"
            };

            var order2 = new Order
            {
                OrderNumber = GenerateInvoice.UniqueReference(),
                Date = "17.10.2017",
                Firstname = "Tormine",
                Surname = "Krattebø",
                Tlf = "12345678",
                Email = "test@gmail.com",
                Price = "3400"
            };

            var orderList = new List<Order>();
            orderList.Add(order1);
            orderList.Add(order2);

            var departure1 = new Departure
            {
                FlightId = "SK323234",
                From = "CAN",
                To = "OSL",
                Date = "29.10.2017",
                DepartureTime = "13:10:00",
                Order = orderList
            };

            var departures = new List<Departure>();
            departures.Add(departure1);


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

            allAirports.Add(new Airport
            {
                AirportId = "ATL",
                Name = "Hartsfield Jackson International Airport",
                City = "Atlanta",
                Country = "USA",
                Continent = "North America",
                Fee = "45.12",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "BCN",
                Name = "Barcelona International Airport",
                City = "Barcelona",
                Country = "Spain",
                Continent = "Europe",
                Fee = "35.33",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "BKK",
                Name = "Suvarnabhumi International Airport",
                City = "Bangkok",
                Country = "Thailand",
                Continent = "Asia",
                Fee = "26.96",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "BOM",
                Name = "Chhatrapati Shivaji International Airport",
                City = "Mumbai",
                Country = "India",
                Continent = "Asia",
                Fee = "21.74",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "CAN",
                Name = "Guangzhou Baiyun International Airport",
                City = "Guangzhou",
                Country = "China",
                Continent = "Asia",
                Fee = "37.89",
                Departure = departures
            });

            allAirports.Add(new Airport
            {
                AirportId = "CDG",
                Name = "Charles de Gaulle International Airport",
                City = "Paris",
                Country = "France",
                Continent = "Europe",
                Fee = "39.11",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "CKG",
                Name = "Soekarno-Hatta International Airport",
                City = "Jakarta",
                Country = "Indonesia",
                Continent = "Asia",
                Fee = "19.75",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "CTU",
                Name = "Chengdu Shuuangliu International Airport",
                City = "Chengdu",
                Country = "China",
                Continent = "Asia",
                Fee = "34.61",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "CLT",
                Name = "Charlotte Douglas International Airport",
                City = "Charlotte",
                Country = "USA",
                Continent = "North America",
                Fee = "47.38",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "DEL",
                Name = "Indira Ghandi International Airport",
                City = "New Dehli",
                Country = "India",
                Continent = "Asia",
                Fee = "21.99",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "DEN",
                Name = "Denver International Airport",
                City = "Denver",
                Country = "USA",
                Continent = "North America",
                Fee = "41.94",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "DFW",
                Name = "Dallas Fort Worth International Airport",
                City = "Dallas",
                Country = "USA",
                Continent = "North America",
                Fee = "39.87",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "DXB",
                Name = "Dubai International Airport",
                City = "Dubai",
                Country = "United Arab Emirates",
                Continent = "Asia",
                Fee = "38.95",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "EWR",
                Name = "Newark Liberty International Airport",
                City = "New York",
                Country = "USA",
                Continent = "North America",
                Fee = "45.67",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "FCO",
                Name = "Leonardo Da Vinci International Airport",
                City = "Rome",
                Country = "Italy",
                Continent = "Europe",
                Fee = "36.17",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "FRA",
                Name = "Frankfurt am Main International Airport",
                City = "Frankfurt",
                Country = "Germany",
                Continent = "Europe",
                Fee = "33.21",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "GRU",
                Name = "Guarulhos International Airport",
                City = "Sao Paulo",
                Country = "Brazil",
                Continent = "South America",
                Fee = "30.09",
                Departure = new List<Departure>()

            });

            allAirports.Add(new Airport
            {
                AirportId = "HKG",
                Name = "Hong Kong International Airport",
                City = "Hong Kong",
                Country = "Hong Kong",
                Continent = "Asia",
                Fee = "46.10",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "HND",
                Name = "Tokyo International Airport",
                City = "Tokyo",
                Country = "Japan",
                Continent = "Asia",
                Fee = "34.21",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "IAH",
                Name = "Georgi Bush Intercontinental Houston",
                City = "Houston",
                Country = "USA",
                Continent = "North America",
                Fee = "38.45",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "ICN",
                Name = "Incheon International Airport",
                City = "Seoul",
                Country = "South Korea",
                Continent = "Asia",
                Fee = "32.55",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "IST",
                Name = "Atatürk International Airport",
                City = "Istanbul",
                Country = "Turkey",
                Continent = "Europe",
                Fee = "25.31",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "JFK",
                Name = "John F Kennedy International Airport",
                City = "New York",
                Country = "USA",
                Continent = "North America",
                Fee = "43.17",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "KMG",
                Name = "Kunming Changshui International Airport",
                City = "Kunming",
                Country = "China",
                Continent = "Asia",
                Fee = "29.32",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "KUL",
                Name = "Kuala Lumpur International Airport",
                City = "Kuala Lumpur",
                Country = "Malaysia",
                Continent = "Asia",
                Fee = "41.76",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "LAS",
                Name = "McCarran International Airport",
                City = "Las Vegas",
                Country = "USA",
                Continent = "North America",
                Fee = "36.66",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "LAX",
                Name = "Los Angeles International Airport",
                City = "Los Angeles",
                Country = "USA",
                Continent = "North America",
                Fee = "43.78",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "LGW",
                Name = "London Gatwick",
                City = "London",
                Country = "United Kingdom",
                Continent = "Europe",
                Fee = "31.09",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "LHR",
                Name = "London Heathrow",
                City = "London",
                Country = "United Kingdom",
                Continent = "Europe",
                Fee = "37.11",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "MAD",
                Name = "Madrid Barajas International Airport",
                City = "Madrid",
                Country = "Spain",
                Continent = "Europe",
                Fee = "31.01",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "MCO",
                Name = "Orlando International Airport",
                City = "Orlando",
                Country = "USA",
                Continent = "North America",
                Fee = "37.90",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "MEX",
                Name = "Benito Juarez International Airport",
                City = "Mexico City",
                Country = "Mexico",
                Continent = "North America",
                Fee = "23.53",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "MIA",
                Name = "Miami International Airport",
                City = "Miami",
                Country = "USA",
                Continent = "Europe",
                Fee = "36.31",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "MNL",
                Name = "Ninoy Aquino International Airport",
                City = "Manila",
                Country = "Philippines",
                Continent = "Asia",
                Fee = "23.87",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "MSP",
                Name = "Minneapolis-St Paul International Airport",
                City = "Minneapolis",
                Country = "USA",
                Continent = "North America",
                Fee = "35.13",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "MUC",
                Name = "Munich International",
                City = "Munich",
                Country = "Germany",
                Continent = "Europe",
                Fee = "28.90",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "NRT",
                Name = "Narita International Airport",
                City = "Tokyo",
                Country = "Japan",
                Continent = "Asia",
                Fee = "31.71",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "ORD",
                Name = "Chicago O'Hare International Airport",
                City = "Chicago",
                Country = "USA",
                Continent = "North America",
                Fee = "33.33",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "OSL",
                Name = "Oslo Airport Gardemoen",
                City = "Oslo",
                Country = "Norway",
                Continent = "Europe",
                Fee = "36.78",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "PHX",
                Name = "Phoenix Sky Harbor International",
                City = "Phoenix",
                Country = "USA",
                Continent = "North America",
                Fee = "32.19",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "SEA",
                Name = "Seattle Tacoma International Airport",
                City = "Seattle",
                Country = "USA",
                Continent = "North America",
                Fee = "36.69",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "SFO",
                Name = "San Francisco International Airport",
                City = "San Francisco",
                Country = "USA",
                Continent = "North America",
                Fee = "39.99",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "SHA",
                Name = "Shanghai Hongqiao International Airport",
                City = "Shanghai",
                Country = "China",
                Continent = "Asia",
                Fee = "38.11",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "SIN",
                Name = "Singapore Changi International Airport",
                City = "Singapore",
                Country = "Singapore",
                Continent = "Asia",
                Fee = "25.12",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "SYD",
                Name = "Sydney Kingsford Smith International Airport",
                City = "Sydney",
                Country = "Australia",
                Continent = "Oseania",
                Fee = "41.19",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "SZX",
                Name = "Shenzhen Bao'an International Airport",
                City = "Shenzhen",
                Country = "China",
                Continent = "Asia",
                Fee = "25.86",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "TPE",
                Name = "Taoyuan International Airport",
                City = "Taiwan",
                Country = "Taipei",
                Continent = "Asia",
                Fee = "31.07",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "PEK",
                Name = "Beijing Capital International Airport",
                City = "Beijing",
                Country = "China",
                Continent = "Asia",
                Fee = "30.65",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "PVG",
                Name = "Shanghai Pudong International Airport",
                City = "Shanghai",
                Country = "China",
                Continent = "Asia",
                Fee = "33.90",
                Departure = new List<Departure>()
            });

            allAirports.Add(new Airport
            {
                AirportId = "YYZ",
                Name = "Lester B. Pearson International Airport",
                City = "Toronto",
                Country = "Canada",
                Continent = "North America",
                Fee = "37.89",
                Departure = new List<Departure>()
            });

            context.User.Add(admin);

            foreach (Airport f in allAirports)
            {
                context.Airport.Add(f);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}