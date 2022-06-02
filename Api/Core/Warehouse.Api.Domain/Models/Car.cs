namespace Warehouse.Api.Domain.Models
{
    public class Car
    {
        public string Location { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
