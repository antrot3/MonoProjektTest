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
        IEnumerable<VehicleModelViewModel> GetVehicleByModelSearch(string sortOrder, string currentFilter, string searchByName, int? page);
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
        public IEnumerable<VehicleModelViewModel> GetVehicleByModelSearch(string sortOrder, string currentFilter, string searchByName, int? page)
        {
            var VehicleModel = Mapper.Map<IEnumerable<VehicleModelViewModel>>(_context.VehicleModels.ToList());
            if (searchByName == null)
            {

                int currentpage = page ?? default(int);
                switch (sortOrder)
                {
                    case "Name_Desc":
                        if (currentpage > 1)
                        {
                            VehicleModel = VehicleModel.OrderByDescending(b => b.Name).Skip((currentpage - 2) * 3).Take(10);
                        }
                        else {
                            VehicleModel = VehicleModel.OrderByDescending(b => b.Name).Take(6);
                        }
                        break;

                    case "Connection_Desc":
                        if (currentpage > 1)
                        {
                            VehicleModel = VehicleModel.OrderByDescending(b => b.VehicleMake.Name).Skip((currentpage - 2) * 3).Take(10);
                        }
                        else
                        {
                            VehicleModel = VehicleModel.OrderByDescending(b => b.VehicleMake.Name).Take(6);

                        }
                        break;
                    case "Abrv_Desc":
                        if (currentpage > 1)
                        {
                            VehicleModel = VehicleModel.OrderByDescending(b => b.Abrv).Skip((currentpage - 2) * 3).Take(10);
                        }
                        else
                        {
                            VehicleModel = VehicleModel.OrderByDescending(b => b.Abrv).Take(6);
                        }
                        break;
                    default:

                        if (currentpage > 1)
                        {
                            VehicleModel = VehicleModel.OrderBy(v => v.Name).Skip((currentpage - 2) * 3).Take(10);
                        }
                        else
                        {
                            VehicleModel = VehicleModel.OrderBy(v => v.Name).Take(6);
                        }
                        break;

                }
                return VehicleModel;

            }
            else {

                VehicleModel = VehicleModel.Where(v => v.VehicleMake.Name.Contains(searchByName));
                int currentpage = page ?? default(int);
                switch (sortOrder)
                {
                    case "Name_Desc":
                        if (currentpage > 1)
                        {
                            VehicleModel = VehicleModel.OrderByDescending(b => b.Name).Skip((currentpage - 2) * 3).Take(10);
                        }
                        else {
                            VehicleModel = VehicleModel.OrderByDescending(b => b.Name).Take(6);
                        }
                        break;

                    case "Connection_Desc":
                        if (currentpage > 1)
                        {
                            VehicleModel = VehicleModel.OrderByDescending(b => b.VehicleMake.Name).Skip((currentpage - 2) * 3).Take(10);
                        }
                        else
                        {
                            VehicleModel = VehicleModel.OrderByDescending(b => b.VehicleMake.Name).Take(6);

                        }
                        break;
                    case "Abrv_Desc":
                        if (currentpage > 1)
                        {
                            VehicleModel = VehicleModel.OrderByDescending(b => b.Abrv).Skip((currentpage - 2) * 3).Take(10);
                        }
                        else
                        {
                            VehicleModel = VehicleModel.OrderByDescending(b => b.Abrv).Take(6);
                        }
                        break;
                    default:
                        
                         if (currentpage > 1)
                        {
                            VehicleModel = VehicleModel.OrderBy(v => v.Name).Skip((currentpage - 2) * 3).Take(10);
                        }
                        else
                        {
                            VehicleModel = VehicleModel.OrderBy(v => v.Name).Take(6);
                        }
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
