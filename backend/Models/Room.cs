using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Room
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    [Required]
    [StringLength(20)]
    public string CreatedBy { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
