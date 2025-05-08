using GoTorz.Data;
using GoTorz.Models.Chat;
using Microsoft.EntityFrameworkCore;

namespace GoTorz.Services;

public class MessageService
{
    private readonly ApplicationDbContext _db;

    public MessageService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<ChatMessage>> GetHistoryAsync()
    {
        return await _db.ChatMessages
                    .AsNoTracking()
                    .OrderBy(m => m.TimeStamp)
                    .ToListAsync();
    }

    public async Task AddAsync(ChatMessage message)
    {
        _db.ChatMessages.Add(message);
        await _db.SaveChangesAsync();
    }
}
