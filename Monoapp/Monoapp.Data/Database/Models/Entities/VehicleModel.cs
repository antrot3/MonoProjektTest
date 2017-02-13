using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monoapp.Data.Database.Models.Entities
{
    public class VehicleModel
    {
        [Key]
        public int ModelId { get; set; }

        public int MakeId { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        [ForeignKey("MakeId")]
        public virtual VehicleMake VehicleMake { get; set; }
    }
}
