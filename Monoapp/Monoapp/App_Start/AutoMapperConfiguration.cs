using AutoMapper;
using Monoapp.Data.Database.Models.Entities;
using Monoapp.Data.ViewModels;

namespace Monoapp.App_Start
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