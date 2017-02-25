using AutoMapper;
using Monoapp.Data.Database.Models.Entities;
using Monoapp.ViewModels;

namespace Monoapp
{
    public static class AutoMapperConfiguration
    {
        public static void Execute()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<VehicleModelViewModel, VehicleModel>().ReverseMap();
                cfg.CreateMap<VehicleMakeViewModel, VehicleMake>().ReverseMap();
            });
        }
    }
}