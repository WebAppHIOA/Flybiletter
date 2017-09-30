using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Flybiletter.Models
{
    public class DbInitialize : DropCreateDatabaseAlways<DbContext>
    {
        protected override void Seed(DbContext context)
        {
            var AMS = new Flyplass
            {
                FlyplassId = "AMS",
                Navn = "Amsterdam Schipol",
                By = "Amsterdam",
                Land = "Netherlands",
                Kontinent = "Europe",
                Avgift = "32.79"
            };

            var ATL = new Flyplass
            {
                FlyplassId = "ATL",
                Navn = "Hartsfield Jackson International",
                By = "Atlanta",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "45.12"
            };

            var BCN = new Flyplass
            {
                FlyplassId = "BCN",
                Navn = "Barcelona International",
                By = "Barcelona",
                Land = "Spain",
                Kontinent = "Europe",
                Avgift = "35.33"
            };

            var BKK = new Flyplass
            {
                FlyplassId = "BKK",
                Navn = "Suvarnabhumi",
                By = "Bangkok",
                Land = "Thailand",
                Kontinent = "Asia",
                Avgift = "26.96"
            };

            var BOM = new Flyplass
            {
                FlyplassId = "BOM",
                Navn = "Chhatrapati Shivaji International",
                By = "Mumbai",
                Land = "India",
                Kontinent = "Asia",
                Avgift = "21.74"
            };

            var CAN = new Flyplass
            {
                FlyplassId = "CAN",
                Navn = "Guangzhou Baiyun International",
                By = "Guangzhou",
                Land = "China",
                Kontinent = "Asia",
                Avgift = "37.89"
            };

            var CDG = new Flyplass
            {
                FlyplassId = "CDG",
                Navn = "Charles de Gaulle International",
                By = "Paris",
                Land = "France",
                Kontinent = "Europe",
                Avgift = "39.11"
            };

            var CKG = new Flyplass
            {
                FlyplassId = "CKG",
                Navn = "Soekarno-Hatta International",
                By = "Jakarta",
                Land = "Indonesia",
                Kontinent = "Asia",
                Avgift = "19.75"
            };

            var CTU = new Flyplass
            {
                FlyplassId = "CTU",
                Navn = "Chengdu Shuuangliu International",
                By = "Chengdu",
                Land = "China",
                Kontinent = "Asia",
                Avgift = "34.61"
            };

            var CLT = new Flyplass
            {
                FlyplassId = "CLT",
                Navn = "Charlotte Douglas International",
                By = "Charlotte",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "47.38"
            };

            var DEL = new Flyplass
            {
                FlyplassId = "DEL",
                Navn = "Indira Ghandi International",
                By = "New Dehli",
                Land = "India",
                Kontinent = "Asia",
                Avgift = "21.99"
            };

            var DEN = new Flyplass
            {
                FlyplassId = "DEN",
                Navn = "Denver International",
                By = "Denver",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "41.94"
            };

            var DFW = new Flyplass
            {
                FlyplassId = "DFW",
                Navn = "Dallas Fort Worth International",
                By = "Dallas",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "39.87"
            };

            var DXB = new Flyplass
            {
                FlyplassId = "DXB",
                Navn = "Dubai International",
                By = "Dubai",
                Land = "United Arab Emirates",
                Kontinent = "Asia",
                Avgift = "38.95"
            };

            var EWR = new Flyplass
            {
                FlyplassId = "EWR",
                Navn = "Newark Liberty International",
                By = "New York",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "45.67"
            };

            var FCO = new Flyplass
            {
                FlyplassId = "FCO",
                Navn = "Leonardo Da Vinci International",
                By = "Rome",
                Land = "Italy",
                Kontinent = "Europe",
                Avgift = "36.17"
            };

            var FRA = new Flyplass
            {
                FlyplassId = "FRA",
                Navn = "Frankfurt am Main International",
                By = "Frankfurt",
                Land = "Germany",
                Kontinent = "Europe",
                Avgift = "33.21"
            };

            var GRU = new Flyplass
            {
                FlyplassId = "GRU",
                Navn = "Guarulhos International",
                By = "Sao Paulo",
                Land = "Brazil",
                Kontinent = "South America",
                Avgift = "30.09"
            };

            var HKG = new Flyplass
            {
                FlyplassId = "HKG",
                Navn = "Hong Kong International",
                By = "Hong Kong",
                Land = "Hong Kong",
                Kontinent = "Asia",
                Avgift = "46.10"
            };

            var HND = new Flyplass
            {
                FlyplassId = "HND",
                Navn = "Tokyo International",
                By = "Tokyo",
                Land = "Japan",
                Kontinent = "Asia",
                Avgift = "34.21"
            };

            var IAH = new Flyplass
            {
                FlyplassId = "IAH",
                Navn = "Georgi Bush Intercontinental Houston",
                By = "Houston",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "38.45"
            };

            var ICN = new Flyplass
            {
                FlyplassId = "ICN",
                Navn = "Incheon International",
                By = "Seoul",
                Land = "South Korea",
                Kontinent = "Asia",
                Avgift = "32.55"
            };

            var IST = new Flyplass
            {
                FlyplassId = "IST",
                Navn = "Atatürk International",
                By = "Istanbul",
                Land = "Turkey",
                Kontinent = "Europe",
                Avgift = "25.31"
            };

            var JFK = new Flyplass
            {
                FlyplassId = "JFK",
                Navn = "John F Kennedy International",
                By = "New York",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "43.17"
            };

            var KMG = new Flyplass
            {
                FlyplassId = "KMG",
                Navn = "Kunming Changshui International Airport",
                By = "Kunming",
                Land = "China",
                Kontinent = "Asia",
                Avgift = "29.32"
            };

            var KUL = new Flyplass
            {
                FlyplassId = "KUL",
                Navn = "Kuala Lumpur International Airport",
                By = "Kuala Lumpur",
                Land = "Malaysia",
                Kontinent = "Asia",
                Avgift = "41.76"
            };

            var LAS = new Flyplass
            {
                FlyplassId = "LAS",
                Navn = "McCarran International Airport",
                By = "Las Vegas",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "36.66"
            };

            var LAX = new Flyplass
            {
                FlyplassId = "LAX",
                Navn = "Los Angeles International Airport",
                By = "Los Angeles",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "43.78"
            };

            var LGW = new Flyplass
            {
                FlyplassId = "LGW",
                Navn = "London Gatwick",
                By = "London",
                Land = "United Kingdom",
                Kontinent = "Europe",
                Avgift = "31.09"
            };

            var LHR = new Flyplass
            {
                FlyplassId = "LHR",
                Navn = "London Heathrow",
                By = "London",
                Land = "United Kingdom",
                Kontinent = "Europe",
                Avgift = "37.11"
            };

            var MAD = new Flyplass
            {
                FlyplassId = "MAD",
                Navn = "Madrid Barajas International Airport",
                By = "Madrid",
                Land = "Spain",
                Kontinent = "Europe",
                Avgift = "31.01"
            };

            var MCO = new Flyplass
            {
                FlyplassId = "MCO",
                Navn = "Orlando International Airport",
                By = "Orlando",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "37.90"
            };

            var MEX = new Flyplass
            {
                FlyplassId = "MEX",
                Navn = "Benito Juarez International Airport",
                By = "Mexico City",
                Land = "Mexico",
                Kontinent = "North America",
                Avgift = "23.53"
            };

            var MIA = new Flyplass
            {
                FlyplassId = "MIA",
                Navn = "Miami International Airport",
                By = "Miami",
                Land = "USA",
                Kontinent = "Europe",
                Avgift = "36.31"
            };

            var MNL = new Flyplass
            {
                FlyplassId = "MNL",
                Navn = "Ninoy Aquino International Airport",
                By = "Manila",
                Land = "Philippines",
                Kontinent = "Asia",
                Avgift = "23.87"
            };

            var MSP = new Flyplass
            {
                FlyplassId = "MSP",
                Navn = "Minneapolis-St Paul International Airport",
                By = "Minneapolis",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "35.13"
            };

            var MUC = new Flyplass
            {
                FlyplassId = "MUC",
                Navn = "Munich International",
                By = "Munich",
                Land = "Germany",
                Kontinent = "Europe",
                Avgift = "28.90"
            };

            var NRT = new Flyplass
            {
                FlyplassId = "NRT",
                Navn = "Narita International Airport",
                By = "Tokyo",
                Land = "Japan",
                Kontinent = "Asia",
                Avgift = "31.71"
            };

            var ORD = new Flyplass
            {
                FlyplassId = "ORD",
                Navn = "Chicago O'Hare International Airport",
                By = "Chicago",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "33.33"
            };

            var OSL = new Flyplass
            {
                FlyplassId = "OSL",
                Navn = "Oslo Airport Gardemoen",
                By = "Oslo",
                Land = "Norway",
                Kontinent = "Europe",
                Avgift = "36.78"
            };

            var PHX = new Flyplass
            {
                FlyplassId = "PHX",
                Navn = "Phoenix Sky Harbor International",
                By = "Phoenix",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "32.19"
            };

            var SEA = new Flyplass
            {
                FlyplassId = "SEA",
                Navn = "Seattle Tacoma International Airport",
                By = "Seattle",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "36.69"
            };

            var SFO = new Flyplass
            {
                FlyplassId = "SFO",
                Navn = "San Francisco International Airport",
                By = "San Francisco",
                Land = "USA",
                Kontinent = "North America",
                Avgift = "39.99"
            };

            var SHA = new Flyplass
            {
                FlyplassId = "SHA",
                Navn = "Shanghai Hongqiao International Airport",
                By = "Shanghai",
                Land = "China",
                Kontinent = "Asia",
                Avgift = "38.11"
            };

            var SIN = new Flyplass
            {
                FlyplassId = "SIN",
                Navn = "Singapore Changi International Airport",
                By = "Singapore",
                Land = "Singapore",
                Kontinent = "Asia",
                Avgift = "25.12"
            };

            var SYD = new Flyplass
            {
                FlyplassId = "SYD",
                Navn = "Sydney Kingsford Smith International Airport",
                By = "Sydney",
                Land = "Australia",
                Kontinent = "Oseania",
                Avgift = "41.19"
            };

            var SZX = new Flyplass
            {
                FlyplassId = "SZX",
                Navn = "Shenzhen Bao'an International Airport",
                By = "Shenzhen",
                Land = "China",
                Kontinent = "Asia",
                Avgift = "25.86"
            };

            var TPE = new Flyplass
            {
                FlyplassId = "TPE",
                Navn = "Taoyuan International Airport",
                By = "Taiwan",
                Land = "Taipei",
                Kontinent = "Asia",
                Avgift = "31.07"
            };

            var PEK = new Flyplass
            {
                FlyplassId = "PEK",
                Navn = "Beijing Capital International Airport",
                By = "Beijing",
                Land = "China",
                Kontinent = "Asia",
                Avgift = "30.65"
            };

            var PVG = new Flyplass
            {
                FlyplassId = "PVG",
                Navn = "Shanghai Pudong International Airport",
                By = "Shanghai",
                Land = "China",
                Kontinent = "Asia",
                Avgift = "33.90"
            };

            var YYZ = new Flyplass
            {
                FlyplassId = "YYZ",
                Navn = "Lester B. Pearson International Airport",
                By = "Toronto",
                Land = "Canada",
                Kontinent = "North America",
                Avgift = "37.89"
            };

            base.Seed(context);
        }
    }
}