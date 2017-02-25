using System.Collections.Generic;
using Monoapp.ViewModels;

namespace Monoapp.Data.ViewModels
{
    public class PaginationModel<T> : List<T>
    {
        private List<VehicleMakeViewModel> list;
        private int max;

        public int TotalCount { get; set; }
        public List<T> List { get; set; }
        public PaginationModel(List<T> list, int totalCount)
        {
            this.List = list;
            this.TotalCount = totalCount;
        }

        public PaginationModel()
        {

        }

        public PaginationModel(List<VehicleMakeViewModel> list, int max)
        {
            this.list = list;
            this.max = max;
        }
    }
}
