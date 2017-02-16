using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Monoapp.Data.Database.Models;
using Monoapp.ViewModels;

namespace Monoapp.Data.Repositories.VehicleModel
{
    public interface IVehicleModelsQueries
    {
        IEnumerable<VehicleModelViewModel> GetAllVehicleModels();
        VehicleModelViewModel GetVehicleModelById(int id);
    }
    public class VehicleModelsQueries : IVehicleModelsQueries
    {
        private readonly IVehicleContext _context;
        public VehicleModelsQueries()
        {
            _context = new VehicleContext();
        }
        public IEnumerable<VehicleModelViewModel> GetAllVehicleModels()
        {
            return 
                Mapper.Map<IEnumerable<VehicleModelViewModel>>(_context.VehicleModels.ToList());
        }
        public VehicleModelViewModel GetVehicleModelById(int id)
        {
            return 
                Mapper.Map<VehicleModelViewModel>(_context.VehicleModels.Find(id));
        }
    }
}
