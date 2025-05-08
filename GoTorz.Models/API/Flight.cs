namespace GoTorz.Models.API;

public class Flight
{
    public int FlightId { get; set; }

    public string FlightNumber { get; set; }

    public string DepartureAirport { get; set; }

    public string ArrivalAirport { get; set; }
    public string ArrivalCity { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public string Airline { get; set; }

    public decimal Price { get; set; }
}
