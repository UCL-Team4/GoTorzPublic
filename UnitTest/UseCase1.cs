using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoTorz.Data;
using GoTorz.Services;
using GoTorz.Models.API;

namespace UnitTest
{
    [TestClass]
    public sealed class PackageServiceTests
    {
        private ApplicationDbContext _context;
        private PackageService _packageService;

        [TestInitialize]
        public void TestInitialize()
        {
            // Use an in-memory database so data can be created and disposed without affecting a real database.
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            // Seed test data.
            SeedDatabase();

            _packageService = new PackageService(_context);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
        }

        [TestMethod]
        public async Task SearchPackages_WithDepartureAndLocation_ReturnsExpectedPackage()
        {
            // Arrange: Search using departure "JFK" and location "Los Angeles" (should return package1 from seed data).
            var request = new SearchPackagesRequest
            {
                Departure = "JFK",
                Location = "Los Angeles"
            };

            // Act
            List<Package> results = await _packageService.SearchPackagesAsync(request);

            // Assert
            Assert.IsNotNull(results, "Result should not be null.");
            Assert.AreEqual(1, results.Count, "There should be one matching package for Los Angeles.");
            Assert.AreEqual("Los Angeles", results.First().Hotel.City, "Hotel city should be 'Los Angeles'.");
        }

        [TestMethod]
        public async Task SearchPackages_WithNonExistingLocation_ReturnsEmptyList()
        {
            // Arrange: Valid departure but a location that does not exist in test data.
            var request = new SearchPackagesRequest
            {
                Departure = "JFK",
                Location = "NonExistingCity"
            };

            // Act
            List<Package> results = await _packageService.SearchPackagesAsync(request);

            // Assert
            Assert.IsNotNull(results, "Result should not be null.");
            Assert.AreEqual(0, results.Count, "No packages should match when the location does not exist.");
        }

        [TestMethod]
        public async Task SearchPackages_WithCaseInsensitiveLocation_ReturnsExpectedPackage()
        {
            // Arrange: Provide location in a different case ("los angeles" instead of "Los Angeles")
            var request = new SearchPackagesRequest
            {
                Departure = "JFK",
                Location = "los angeles"
            };

            // Act
            List<Package> results = await _packageService.SearchPackagesAsync(request);

            // Assert
            Assert.IsNotNull(results, "Result should not be null.");
            Assert.AreEqual(1, results.Count, "There should be one matching package for 'los angeles'.");
            Assert.AreEqual("Los Angeles", results.First().Hotel.City, "Hotel city should be 'Los Angeles'.");
        }

        [TestMethod]
        public async Task SearchPackages_WithMultipleMatches_ReturnsAllMatches()
        {
            // Arrange: Add an extra package to test the multiple-results scenario.
            // This extra package has departure "JFK" and location "Paris" (in addition to one already in seed data).
            var flightExtra = new Flight
            {
                FlightId = 3,
                FlightNumber = "FL789",
                Price = 200,
                DepartureAirport = "JFK",
                ArrivalAirport = "CDG",
                ArrivalCity = "Paris",
                Airline = "Norwegian Air",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(7)
            };
            _context.Flights.Add(flightExtra);

            var hotelParis = _context.Hotels.First(h => h.City.Equals("Paris", StringComparison.OrdinalIgnoreCase));
            var packageExtra = new Package
            {
                PackageId = 3,
                Flight = flightExtra,
                FlightId = flightExtra.FlightId,
                Hotel = hotelParis,
                HotelId = hotelParis.HotelId,
                Nights = 2,
                CommissionPercentage = 10
            };
            _context.Packages.Add(packageExtra);
            _context.SaveChanges();

            // Now search for packages using departure "JFK" and location "Paris"
            var request = new SearchPackagesRequest
            {
                Departure = "JFK",
                Location = "Paris"
            };

            // Act
            List<Package> results = await _packageService.SearchPackagesAsync(request);

            // Assert: Expect both the seeded package and the extra one to match.
            Assert.IsNotNull(results, "Result should not be null.");
            Assert.AreEqual(2, results.Count, "There should be two matching packages for 'Paris'.");
        }

        [TestMethod]
        public async Task SearchPackages_WithMissingLocation_ReturnsAllDepartureMatches()
        {
            // Arrange: Only provide departure ("JFK"). If location is missing,
            // our logic should return all packages with that departure.
            var request = new SearchPackagesRequest
            {
                Departure = "JFK",
                // Location is not provided (or an empty string)
                Location = string.Empty
            };

            // Act
            List<Package> results = await _packageService.SearchPackagesAsync(request);

            // Assert: From seed data, there are two packages with departure "JFK".
            Assert.IsNotNull(results, "Result should not be null.");
            Assert.AreEqual(2, results.Count, "There should be two matching packages for 'JFK' when location is not specified.");
        }

        [TestMethod]
        public async Task SearchPackages_WithMissingDeparture_ReturnsResults()
        {
            // Arrange: Provide a request without a departure value.
            // Expecting that our service will still return at least one result.
            var request = new SearchPackagesRequest
            {
                Departure = null,
                Location = "Paris"
            };

            // Act
            List<Package> results = await _packageService.SearchPackagesAsync(request);

            // Assert
            Assert.IsNotNull(results, "Result should not be null.");
            Assert.IsTrue(results.Count > 0, "There should be at least one matching package when departure is missing.");
        }

        // Helper method to seed the in-memory database with initial test data.
        private void SeedDatabase()
        {
            // Create two flights
            var flight1 = new Flight
            {
                FlightId = 1,
                FlightNumber = "FL123",
                Price = 100,
                DepartureAirport = "JFK",
                ArrivalAirport = "LAX",
                DepartureTime = DateTime.Now,
                Airline = "Delta",
                ArrivalCity = "Los Angeles",
                ArrivalTime = DateTime.Now.AddHours(5)
            };
            var flight2 = new Flight
            {
                FlightId = 2,
                FlightNumber = "FL456",
                Price = 150,
                DepartureAirport = "JFK",
                ArrivalAirport = "CDG",
                Airline = "Delta",
                ArrivalCity = "Paris",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(7)
            };
            _context.Flights.AddRange(flight1, flight2);

            // Create hotels
            var hotel1 = new Hotel
            {
                HotelId = 1,
                Name = "LA Hotel",
                City = "Los Angeles",
                Country = "USA",
                Address = "1234 LA Street",
                PricePerNight = 80
            };
            var hotel2 = new Hotel
            {
                HotelId = 2,
                Name = "Paris Inn",
                City = "Paris",
                Address = "1234 Paris Street",
                Country = "France",
                PricePerNight = 120
            };
            _context.Hotels.AddRange(hotel1, hotel2);

            // Create packages from the flights and hotels.
            var package1 = new Package
            {
                PackageId = 1,
                Flight = flight1,
                FlightId = flight1.FlightId,
                Hotel = hotel1,
                HotelId = hotel1.HotelId,
                Nights = 3,
                CommissionPercentage = 10
            };
            var package2 = new Package
            {
                PackageId = 2,
                Flight = flight2,
                FlightId = flight2.FlightId,
                Hotel = hotel2,
                HotelId = hotel2.HotelId,
                Nights = 4,
                CommissionPercentage = 10
            };
            _context.Packages.AddRange(package1, package2);

            _context.SaveChanges();
        }
    }
}
