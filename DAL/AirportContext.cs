using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Model;
using TrackerEnabledDbContext;

namespace DAL
{

    public class AirportContext : TrackerContext
    {
        public AirportContext() : base("name=Flybilett")
        {
            Database.SetInitializer<AirportContext>(new DbInitialize());
        }

       
        public DbSet<Departure> Departure { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Airport> Airport { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
           /* 
          // Allow to delete and update entries between Airport and Departure
            modelBuilder.Entity<Airport>()
                        .HasMany(v => v.Departure)
                        .WithRequired(v => v.Airport)
                        .WillCascadeOnDelete(true);


            //Allow to delete and update entries between Departure and Order 
            modelBuilder.Entity<Departure>()
                        .HasMany(v => v.Order)
                        .WithRequired(v => v.Departure)
                        .WillCascadeOnDelete(true);
                        
            base.OnModelCreating(modelBuilder);
            */
            
        }
    }
    }