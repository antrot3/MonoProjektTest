using System.Collections.Generic;
using AutoMapper;
using Monoapp.Data.Database.Models;
using Monoapp.Data.ViewModels;

namespace Monoapp.Data.Repositories.VehicleMakes
{
    public interface IVehicleMakesQueries
    {
        IEnumerable<VehicleMakeViewModel> GetAllVehicleMakes();
        VehicleMakeViewModel GetVehicleMakeById(int id);
    }
    public class VehicleMakesQueries : IVehicleMakesQueries
    {
        private readonly IVehicleContext _context;
        public VehicleMakesQueries()
        {
            _context = new VehicleContext();
        }
        public IEnumerable<VehicleMakeViewModel> GetAllVehicleMakes()
        {
            return 
                Mapper.Map<IEnumerable<VehicleMakeViewModel>>(_context.VehicleMakes);
        }
        public VehicleMakeViewModel GetVehicleMakeById(int id)
        {
            return 
                Mapper.Map<VehicleMakeViewModel>(_context.VehicleMakes.Find(id));
        }
    }
}
