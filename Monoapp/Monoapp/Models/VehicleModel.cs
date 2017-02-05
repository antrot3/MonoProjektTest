using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Monoapp.Models
{
    [Table("VehicleModle")]
    public class VehicleModel
    {
        [Key]
        public int ModelId { get; set;}
        
        public int MakeId { get; set; }

        public string name { get; set; }

        public string abrv { get; set; }
        [ForeignKey("MakeId")]
        public virtual VehicleMake VehicleMake { get; set; }
    }
}