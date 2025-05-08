using Microsoft.AspNetCore.Identity;

namespace GoTorz.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public string SocialSecurityNumber { get; set; }
    }
}
