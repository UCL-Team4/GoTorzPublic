using System.ComponentModel.DataAnnotations;

namespace GoTorz.Models.Chat;

public class ChatMessage
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string UserName { get; set; }

    [Required]
    public string Message { get; set; }

    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}
