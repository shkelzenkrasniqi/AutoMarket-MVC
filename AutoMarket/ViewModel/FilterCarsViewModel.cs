using AutoMarket.Models;
using Models.Enums;

namespace AutoMarket.ViewModel
{
    public class FilterCarsViewModel
    {
        public float? Price { get; set; }
        public int? BrandId { get; set; }
        public int? ModelId { get; set; }
        public Color color { get; set; }
        public Condition condition { get; set; }
        public FuelType fuelType { get; set; }
        public Mileage mileage { get; set; }
        public CarSeats seats { get; set; }
        public TransmissionType transmissionType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SortByPrice { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public List<Car> FilteredCars { get; set; }
        public List<CarBrand> Brands { get; set; }
        public List<CarModel> Models { get; set; }
    }
}
