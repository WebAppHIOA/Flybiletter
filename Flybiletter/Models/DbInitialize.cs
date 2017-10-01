using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public class DbInitialize : CreateDatabaseIfNotExists<AirportContext>
    {
        protected override void Seed(AirportContext context)
        {
            List<Airport> allAirports = new List<Airport>();

           allAirports.Add(new Airport
           {
                FlyplassId = "AMS",
                Navn = "Amsterdam Schipol International Airport",
                By = "Amsterdam",
                Land = "Netherlands",
                Kontinent = "Europe",
                Avgift = "32.79"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "ATL",
                Navn = "Hartsfield Jackson International Airport",
                By = "Atlanta",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "45.12"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "BCN",
                Navn = "Barcelona International Airport",
                By = "Barcelona",
                Land = "Spain",
                Kontinent = "Europe",
                Avgift = "35.33"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "BKK",
                Navn = "Suvarnabhumi International Airport",
                By = "Bangkok",
                Land = "Thailand",
                Kontinent = "Asia",
                Avgift = "26.96"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "BOM",
                Navn = "Chhatrapati Shivaji International Airport",
                By = "Mumbai",
                Land = "India",
                Kontinent = "Asia",
                Avgift = "21.74"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "CAN",
                Navn = "Guangzhou Baiyun International Airport",
                By = "Guangzhou",
                Land = "China",
                Kontinent = "Asia",
                Avgift = "37.89"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "CDG",
                Navn = "Charles de Gaulle International Airport",
                By = "Paris",
                Land = "France",
                Kontinent = "Europe",
                Avgift = "39.11"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "CKG",
                Navn = "Soekarno-Hatta International Airport",
                By = "Jakarta",
                Land = "Indonesia",
                Kontinent = "Asia",
                Avgift = "19.75"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "CTU",
                Navn = "Chengdu Shuuangliu International Airport",
                By = "Chengdu",
                Land = "China",
                Kontinent = "Asia",
                Avgift = "34.61"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "CLT",
                Navn = "Charlotte Douglas International Airport",
                By = "Charlotte",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "47.38"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "DEL",
                Navn = "Indira Ghandi International Airport",
                By = "New Dehli",
                Land = "India",
                Kontinent = "Asia",
                Avgift = "21.99"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "DEN",
                Navn = "Denver International Airport",
                By = "Denver",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "41.94"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "DFW",
                Navn = "Dallas Fort Worth International Airport",
                By = "Dallas",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "39.87"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "DXB",
                Navn = "Dubai International Airport",
                By = "Dubai",
                Land = "United Arab Emirates",
                Kontinent = "Asia",
                Avgift = "38.95"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "EWR",
                Navn = "Newark Liberty International Airport",
                By = "New York",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "45.67"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "FCO",
                Navn = "Leonardo Da Vinci International Airport",
                By = "Rome",
                Land = "Italy",
                Kontinent = "Europe",
                Avgift = "36.17"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "FRA",
                Navn = "Frankfurt am Main International Airport",
                By = "Frankfurt",
                Land = "Germany",
                Kontinent = "Europe",
                Avgift = "33.21"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "GRU",
                Navn = "Guarulhos International Airport",
                By = "Sao Paulo",
                Land = "Brazil",
                Kontinent = "South America",
                Avgift = "30.09"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "HKG",
                Navn = "Hong Kong International Airport",
                By = "Hong Kong",
                Land = "Hong Kong",
                Kontinent = "Asia",
                Avgift = "46.10"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "HND",
                Navn = "Tokyo International Airport",
                By = "Tokyo",
                Land = "Japan",
                Kontinent = "Asia",
                Avgift = "34.21"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "IAH",
                Navn = "Georgi Bush Intercontinental Houston",
                By = "Houston",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "38.45"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "ICN",
                Navn = "Incheon International Airport",
                By = "Seoul",
                Land = "South Korea",
                Kontinent = "Asia",
                Avgift = "32.55"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "IST",
                Navn = "Atatürk International Airport",
                By = "Istanbul",
                Land = "Turkey",
                Kontinent = "Europe",
                Avgift = "25.31"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "JFK",
                Navn = "John F Kennedy International Airport",
                By = "New York",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "43.17"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "KMG",
                Navn = "Kunming Changshui International Airport",
                By = "Kunming",
                Land = "China",
                Kontinent = "Asia",
                Avgift = "29.32"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "KUL",
                Navn = "Kuala Lumpur International Airport",
                By = "Kuala Lumpur",
                Land = "Malaysia",
                Kontinent = "Asia",
                Avgift = "41.76"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "LAS",
                Navn = "McCarran International Airport",
                By = "Las Vegas",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "36.66"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "LAX",
                Navn = "Los Angeles International Airport",
                By = "Los Angeles",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "43.78"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "LGW",
                Navn = "London Gatwick",
                By = "London",
                Land = "United Kingdom",
                Kontinent = "Europe",
                Avgift = "31.09"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "LHR",
                Navn = "London Heathrow",
                By = "London",
                Land = "United Kingdom",
                Kontinent = "Europe",
                Avgift = "37.11"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "MAD",
                Navn = "Madrid Barajas International Airport",
                By = "Madrid",
                Land = "Spain",
                Kontinent = "Europe",
                Avgift = "31.01"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "MCO",
                Navn = "Orlando International Airport",
                By = "Orlando",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "37.90"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "MEX",
                Navn = "Benito Juarez International Airport",
                By = "Mexico City",
                Land = "Mexico",
                Kontinent = "North America",
                Avgift = "23.53"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "MIA",
                Navn = "Miami International Airport",
                By = "Miami",
                Land = "USA",
                Kontinent = "Europe",
                Avgift = "36.31"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "MNL",
                Navn = "Ninoy Aquino International Airport",
                By = "Manila",
                Land = "Philippines",
                Kontinent = "Asia",
                Avgift = "23.87"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "MSP",
                Navn = "Minneapolis-St Paul International Airport",
                By = "Minneapolis",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "35.13"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "MUC",
                Navn = "Munich International",
                By = "Munich",
                Land = "Germany",
                Kontinent = "Europe",
                Avgift = "28.90"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "NRT",
                Navn = "Narita International Airport",
                By = "Tokyo",
                Land = "Japan",
                Kontinent = "Asia",
                Avgift = "31.71"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "ORD",
                Navn = "Chicago O'Hare International Airport",
                By = "Chicago",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "33.33"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "OSL",
                Navn = "Oslo Airport Gardemoen",
                By = "Oslo",
                Land = "Norway",
                Kontinent = "Europe",
                Avgift = "36.78"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "PHX",
                Navn = "Phoenix Sky Harbor International",
                By = "Phoenix",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "32.19"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "SEA",
                Navn = "Seattle Tacoma International Airport",
                By = "Seattle",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "36.69"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "SFO",
                Navn = "San Francisco International Airport",
                By = "San Francisco",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "39.99"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "SHA",
                Navn = "Shanghai Hongqiao International Airport",
                By = "Shanghai",
                Land = "China",
                Kontinent = "Asia",
                Avgift = "38.11"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "SIN",
                Navn = "Singapore Changi International Airport",
                By = "Singapore",
                Land = "Singapore",
                Kontinent = "Asia",
                Avgift = "25.12"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "SYD",
                Navn = "Sydney Kingsford Smith International Airport",
                By = "Sydney",
                Land = "Australia",
                Kontinent = "Oseania",
                Avgift = "41.19"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "SZX",
                Navn = "Shenzhen Bao'an International Airport",
                By = "Shenzhen",
                Land = "China",
                Kontinent = "Asia",
                Avgift = "25.86"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "TPE",
                Navn = "Taoyuan International Airport",
                By = "Taiwan",
                Land = "Taipei",
                Kontinent = "Asia",
                Avgift = "31.07"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "PEK",
                Navn = "Beijing Capital International Airport",
                By = "Beijing",
                Land = "China",
                Kontinent = "Asia",
                Avgift = "30.65"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "PVG",
                Navn = "Shanghai Pudong International Airport",
                By = "Shanghai",
                Land = "China",
                Kontinent = "Asia",
                Avgift = "33.90"
            });

            allAirports.Add(new Airport
            {
                FlyplassId = "YYZ",
                Navn = "Lester B. Pearson International Airport",
                By = "Toronto",
                Land = "Canada",
                Kontinent = "North America",
                Avgift = "37.89"
            });

            foreach(Airport f in allAirports)
            {
                context.Airport.Add(f);
            }

            base.Seed(context);
        }
    }
}