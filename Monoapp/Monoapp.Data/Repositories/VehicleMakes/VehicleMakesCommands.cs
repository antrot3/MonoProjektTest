using System.Data.Entity;
using AutoMapper;
using Monoapp.Data.Database.Models;
using Monoapp.Data.Database.Models.Entities;
using Monoapp.Data.ViewModels;

namespace Monoapp.Data.Repositories.VehicleMakes
{
    public interface IVehicleMakesCommands
    {
        void AddNewVehicleMake(VehicleMakeViewModel vehicleMakeViewModel);
        void EditVehicleMake(VehicleMakeViewModel vehicleMakeViewModel);
        void DeleteVehicleMake(int id);
    }
    public class VehicleMakesCommands : IVehicleMakesCommands
    {
        private readonly VehicleContext _context;
        public VehicleMakesCommands()
        {
            _context = new VehicleContext();
        }
        public void AddNewVehicleMake(VehicleMakeViewModel vehicleMakeViewModel)
        {
            var vehicleMakeAsDataObject = Mapper.Map<VehicleMake>(vehicleMakeViewModel);
            _context.VehicleMakes.Add(vehicleMakeAsDataObject);
            _context.SaveChanges();
        }

        public void EditVehicleMake(VehicleMakeViewModel vehicleMakeViewModel)
        {
            var vehicleMakeAsDataObject = Mapper.Map<VehicleMake>(vehicleMakeViewModel);
            _context.Entry(vehicleMakeAsDataObject).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteVehicleMake(int id)
        {
            var vehicleMakeToDelete = _context.VehicleMakes.Find(id);
            _context.VehicleMakes.Remove(vehicleMakeToDelete);
            _context.SaveChanges();
        }
    }
}
