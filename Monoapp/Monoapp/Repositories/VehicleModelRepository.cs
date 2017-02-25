using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Monoapp.Data.Database.Models;
using Monoapp.Data.ViewModels;

namespace Monoapp.Data.Repositories
{
    public interface IVehicleModelRepository
    {
        IEnumerable<VehicleModelViewModel> GetAllVehicleModels();
        VehicleModelViewModel GetVehicleModelById(int id);
        IEnumerable<VehicleModelViewModel> GetVehicleByModelSearch(string sortOrder, string currentFilter, string searchByName, int pageNumber, int pageSize);
        void AddNewVehicleModel(VehicleModelViewModel vehicleModelViewModel);
        void EditVehicleModel(VehicleModelViewModel vehicleModelViewModel);
        void DeleteVehicleModel(int id);
    }
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly VehicleContext _context;
        public VehicleModelRepository()
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
        public IEnumerable<VehicleModelViewModel> GetAllVehicleModels()
        {
            return
                Mapper.Map<IEnumerable<VehicleModelViewModel>>(_context.VehicleModels.ToList());
        }
        public IEnumerable<VehicleModelViewModel> GetVehicleByModelSearch(string sortOrder, string currentFilter, string searchByName, int pageNumber, int pageSize)
        {
            var VehicleModel = Mapper.Map<IEnumerable<VehicleModelViewModel>>(_context.VehicleModels);
            if (searchByName == null)
            {

                switch (sortOrder)
                {
                    case "Name_Desc":
                            VehicleModel = VehicleModel.OrderByDescending(b => b.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        break;

                    case "Connection_Desc":
                            VehicleModel = VehicleModel.OrderByDescending(b => b.VehicleMake.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        break;
                    case "Abrv_Desc":
                            VehicleModel = VehicleModel.OrderByDescending(b => b.Abrv).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        break;
                    default:
                            VehicleModel = VehicleModel.OrderBy(v => v.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        break;

                }
                return VehicleModel;

            }
            else {

                VehicleModel = VehicleModel.Where(v => v.VehicleMake.Name.Contains(searchByName)).ToList();
                switch (sortOrder)
                {
                    case "Name_Desc":
                            VehicleModel = VehicleModel.OrderByDescending(b => b.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        break;

                    case "Connection_Desc":
                            VehicleModel = VehicleModel.OrderByDescending(b => b.VehicleMake.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        break;
                    case "Abrv_Desc":
                            VehicleModel = VehicleModel.OrderByDescending(b => b.Abrv).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        break;
                    default:
                        VehicleModel = VehicleModel.OrderBy(v => v.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        break;

                }
                return VehicleModel;
            }
        }

        public VehicleModelViewModel GetVehicleModelById(int id)
        {
            return
                Mapper.Map<VehicleModelViewModel>(_context.VehicleModels.Find(id));
        }
    }
}
