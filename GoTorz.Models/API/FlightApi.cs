using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GoTorz.Models.API
{
    public class FlightApi
    {
        [JsonPropertyName("status")]
        public bool? Status { get; set; }
        // This for some reason caused the deserializing to crash and it is not necessary for our app
        //[JsonPropertyName("message")]
        //public string? Message { get; set; }
        [JsonPropertyName("timestamp")]
        public long? Timestamp { get; set; }
        [JsonPropertyName("data")]
        public Data? Data { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("flightOffers")]
        public List<FlightOffer>? FlightOffers { get; set; }
    }

    public class FlightOffer
    {
        [JsonPropertyName("segments")]
        public List<Segment>? Segments { get; set; }
        [JsonPropertyName("priceBreakdown")]
        public PriceBreakdown? PriceBreakdown { get; set; }
    }

    public class Segment
    {
        [JsonPropertyName("legs")]
        public List<Leg>? Legs { get; set; }
    }

    public class Leg
    {
        [JsonPropertyName("departureTime")]
        public DateTime DepartureTime { get; set; }
        [JsonPropertyName("arrivalTime")]
        public DateTime ArrivalTime { get; set; }
        [JsonPropertyName("departureAirport")]
        public DepartureAirport? DepartureAirport { get; set; }
        [JsonPropertyName("arrivalAirport")]
        public ArrivalAirport? ArrivalAirport { get; set; }
        [JsonPropertyName("flightInfo")]
        public FlightInfo? FlightInfo { get; set; }
        [JsonPropertyName("carriersData")]
        public List<CarriersData>? CarriersData { get; set; }
    }

    public class PriceBreakdown
    {
        [JsonPropertyName("total")]
        public Total? Total { get; set; }
    }

    public class Total
    {
        [JsonPropertyName("currencyCode")]
        public string? CurrencyCode { get; set; }
        [JsonPropertyName("units")]
        public int? Units { get; set; }
        [JsonPropertyName("nanos")]
        public int? Nanos { get; set; }
    }

    public class DepartureAirport
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        [JsonPropertyName("cityName")]
        public string? CityName { get; set; }
    }

    public class ArrivalAirport
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        [JsonPropertyName("cityName")]
        public string? CityName { get; set; }
    }

    public class FlightInfo
    {
        [JsonPropertyName("flightNumber")]
        public int? FlightNumber { get; set; }
    }

    public class CarriersData
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
