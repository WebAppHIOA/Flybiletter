using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Flybiletter.Models
{

    public class AirportContext : DbContext
    {
        public AirportContext() : base("name=Flybilett")
        {
            Database.CreateIfNotExists();
            Database.SetInitializer<AirportContext>(new DbInitialize());
        }

        public DbSet<Passasjer> Passasjer { get; set; }
        public DbSet<Avgang> Avgang { get; set; }
        public DbSet<Bestilling> Bestilling { get; set; }
        public DbSet<Airport> Airport { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}