using GoTorz.Data;
using Microsoft.AspNetCore.Identity;

namespace GoTorz.Services
{
    public class SignInService
    {
        private readonly ApplicationDbContext _context;

        public SignInService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password)
        {
            throw new NotImplementedException();
        }
    }
}
