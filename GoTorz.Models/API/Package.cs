namespace GoTorz.Models.API;

public class Package
{
    public int PackageId { get; set; }
    public int FlightId { get; set; }
    public Flight Flight { get; set; }
    public int HotelId { get; set; }
    public Hotel Hotel { get; set; }
    public int CommissionPercentage { get; set; }
    public int Nights { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    // Get the flight price + total hotel price + gotorz comission
    public decimal GetPrice() => (Flight.Price + (Hotel.PricePerNight * Nights)) * (1 + (CommissionPercentage / 100));
}

public class SearchPackagesRequest
{
    public string? Departure { get; set; }
    public string? Location { get; set; }
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public int Guests { get; set; }
}