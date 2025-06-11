using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GoTorz.Models.API;

namespace GoTorz.Models.API
{
    public class HotelApi
    {
        [JsonPropertyName("status")]
        public bool? Status { get; set; }
        // This for some reason caused the deserializing to crash and it is not necessary for our app
        //[JsonPropertyName("message")]
        //public string? Message { get; set; }
        [JsonPropertyName("timestamp")]
        public long? Timestamp { get; set; }
        [JsonPropertyName("data")]
        public DataHotel? Data { get; set; }
    }

    public class DataHotel
    {
        [JsonPropertyName("hotels")]
        public List<HotelName>? Hotels { get; set; }
    }

    public class HotelName
    {
        [JsonPropertyName("property")]
        public Property? Property { get; set; }
    }

    public class Property
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("countryCode")]
        public string? CountryCode { get; set; }
        // This for some reason caused the deserializing to crash and would be nice to have!
        //[JsonPropertyName("reviewScore")]
        //public int? ReviewScore { get; set; }
    }
}
