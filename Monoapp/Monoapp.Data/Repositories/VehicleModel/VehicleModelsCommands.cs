using System.Data.Entity;
using AutoMapper;
using Monoapp.Data.Database.Models;
using Monoapp.Data.ViewModels;

namespace Monoapp.Data.Repositories.VehicleModel
{
    public interface IVehicleModelsCommands
    {
        void AddNewVehicleModel(VehicleModelViewModel vehicleModelViewModel);
        void EditVehicleModel(VehicleModelViewModel vehicleModelViewModel);
        void DeleteVehicleModel(int id);
    }
    public class VehicleModelsCommands : IVehicleModelsCommands
    {
        private readonly VehicleContext _context;
        public VehicleModelsCommands()
        {
            _context = new VehicleContext();
        }
        public void AddNewVehicleModel(VehicleModelViewModel vehicleModelViewModel)
        {
            var vehicleModelAsDataObject = Mapper.Map<Database.Models.Entities.VehicleModel>(vehicleModelViewModel);
            _context.VehicleModels.Add(vehicleModelAsDataObject);
            _context.SaveChanges();
        }

        public void EditVehicleModel(VehicleModelViewModel vehicleModelViewModel)
        {
            var vehicleModelAsDataObject = Mapper.Map<Database.Models.Entities.VehicleModel>(vehicleModelViewModel);
            _context.Entry(vehicleModelAsDataObject).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteVehicleModel(int id)
        {
            var vehicleModelToDelete = _context.VehicleModels.Find(id);
            _context.VehicleModels.Remove(vehicleModelToDelete);
            _context.SaveChanges();
        }
    }
}
