using System.ComponentModel;

namespace AutoMarket.Models
{
    public class MotorcycleModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public int MotorcycleBrandId { get; set; }
        public MotorcycleBrand MotorcycleBrand { get; set; }    
    }
}
