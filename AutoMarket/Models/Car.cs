using Microsoft.AspNetCore.Identity;
using Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace AutoMarket.Models
{
    public class Car
    {
        public int Id { get; set; }
        public DateTime FirstRegistration { get; set; }
        public int EnginePower { get; set; } 
        public float Price { get; set; }
        public string Features { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int CarBrandId { get; set; }
        public int CarModelId { get; set; }
        public IdentityUser? User { get; set; }
        public CarBrand? CarBrand { get; set; }
        public CarModel? CarModel { get; set; }
        public Condition CarCondition { get; set; }
        public Color CarColor { get; set; }
        public FuelType CarFuelType { get; set; }  
        public Mileage CarMileage { get; set; }
        public CarSeats CarSeats { get; set; }
        public TransmissionType CarTransmissionType { get; set; }
        public List<CarPhoto>? Photos { get; set; }

    }
}
