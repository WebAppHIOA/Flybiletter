﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Flybiletter.Models;
using System.Text;

namespace Flybiletter
{

    public class DB
    {
        public List<Airport> getAllAirports()
        {
            using (var db = new AirportContext())
            {
                List<Airport> allAirports = (from a in db.Airport
                                             select a).ToList();
                return allAirports;
            }
        }


        public Boolean AddOrder(Order order)
        {
            using (var db = new AirportContext())
            {
                var departure = db.Departure.Where(d => d.FlightId == order.Departure.FlightId).First();
                departure.Order.Add(new Order
                {
                    OrderNumber = order.OrderNumber,
                    Date = order.Date,
                    Firstname = order.Firstname,
                    Surname = order.Surname,
                    Tlf = order.Tlf,
                    Email = order.Email,
                    Price = order.Price,
                });

                db.SaveChanges();

                return true;
            }
        }

        //Fjernes?
        public Departure getFlightID(string id)
        {
            var db = new AirportContext();
            Departure dbFlightID = db.Departure.Find(id);

            var flightDetails = new Departure()
            {
                FlightId = dbFlightID.FlightId,
                From = dbFlightID.From,
                To = dbFlightID.To,
                Date = dbFlightID.Date,
                // pris??
            };
            return flightDetails;
        }

        public List<Departure> GetFlightInfo()
        {
            var db = new AirportContext();
            List<Departure> dep = db.Departure.Select(d => new Departure()
            {
                FlightId = d.FlightId,
                DepartureTime = d.DepartureTime,
                From = d.From,
                To = d.To,
                //burde stå pris, men pris er på order klassen
            }).ToList();
            return dep;
        }




        public Order FindOrder(string id)
        {
            using (var db = new AirportContext())
            {
                var order = db.Order.First(o => o.OrderNumber == id);
                return order;
            }
        }


        public Boolean AddDeparture(Departure departure)
        {
            using (var db = new AirportContext())
            {

                var airport = db.Airport.Where(c => c.AirportId == departure.Airport.AirportId).First();
                airport.Departure.Add(new Departure
                {
                    FlightId = departure.FlightId,
                    From = departure.From,
                    To = departure.To,
                    Date = departure.Date,
                    DepartureTime = departure.DepartureTime,
                });


                db.SaveChanges();

                return true;
            }

        }


        public Departure FindDeparture(string id)
        {
            using (var db = new AirportContext())
            {
                var departure = db.Departure.First(d => d.FlightId == id);
                return departure;
            }
        }

        public string UniqueReference()
        {
            var guid = System.Guid.NewGuid().ToString();

            return guid;
        }


        /*
        public Boolean AddCustomer(Customer passenger)
        {
            using (var db = new AirportContext())
            {
                Customer insertCustomer = new Customer
                {
                    CustomerId = passenger.CustomerId,
                    Firstname = passenger.Firstname,
                    Surname = passenger.Surname,
                    Tlf = passenger.Tlf,
                    Email = passenger.Email
                };

                var order = db.Order.Where(o => o.OrderNumber == passenger.Order.OrderNumber).First();
                order.Passenger.Add(insertCustomer);

                var departure = db.Departure.Where(d => d.FlightId == passenger.Departure.FlightId).First();
                departure.Customer.Add(insertCustomer);

                db.SaveChanges();

                return true;
            }

        }
 */
        public Boolean IsFlightIdAvailable(string toTest)
        {
            using (var db = new AirportContext())
            {
                var available = db.Departure.Any(row => row.FlightId == toTest);
                if (available)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}