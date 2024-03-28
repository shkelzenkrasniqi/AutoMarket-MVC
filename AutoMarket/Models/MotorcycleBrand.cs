using System.ComponentModel;

namespace AutoMarket.Models
{
    public class MotorcycleBrand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public ICollection<MotorcycleModel> MotorcycleModels { get; set; }

    }
}
