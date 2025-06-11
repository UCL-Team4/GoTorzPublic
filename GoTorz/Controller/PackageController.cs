using GoTorz.Services;
using Microsoft.AspNetCore.Mvc;
using GoTorz.Models.Booking;

namespace GoTorz.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly PackageService _packageService;

        public PackageController(PackageService packageService)
        {
            _packageService = packageService;
        }


        [HttpPost("search")]
        public async Task<IActionResult> SearchPackages([FromBody] SearchPackagesRequest request)
        {
            var packages = await _packageService.SearchPackagesAsync(request);
            return Ok(packages);
        }
    }
}
