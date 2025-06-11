using System.Net.Http.Json;
using GoTorz;
using GoTorz.Data;
using GoTorz.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GoTorz.Models.User;
using Microsoft.AspNetCore.Hosting;

namespace UnitTest
{
    [TestClass]
    public sealed class UseCase2
    {
        private ApplicationDbContext _context;
        private UserService _userService;
        // private SignInService _signInService;

        private HttpClient _client;

        [TestInitialize]
        public void TestInitialize()
        {
            //Set up in-memory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("UnitTest");
                    builder.ConfigureServices(services =>
                    {
                        // Add the in-memory provider
                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDbForTesting");
                        });
                    });
                });

            _client = factory.CreateClient();

            _context = new ApplicationDbContext(options);        
            // _signInService = new SignInService(_context);
        }


        [TestMethod]
        public async Task SuccessfulProfileRegistration()
        {
            var userInput = new UserInput
            {
                Email = "test@gmail.com",
                Password = "SecurePassword123!"
            };

            var response = await _client.PostAsJsonAsync("/api/user/register", userInput);

            response.EnsureSuccessStatusCode();

            var strArray = await response.Content.ReadFromJsonAsync<string[]>();

            var userId = strArray[0];

            Assert.IsNotNull(userId, "User registration failed.");
            Assert.IsTrue(userId.Length > 0, "User registration failed.");
        }

        //[TestMethod]
        //public async Task UserNotRegisteredDueToInvalidUserName()
        //{
        //    //Arrange
        //    var newUser = new ApplicationUser
        //    {
        //        UserName = string.Empty,
        //        Email = "user1@example.com"
        //    };

        //    string password = "SecurePassword123!";

        //    //Act
        //    //var result = await _userService.CreateUserAsync(newUser, password);

        //    //Assert
        //    Assert.IsNull(result, "User registration was successful and that is not what we want.");
        //}

        //[TestMethod]
        //public async Task UserNotRegisteredDueToInvalidEmail()
        //{
        //    //Arrange
        //    var newUser = new ApplicationUser
        //    {
        //        UserName = "User2",
        //        Email = "user2example.com"
        //    };

        //    string password = "SecurePassword123!";

        //    //Act
        //    //var result = await _userService.CreateUserAsync(newUser, password);

        //    //Assert
        //    Assert.IsNull(result, "User registration was successful and that is not what we want.");
        //}

        //[TestMethod]
        //public async Task UserNotRegisteredDueToInvalidPassword()
        //{
        //    //Arrange
        //    var newUser = new ApplicationUser
        //    {
        //        UserName = "User3",
        //        Email = "user3@example.com"
        //    };

        //    string password = "123";

        //    //Act
        //    //var result = await _userService.CreateUserAsync(newUser, password);

        //    //Assert
        //    Assert.IsNull(result, "User registration was successful and that is not what we want.");
        //}

        //[TestMethod]
        //public async Task UserNotRegisteredDueToAlreadyExistingUsername()
        //{
        //    //Arrange
        //    var newUser = new ApplicationUser
        //    {
        //        UserName = "testuser@example.com",
        //        Email = "testuser@example.com"
        //    };

        //    string password = "SecurePassword123!";

        //    //Act
        //    // var result = await _userService.CreateUserAsync(newUser, password);

        //    //Assert
        //    Assert.IsNull(result, "User registration was successful and that is not what we want.");
        //}

        //[TestMethod]
        //public async Task UserSuccesfullyAttemptingToSignIn()
        //{
        //    //Arrange
        //    string correctPassword = "StrongPassword123!";

        //    //Act
        //    var testUser = await _userService.FindByNameAsync("testuser@example.com");
        //    var result = await _signInService.PasswordSignInAsync(testUser, correctPassword);
                        
        //    //Assert
        //    Assert.IsNotNull(testUser, "User doesn't exist in DB.");
        //    Assert.IsNotNull(result, "User failed to sign in.");
        //}

        //[TestMethod]
        //public async Task UserUnsuccesfullyAttemptingToLogInDueToWrongPassword()
        //{
        //    //Arrange            
        //    string incorrectPassword = "1234";

        //    //Act
        //    var testUser = await _userService.FindByNameAsync("testuser@example.com");
        //    var result = await _signInService.PasswordSignInAsync(testUser, incorrectPassword);
            
        //    //Assert
        //    Assert.IsNotNull(testUser, "User doesn't exist in DB.");
        //    Assert.IsNull(result, "User succeeded in signing in.");
        //}
    }
}