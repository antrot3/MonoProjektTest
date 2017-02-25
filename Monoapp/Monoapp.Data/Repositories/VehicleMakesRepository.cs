using System.Collections.Generic;
using System.Data.Entity;
using Monoapp.Data.Database.Models;
using Monoapp.Data.Database.Models.Entities;
using System.Linq;

namespace Monoapp.Data.Repositories
{
    public interface IVehicleMakesRepository
    {
        ICollection<VehicleMake> GetVehicleByMakeSearch(string sortOrder, string currentFilter, string searchByName, int pageNumber, int pageSize);
        IEnumerable<VehicleMake> GetAllVehicleMakes();
        VehicleMake GetVehicleMakeById(int id);
        void AddNewVehicleMake(VehicleMake vehicleMakeViewModel);
        void EditVehicleMake(VehicleMake vehicleMakeViewModel);
        void DeleteVehicleMake(int id);
        int GetCountOfVehicleMake();
        

    }
    public class VehicleMakesRepository : IVehicleMakesRepository
    {
        private readonly VehicleContext _context;
        public VehicleMakesRepository()
        {
            _context = new VehicleContext();
        }
        public void AddNewVehicleMake(VehicleMake vehicleMake)
        {
            _context.VehicleMakes.Add(vehicleMake);
            _context.SaveChanges();
        }

        public void EditVehicleMake(VehicleMake vehicleMake)
        {
            _context.Entry(vehicleMake).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteVehicleMake(int id)
        {
            var vehicleMakeToDelete = _context.VehicleMakes.Find(id);
            _context.VehicleMakes.Remove(vehicleMakeToDelete);
            _context.SaveChanges();
        }

        
        public ICollection<VehicleMake> GetVehicleByMakeSearch(string sortOrder, string currentFilter, string searchByName, int pageNumber,int pageSize)
        {
            var vehicleMakes = _getAllVehicleMakes();

            if (searchByName == null)
            {
               switch (sortOrder)
                {
                    case "name_desc":
                            vehicleMakes = vehicleMakes.OrderByDescending(b => b.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                        break;
                    case "abrv_desc":
                            vehicleMakes = vehicleMakes.OrderByDescending(b => b.Abrv).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                        break;
                    default:
                        vehicleMakes = vehicleMakes.OrderBy(b => b.Name).Skip((pageNumber-1)*pageSize).Take(pageSize);
                        break;
                }
                return vehicleMakes.ToList();
            }

            vehicleMakes = vehicleMakes.Where(v => v.Name.Contains(searchByName));
            switch (sortOrder)
            {
                case "name_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(b => b.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    break;
                case "abrv_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(b => b.Abrv).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    break;
                default:
                    vehicleMakes = vehicleMakes.OrderBy(b => b.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    break;
                    
            }
            return vehicleMakes.ToList();
        }

        public VehicleMake GetVehicleMakeById(int id)
        {
            return _context.VehicleMakes.Find(id);
        }

        public IEnumerable<VehicleMake> GetAllVehicleMakes()
        {
            return _context.VehicleMakes;
        }

        public int GetCountOfVehicleMake() =>  _context.VehicleMakes.Count();
        private IQueryable<VehicleMake> _getAllVehicleMakes() => _context.VehicleMakes.AsQueryable();
    }
}
