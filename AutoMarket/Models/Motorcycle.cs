using Microsoft.AspNetCore.Identity;
using Models.Enums;

namespace AutoMarket.Models
{
    public class Motorcycle
    {
        public int Id { get; set; }
        public DateTime FirstRegistration { get; set; }
        public int EnginePower { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public MotorcycleType MotorcycleType { get; set; }
        public Condition Condition { get; set; }
        public Color Color { get; set; }
        public FuelType FuelType { get; set; }
        public Mileage Mileage { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public int MotorcycleBrandId { get; set; }
        public int MotorcycleModelId { get; set; }
        public MotorcycleBrand? MotorcycleBrand { get; set; }
        public MotorcycleModel? MotorcycleModel { get; set; }
        public IdentityUser? User { get; set; }
        public List<MotorcyclePhoto>? Photos { get; set; }

    }
}
