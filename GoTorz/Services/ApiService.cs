using System.Text.Json;
using GoTorz.Models.API;

namespace GoTorz.Services
{
    public class ApiService : IFlightHotelApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _apiKey;
        private readonly string _apiHost;
        
        // The different Api settings are stored in the appsettings.json file
        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _apiKey = configuration["ApiSettings:ApiKey"];
            _apiHost = configuration["ApiSettings:ApiHost"];
        }

        public async Task<(FlightApi Flights, HotelApi Hotels)> SearchFlightsAndHotelsAsync(string departureAirport, string destinationAirport, string departDateFLight, string destinationId, string arrivalHotel, string departureHotel)
        {
            var urlFlights = $"{_baseUrl}/flights/searchFlights?fromId={departureAirport}.AIRPORT&toId={destinationAirport}.AIRPORT&departDate={departDateFLight}&stops=none&pageNo=1&adults=1&children=0&sort=BEST&cabinClass=ECONOMY&currency_code=AED";
            var urlHotels = $"{_baseUrl}/hotels/searchHotels?dest_id={destinationId}&search_type=CITY&arrival_date={arrivalHotel}&departure_date={departureHotel}&adults=1&children_age=0%2C17&room_qty=1&page_number=1&units=metric&temperature_unit=c&languagecode=en-us&currency_code=AED&location=US";

            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", _apiKey);
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", _apiHost);

            var flights = await GetFlightsAsync(urlFlights);

            var hotels = await GetHotelsAsync(urlHotels);
            
            return (flights, hotels);
        }

        private async Task<FlightApi> GetFlightsAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            Console.WriteLine(response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<FlightApi>(responseStream);
            }
            
            //Console.WriteLine("GetFlightsAsync failed!");
            return null;
        }

        private async Task<HotelApi> GetHotelsAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            Console.WriteLine(response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<HotelApi>(responseStream);
            }
            
            //Console.WriteLine("GetHotelsAsync failed!");     
            return null;
        }
    }
}
