using System.Data.Entity;
using Monoapp.Data.Database.DatabaseInitialization;
using Monoapp.Data.Database.Models.Entities;

namespace Monoapp.Data.Database.Models
{
    public interface IVehicleContext
    {
        IDbSet<VehicleMake> VehicleMakes { get; set; }
        IDbSet<VehicleModel> VehicleModels { get; set; }
    }

    public class VehicleContext : DbContext, IVehicleContext
    {
        public VehicleContext() : base("name=VehicleContextConnectionString")
        {
            System.Data.Entity.Database.SetInitializer(new VehicleDatabaseInitialization());
        }

        public IDbSet<VehicleMake> VehicleMakes { get; set; }
        public IDbSet<VehicleModel> VehicleModels { get; set; }
    }
}
