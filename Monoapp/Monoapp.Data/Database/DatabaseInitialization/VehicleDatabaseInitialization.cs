using System.Collections.Generic;
using System.Data.Entity;
using Monoapp.Data.Database.Models;
using Monoapp.Data.Database.Models.Entities;

namespace Monoapp.Data.Database.DatabaseInitialization
{
    public class VehicleDatabaseInitialization : CreateDatabaseIfNotExists<VehicleContext>
    {
        protected override void Seed(VehicleContext context)
        {
            var vehiclesMakers = new List<VehicleMake>
            {
                new VehicleMake {Name = "BMW", Abrv = "2016"},
                new VehicleMake {Name = "Pedguot", Abrv = "2012"},
                new VehicleMake {Name = "Ford", Abrv = "2009"},
                new VehicleMake {Name = "Toyota", Abrv = "2003"},
            };

            var vehicleModels = new List<VehicleModel>
            {
                new VehicleModel{MakeId=1, Name="X5", Abrv="2016"},
                new VehicleModel{MakeId=2, Name="308", Abrv="2016"},
                new VehicleModel{MakeId=3, Name="Focus", Abrv="2016"},
                new VehicleModel{MakeId=1, Name="X7", Abrv="2016"},
                new VehicleModel{MakeId=3, Name="Fiesta", Abrv="2016"},
            };

            vehiclesMakers.ForEach(s => context.VehicleMakes.Add(s));
            vehicleModels.ForEach(s => context.VehicleModels.Add(s));

            context.SaveChanges();
        }
    }
}
