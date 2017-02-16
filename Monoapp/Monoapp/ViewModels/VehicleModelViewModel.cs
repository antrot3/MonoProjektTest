namespace Monoapp.Data.ViewModels
{
    public class VehicleModelViewModel
    {
        public int ModelId { get; set; }

        public int MakeId { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public virtual VehicleMakeViewModel VehicleMake { get; set; }
    }
}
