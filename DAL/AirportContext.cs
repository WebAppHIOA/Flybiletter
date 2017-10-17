using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Model;

namespace DAL
{

    public class AirportContext : DbContext
    {
        public AirportContext() : base("name=Flybilett")
        {
            Database.SetInitializer<AirportContext>(new DbInitialize());
        }

        //public DbSet<Customer> Passenger { get; set; }
        public DbSet<Departure> Departure { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Airport> Airport { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}