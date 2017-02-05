using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Monoapp.Models;

namespace Monoapp.DAL
{
    public class AutiIntialize : System.Data.Entity.DropCreateDatabaseIfModelChanges<AutiContext>
    {
        protected override void Seed(AutiContext context)
        {
            var VehicleMake = new List<VehicleMake>
            {
            new VehicleMake{Name="BMW",Abrv="2016"},
            new VehicleMake{Name="Pedguot",Abrv="2012"},
            new VehicleMake{Name="Ford",Abrv="2009"},
            new VehicleMake{Name="Toyota",Abrv="2003"},
            };

            VehicleMake.ForEach(s => context.VehicleMake.Add(s));
            context.SaveChanges();
            var VehicleModel = new List<VehicleModel>
            {
            new VehicleModel{MakeId=1,name="X5",abrv="2016"},
            new VehicleModel{MakeId=2,name="308",abrv="2016"},
            new VehicleModel{MakeId=3,name="Focus",abrv="2016"},
            new VehicleModel{MakeId=1,name="X7",abrv="2016"},
            new VehicleModel{MakeId=3,name="Fiesta",abrv="2016"},

            };
            VehicleModel.ForEach(s => context.VehicleModel.Add(s));
            context.SaveChanges();
            
        }
    }
}