using GoTorz.Data;
using Microsoft.AspNetCore.Identity;
using GoTorz.Models.User;
using System.Linq;

namespace GoTorz.Services;

public class UserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    private IEnumerable<IdentityError> _identityErrors;

    // Minimal constructor: no longer need the DbContext or IUserStore
    public UserService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // You can simplify CreateUser() to just "new ApplicationUser()" if you'd like
    private ApplicationUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<ApplicationUser>();
        }
        catch
        {
            throw new InvalidOperationException(
                $"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class " +
                $"and has a parameterless constructor."
            );
        }
    }

    public async Task<(string UserId, string Error)> CreateUserAsync(UserInput userInput)
    {
        var user = CreateUser();

        user.UserName = userInput.Email;
        user.Email = userInput.Email;

        var result = await _userManager.CreateAsync(user, userInput.Password);

        if (!result.Succeeded)
        {
            // Return null as the UserId and the first error message as Error
            return (null, result.Errors.FirstOrDefault().Description);
        }

        var userId = await _userManager.GetUserIdAsync(user);

        // Return the userId and a null error to indicate success
        return (userId, null);
    }

    public async Task<ApplicationUser> FindByEmailAsync(string email)
    {
        // Rely on userManager to find your user
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<ApplicationUser> FindByNameAsync(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }
}
