using System.Collections.Generic;
using System.Data.Entity;
using AutoMapper;
using Monoapp.Data.Database.Models;
using Monoapp.Data.Database.Models.Entities;
using Monoapp.Data.ViewModels;
using System.Linq;
using PagedList;

namespace Monoapp.Data.Repositories
{
    public interface IVehicleMakesRepository
    {
        IEnumerable<VehicleMakeViewModel> GetAllVehicleMakes();
        IEnumerable<VehicleMakeViewModel> GetVehicleByMakeSearch(string sortOrder, string currentFilter, string searchByName, int? page);
        VehicleMakeViewModel GetVehicleMakeById(int id);
        void AddNewVehicleMake(VehicleMakeViewModel vehicleMakeViewModel);
        void EditVehicleMake(VehicleMakeViewModel vehicleMakeViewModel);
        void DeleteVehicleMake(int id);
    }
    public class VehicleMakesRepository : IVehicleMakesRepository
    {
        private readonly VehicleContext _context;
        public VehicleMakesRepository()
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

        public IEnumerable<VehicleMakeViewModel> GetAllVehicleMakes()
        {
            return
                Mapper.Map<IEnumerable<VehicleMakeViewModel>>(_context.VehicleMakes);
        }
        public IEnumerable<VehicleMakeViewModel> GetVehicleByMakeSearch(string sortOrder, string currentFilter, string searchByName, int? page)
        {
            var VehicleMake = Mapper.Map<IEnumerable<VehicleMakeViewModel>>(_context.VehicleMakes);
            if (searchByName == null)
            {
                int currentpage = page ?? default(int);
                switch (sortOrder)
                {
                    case "name_desc":
                        if (currentpage > 1)
                        {
                            VehicleMake = VehicleMake.OrderByDescending(b => b.Name).Skip((currentpage - 2) * 3).Take(10);
                        }
                        else
                        {
                            VehicleMake = VehicleMake.OrderByDescending(b => b.Name).Take(6);
                        }
                        break;
                    case "abrv_desc":
                        if( currentpage >1)
                        {
                            VehicleMake = VehicleMake.OrderByDescending(b => b.Abrv).Skip((currentpage-2) *3).Take(10);
                        }
                        else
                        {
                            VehicleMake = VehicleMake.OrderByDescending(b => b.Abrv).Take(6);

                        }
                        break;
                    default:
                        if (currentpage > 1)
                        {
                            VehicleMake = VehicleMake.OrderBy(b => b.Name).Skip((currentpage - 2) * 3).Take(10);
                        }
                        else
                        {
                            VehicleMake = VehicleMake.OrderBy(b => b.Name).Take(4);

                        }
                        break;
                }
                return VehicleMake;
            }
            else {
                VehicleMake = VehicleMake.Where(v => v.Name.Contains(searchByName));
                int currentpage = page ?? default(int);
                switch (sortOrder)
                {
                    case "name_desc":
                        if (currentpage > 1)
                        {
                            VehicleMake = VehicleMake.OrderByDescending(b => b.Name).Skip((currentpage - 2) * 3).Take(10);
                        }
                        else
                        {
                            VehicleMake = VehicleMake.OrderByDescending(b => b.Name).Take(6);
                        }
                        break;
                    case "abrv_desc":
                        if (currentpage > 1)
                        {
                            VehicleMake = VehicleMake.OrderByDescending(b => b.Abrv).Skip((currentpage - 2) * 3).Take(10);
                        }
                        else
                        {
                            VehicleMake = VehicleMake.OrderByDescending(b => b.Abrv).Take(6);
                        }
                        break;
                    default:
                        if (currentpage > 1)
                        {
                            VehicleMake = VehicleMake.OrderBy(b => b.Name).Skip((currentpage - 2) * 3).Take(10);
                        }
                        else
                        {
                            VehicleMake = VehicleMake.OrderBy(b => b.Name).Take(4);

                        }
                        break;
                }
                return VehicleMake;

            }
        }

        public VehicleMakeViewModel GetVehicleMakeById(int id)
        {
            return
                Mapper.Map<VehicleMakeViewModel>(_context.VehicleMakes.Find(id));
        }
    }
}
