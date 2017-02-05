using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Monoapp.Models
{
     [Table("Vehiclemake")]
    public class VehicleMake
    {
        [Key]
        public int MakeId { get; set; }
        public string Name { get; set; }

        public string Abrv { get; set; }

        public virtual ICollection<VehicleModel> VehicleModel { get; set; }
    }
}