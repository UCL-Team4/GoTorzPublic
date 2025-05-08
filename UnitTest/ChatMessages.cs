using GoTorz.Data;
using GoTorz.Models.Chat;
using GoTorz.Services;
using Microsoft.EntityFrameworkCore;

namespace UnitTest;

[TestClass]
public class ChatMessages
{
    private ApplicationDbContext _context;
    private MessageService _service;

    [TestInitialize]
    public void TestInitialize()
    {
        // Use an in-memory database so data can be created and disposed without affecting a real database.
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _service = new MessageService(_context);
    }


    [TestCleanup]
    public void TestCleanup()
    {
        _context.Dispose();
    }

    [TestMethod]
    public async Task AddPersistantAsyncChatMessage()
    {
        // Arrange
        var msg = new ChatMessage
        {
            UserId = "user1",
            UserName = "Alice",
            Message = "Hello",
            TimeStamp = DateTime.UtcNow
        };

        // Act
        await _service.AddAsync(msg);

        // Assert
        var stored = await _context.ChatMessages.SingleAsync();
        Assert.AreEqual("Alice", stored.UserName);
        Assert.AreEqual("Hello", stored.Message);
    }

    [TestMethod]
    public async Task GetChatMessageHistoryAsync()
    {
        // Arrange
        _context.ChatMessages.AddRange(
            new ChatMessage { TimeStamp = new DateTime(2021, 1, 1), Message = "Old", UserName = "test", UserId = "test3" },
            new ChatMessage { TimeStamp = new DateTime(2021, 2, 1), Message = "New", UserName = "test", UserId = "test3" }
        );
        await _context.SaveChangesAsync();

        // Act
        var history = await _service.GetHistoryAsync();

        // Assert
        Assert.AreEqual(2, history.Count);
        Assert.AreEqual("Old", history[0].Message);
        Assert.AreEqual("New", history[1].Message);
    }
}
