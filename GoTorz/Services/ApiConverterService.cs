using GoTorz.Models.API;
using GoTorz.Models.Booking;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GoTorz.Services
{
    // This class converts the API classes to Booking classes which are used in the DB.
    public class ApiConverterService
    {
        // This class should probably run synchronously?
        public async Task<List<Flight>> ConvertFlightApiToBooking(FlightApi flightApi)
        {
            List<Flight> ConvertedFlights = [];

            var flightOffers = flightApi.Data?.FlightOffers;

            if (flightOffers == null)
                return ConvertedFlights;

            foreach (var flightOffer in flightOffers)
            {
                Flight newFlight = new()
                {
                    FlightNumber = flightOffer.Segments?[0]?.Legs?[0]?.FlightInfo?.FlightNumber.ToString() ?? String.Empty,
                    DepartureAirport = flightOffer.Segments?[0]?.Legs?[0]?.DepartureAirport?.Code ?? String.Empty,
                    ArrivalAirport = flightOffer.Segments?[0]?.Legs?[0]?.ArrivalAirport?.Code ?? String.Empty,
                    ArrivalCity = flightOffer.Segments?[0]?.Legs?[0]?.ArrivalAirport?.CityName ?? String.Empty,
                    DepartureTime = flightOffer.Segments[0].Legs[0].DepartureTime,
                    ArrivalTime = flightOffer.Segments[0].Legs[0].ArrivalTime,
                    Airline = flightOffer.Segments?[0]?.Legs?[0]?.CarriersData?[0]?.Name ?? String.Empty,
                    Price = Convert.ToDecimal(flightOffer.PriceBreakdown.Total.Units)
                };

                ConvertedFlights.Add(newFlight);
            }

            return ConvertedFlights;
        }

        // This class should probably run synchronously?
        public async Task<List<Hotel>> ConvertHotelApiToBooking(HotelApi hotelApi)
        {
            List<Hotel> ConvertedHotels = [];
            
            var hotels = hotelApi.Data?.Hotels;

            if (hotels == null)
                return ConvertedHotels;

            foreach (var hotel in hotels )
            {
                 Hotel newHotel = new()
                {
                    Name = hotel.Property?.Name ?? String.Empty,
                    Country = hotel.Property?.CountryCode ?? String.Empty,                  
                    //StarRating = hotel.Property?.ReviewScore
                };

                ConvertedHotels.Add(newHotel);
            }
            
            return ConvertedHotels;
        }
    }
}
