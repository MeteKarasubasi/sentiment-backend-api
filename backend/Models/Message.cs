using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Message
{
    public int Id { get; set; }
    
    [Required]
    public string Rumuz { get; set; } = string.Empty;
    
    [Required]
    public string Text { get; set; } = string.Empty;
    
    public string? SentimentLabel { get; set; }
    
    public double? SentimentScore { get; set; }
    
    public int RoomId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public User? User { get; set; }
    public Room? Room { get; set; }
}
