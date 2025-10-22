using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 3)]
    public string Rumuz { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
