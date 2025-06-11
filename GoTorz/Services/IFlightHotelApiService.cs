using GoTorz.Models.API;

namespace GoTorz.Services
{
    public interface IFlightHotelApiService
    {
        Task<(FlightApi Flights, HotelApi Hotels)> SearchFlightsAndHotelsAsync(string departureAirport, string destinationAirport, string departDateFlight, string destinationId, string arrivalHotel, string departureHotel);
    }
}
