using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Monoapp.Data.Database.Models;
using Monoapp.Data.Database.Models.Entities;

namespace Monoapp.Data.Repositories
{
    public interface IVehicleModelRepository
    {
        VehicleModel GetVehicleModelById(int id);
        ICollection<VehicleModel> GetVehicleByModelSearch(string sortOrder, string currentFilter, string searchByName, int pageNumber, int pageSize);
        void AddNewVehicleModel(VehicleModel vehicleModelViewModel);
        void EditVehicleModel(VehicleModel vehicleModelViewModel);
        void DeleteVehicleModel(int id);
        int GetCountOfVehicleModels();
    }
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly VehicleContext _context;
        public VehicleModelRepository()
        {
            _context = new VehicleContext();
        }
        public void AddNewVehicleModel(VehicleModel vehicleModel)
        {
            _context.VehicleModels.Add(vehicleModel);
            _context.SaveChanges();
        }

        public void EditVehicleModel(VehicleModel vehicleModel)
        {
            _context.Entry(vehicleModel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteVehicleModel(int id)
        {
            var vehicleModelToDelete = _context.VehicleModels.Find(id);
            _context.VehicleModels.Remove(vehicleModelToDelete);
            _context.SaveChanges();
        }

        public ICollection<VehicleModel> GetVehicleByModelSearch(string sortOrder, string currentFilter, string searchByName, int pageNumber, int pageSize)
        {
            var vehicleModels = GetAllVehicleModels();
            if (searchByName == null)
            {

                switch (sortOrder)
                {
                    case "Name_Desc":
                        vehicleModels = vehicleModels.OrderByDescending(b => b.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                        break;

                    case "Connection_Desc":
                        vehicleModels = vehicleModels.OrderByDescending(b => b.VehicleMake.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                        break;
                    case "Abrv_Desc":
                        vehicleModels = vehicleModels.OrderByDescending(b => b.Abrv).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                        break;
                    default:
                        vehicleModels = vehicleModels.OrderBy(v => v.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                        break;

                }
                return vehicleModels.ToList();

            }
            vehicleModels = vehicleModels.Where(v => v.VehicleMake.Name.Contains(searchByName));
            switch (sortOrder)
            {
                case "Name_Desc":
                    vehicleModels = vehicleModels.OrderByDescending(b => b.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    break;

                case "Connection_Desc":
                    vehicleModels = vehicleModels.OrderByDescending(b => b.VehicleMake.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    break;
                case "Abrv_Desc":
                    vehicleModels = vehicleModels.OrderByDescending(b => b.Abrv).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    break;
                default:
                    vehicleModels = vehicleModels.OrderBy(v => v.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    break;

            }
            return vehicleModels.ToList();
        }

        public VehicleModel GetVehicleModelById(int id)
        {
            return _context.VehicleModels.Find(id);
        }

        public int GetCountOfVehicleModels() => _context.VehicleModels.Count();
        private IQueryable<VehicleModel> GetAllVehicleModels() => _context.VehicleModels.AsQueryable();
    }
}
