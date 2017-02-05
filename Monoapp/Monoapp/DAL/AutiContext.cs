using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monoapp.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Monoapp.DAL
{
    public class AutiContext : DbContext
    {   
        public AutiContext() : base("Test")
        {

        }
        public DbSet<VehicleMake> VehicleMake { get; set; }
        public DbSet<VehicleModel> VehicleModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}