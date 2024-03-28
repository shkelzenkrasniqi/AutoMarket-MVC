namespace AutoMarket.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public int CarBrandId { get; set; }
        public CarBrand CarBrand { get; set; }
    }
}
