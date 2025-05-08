using GoTorz.Data;
using GoTorz.Models.API;
using Microsoft.EntityFrameworkCore;

namespace GoTorz.Services;

public class PackageService
{
    private readonly ApplicationDbContext _context;

    public PackageService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Package>> SearchPackagesAsync(SearchPackagesRequest request)
    {
        if (request == null)
        {
            return new List<Package>();
        }

        IQueryable<Package> query = _context.Packages
            .Include(p => p.Flight)
            .Include(p => p.Hotel);

        if (!string.IsNullOrWhiteSpace(request.Departure))
        {
            query = query.Where(p => p.Flight.DepartureAirport != null &&
                                     p.Flight.DepartureAirport.ToLower() == request.Departure.ToLower());
        }

        if (!string.IsNullOrWhiteSpace(request.Location))
        {
            query = query.Where(p => p.Hotel.City != null &&
                                     p.Hotel.City.ToLower() == request.Location.ToLower());
        }

        return await query.ToListAsync();
    }


    public async Task<List<Package>> SearchPackagesByDestinationAsync(string destination)
    {
        if (string.IsNullOrWhiteSpace(destination))
        {
            return new List<Package>();
        }

        List<Package> packages = _context.Packages
            .Include(p => p.Flight)
            .Include(p => p.Hotel)
            .Where(p => p.Hotel.City.ToLower() == destination.ToLower())
            .ToList();


        return packages;
    }

    public async Task<List<Package>> SearchPackageFromDestinationAsync(string destination)
    {
        if (string.IsNullOrWhiteSpace(destination))
        {
            return new List<Package>();
        }

        List<Package> packages = _context.Packages
            .Include(p => p.Flight)
            .Include(p => p.Hotel)
            .Where(p => p.Flight.DepartureAirport.ToLower() == destination.ToLower())
            .ToList();


        return packages;
    }

    public async Task<List<Package>> GetCompleteDestinationPackageAsync(string departure, string destination)
    {
        if (string.IsNullOrWhiteSpace(destination) || string.IsNullOrWhiteSpace(departure))
        {
            return new List<Package>();
        }

        List<Package> packages = _context.Packages
            .Include(p => p.Flight)
            .Include(p => p.Hotel)
            .Where(p => p.Flight.DepartureAirport.ToLower() == departure.ToLower())
            .Where(p => p.Hotel.City.ToLower() == destination.ToLower())
            .Where(p => p.Flight.ArrivalCity.ToLower() == destination.ToLower())
            .ToList();


        return packages;
    }
}
