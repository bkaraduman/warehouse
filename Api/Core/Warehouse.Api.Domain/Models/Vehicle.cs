namespace Warehouse.Api.Domain.Models
{
    public class Vehicle : BaseEntity
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int YearModel { get; set; }
        public double Price { get; set; }
        public bool IsLicensed { get; set; }
        public string DateAdded { get; set; }
    }
}
