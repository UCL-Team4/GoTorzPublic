using System.Linq;
using Azure.Core;
using GoTorz.Data;
using GoTorz.Models.Booking;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace GoTorz.Services
{
    // This service was supposed to make use of the RoleManager for our own methods but it was too difficult to test
    //public class RoleService
    //{
    //    private readonly RoleManager<IdentityRole> _roleManager;
    //    private readonly ApplicationDbContext _context;

    //    public RoleService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
    //    {
    //        _context = context;
    //        _roleManager = roleManager;
    //    }
              
    //    public async Task<List<ApplicationUser>> FindEmployeesByRoleAsync(string role)
    //    {
    //        if (role == null)
    //        {
    //            return [];
    //        }

    //        // Matches the searched role with an actual role
    //        var FindRole = await _roleManager.FindByNameAsync(role);
    //        //var FindRole = _context.Roles.FirstOrDefault(r => r.Name == role);

    //        if (FindRole == null)
    //        {
    //            return [];
    //        }

    //        // Finds the ID of the role
    //        var FindRoleId = await _roleManager.GetRoleIdAsync(FindRole);
    //        //var FindRoleId = FindRole.Id;

    //        // Returns a list of user ids with the specific role
    //        var userIds = _context.UserRoles.Where(r => r.RoleId == FindRoleId).ToList();

    //        List<ApplicationUser> users = [];

    //        // Each userId is matched with a user and added to the list
    //        foreach (var userId in userIds)
    //        {
    //            var user = _context.Users.FirstOrDefault(u => u.Id == userId.UserId.ToString());
    //            if (user != null)
    //            {
    //                users.Add(user);
    //            }
    //        }

    //        // This returns af list of users with the given role         
    //        return users;
    //    }
    //}
}
