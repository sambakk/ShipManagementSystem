namespace ShipManagement.Domain.Entities;
public class Ship
{
    //TODO : add validation
    public Guid id { get; set; }
    public string name { get; set; } = String.Empty;
    public double length { get; set; }
    public double width { get; set; }
    public string code { get; set; } = String.Empty;
}