using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoTorz.Data;
using GoTorz.Models.Booking;
using GoTorz.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTest
{
    // This Unit Test should proberbly be deleted, as the use of Moq was too difficult to implement
    //[TestClass]
    //public class UseCase3
    //{
    //    private ApplicationDbContext _context;
    //    private RoleService _roleService;
    //    private RoleManager<IdentityRole> _roleManager;
    //    private UserManager<ApplicationUser> _userManager;

    //    [TestInitialize]
    //    public void TestInitialize()
    //    {
    //        // Use an in-memory database so data can be created and disposed without affecting a real database.
    //        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    //            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
    //            .Options;
    //        _context = new ApplicationDbContext(options);

    //        // Use Moq for the role and user managers in the unit test. In the razor objects we can just use dependency injection.
    //        var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();

    //        roleStoreMock.Setup(x => x.CreateAsync(It.IsAny<IdentityRole>(), .ReturnsAsync());
            
    //        _roleManager = new RoleManager<IdentityRole>(
    //            new Mock<IRoleStore<IdentityRole>>().Object,
    //            new List<IRoleValidator<IdentityRole>> { new RoleValidator<IdentityRole>() },
    //            new Mock<ILookupNormalizer>().Object,
    //            new Mock<IdentityErrorDescriber>().Object,
    //            new Mock<ILogger<RoleManager<IdentityRole>>>().Object
    //            );

    //        _userManager = new UserManager<ApplicationUser>(
    //            new Mock<IUserStore<ApplicationUser>>().Object,
    //            new Mock<Microsoft.Extensions.Options.IOptions<IdentityOptions>>().Object,
    //            new Mock<IPasswordHasher<ApplicationUser>>().Object,
    //            new List<IUserValidator<ApplicationUser>> { new UserValidator<ApplicationUser>() },
    //            new List<IPasswordValidator<ApplicationUser>> { new PasswordValidator<ApplicationUser>() },
    //            new Mock<ILookupNormalizer>().Object,
    //            new Mock<IdentityErrorDescriber>().Object,
    //            new Mock<IServiceProvider>().Object,
    //            new Mock<ILogger<UserManager<ApplicationUser>>>().Object
    //            );

            

    //        _roleService = new RoleService(_context, _roleManager);

    //        // Seed test data.
    //        SeedDatabase();
    //    }

    //    [TestCleanup]
    //    public void TestCleanup()
    //    {
    //        _context.Dispose();
    //    }

    //    [TestMethod]
    //    public async Task SearchEmployees_ByAdminRole_ReturnsListOfOne()
    //    {
    //        // Arrange
    //        string search = "Admin";

    //        // Act
    //        List<ApplicationUser> admins = await _roleService.FindEmployeesByRoleAsync(search);

    //        // Assert
    //        Assert.AreEqual(1, admins.Count, "There should be exactly one matching employee who is an admin.");
    //    }

    //    // Helper method to seed the in-memory database with initial test data.
    //    private void SeedDatabase()
    //    {
    //        // Create two users with different roles
    //        var user1 = new ApplicationUser
    //        {
    //            //Id = "1",
    //            UserName = "Hans",
    //            Email = "hans@jensen.dk",
    //            PasswordHash = Guid.NewGuid().ToString(),
    //        };

    //        var user2 = new ApplicationUser
    //        {
    //            //Id= "2",
    //            UserName = "Jens",
    //            Email = "jens@hansen.dk",
    //            PasswordHash = Guid.NewGuid().ToString(),
    //        };

    //        //var adminRole = new IdentityRole
    //        //{
    //        //    Id = "1",
    //        //    Name = "Admin",
    //        //};

    //        //var userRole = new IdentityUserRole<string>
    //        //{
    //        //    UserId = "1",
    //        //    RoleId = "1",
    //        //};

    //        _context.Users.AddRange(user1, user2);
    //        //_context.Roles.Add(adminRole);
    //        //_context.UserRoles.Add(userRole);

    //        _roleManager.CreateAsync(new IdentityRole("Admin"));
    //        _userManager.AddToRoleAsync(user1, "Admin");

    //        _context.SaveChanges();
    //    }
    //}
}
