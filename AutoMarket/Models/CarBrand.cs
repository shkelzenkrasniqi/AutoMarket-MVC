namespace AutoMarket.Models
{
    public class CarBrand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public ICollection<CarModel> CarModels { get; set; }
    }
}
