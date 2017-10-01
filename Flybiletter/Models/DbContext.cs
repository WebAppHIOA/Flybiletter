using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Flybiletter.Models
{

    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext() : base("name=Flybilett")
        {
            Database.SetInitializer<SchoolDBContext>(new DbInitialize());
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