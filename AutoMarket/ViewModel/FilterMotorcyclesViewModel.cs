using AutoMarket.Models;
using Models.Enums;

namespace AutoMarket.ViewModel
{
    public class FilterMotorcyclesViewModel
    {
        public float? Price { get; set; }
        public int? BrandId { get; set; }
        public int? ModelId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Condition condition { get; set; }
        public FuelType fuelType { get; set; }
        public Mileage mileage { get; set; }
        public MotorcycleType type { get; set; }
        public TransmissionType transmissionType { get; set; }
        public Color color { get; set; }
        public string SortByPrice { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public bool IsNew { get; set; }
        public bool IsUsed { get; set; }
        public List<Motorcycle> FilteredMotorcycles { get; set; }
        public List<MotorcycleBrand> Brands { get; set; }
        public List<MotorcycleModel> Models { get; set; }
    }
}
