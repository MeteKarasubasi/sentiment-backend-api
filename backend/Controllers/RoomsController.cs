using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using System.Security.Cryptography;
using System.Text;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<RoomsController> _logger;

    public RoomsController(AppDbContext context, ILogger<RoomsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // POST: api/rooms - Create a new room
    [HttpPost]
    public async Task<ActionResult<RoomResponse>> CreateRoom([FromBody] CreateRoomRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { message = "Oda adı boş olamaz" });
        }

        if (request.Name.Length < 3 || request.Name.Length > 50)
        {
            return BadRequest(new { message = "Oda adı 3-50 karakter arasında olmalıdır" });
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Şifre boş olamaz" });
        }

        if (request.Password.Length < 4)
        {
            return BadRequest(new { message = "Şifre en az 4 karakter olmalıdır" });
        }

        // Check if room name already exists
        var existingRoom = await _context.Rooms
            .Where(r => r.Name == request.Name)
            .FirstOrDefaultAsync();

        if (existingRoom != null)
        {
            return Conflict(new { message = "Bu oda adı zaten kullanılıyor" });
        }

        // Hash the password
        var passwordHash = HashPassword(request.Password);

        var room = new Room
        {
            Name = request.Name,
            PasswordHash = passwordHash,
            CreatedBy = request.CreatedBy,
            CreatedAt = DateTime.UtcNow
        };

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Room created: {Name} by {CreatedBy}", room.Name, room.CreatedBy);

        var response = new RoomResponse
        {
            Id = room.Id,
            Name = room.Name,
            CreatedBy = room.CreatedBy,
            CreatedAt = room.CreatedAt
        };

        return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, response);
    }

    // POST: api/rooms/join - Join a room with password
    [HttpPost("join")]
    public async Task<ActionResult<RoomResponse>> JoinRoom([FromBody] JoinRoomRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { message = "Oda adı boş olamaz" });
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Şifre boş olamaz" });
        }

        var room = await _context.Rooms
            .Where(r => r.Name == request.Name)
            .FirstOrDefaultAsync();

        if (room == null)
        {
            return NotFound(new { message = "Oda bulunamadı" });
        }

        // Verify password
        var passwordHash = HashPassword(request.Password);
        if (room.PasswordHash != passwordHash)
        {
            return Unauthorized(new { message = "Yanlış şifre" });
        }

        _logger.LogInformation("User {Rumuz} joined room: {Name}", request.Rumuz, room.Name);

        var response = new RoomResponse
        {
            Id = room.Id,
            Name = room.Name,
            CreatedBy = room.CreatedBy,
            CreatedAt = room.CreatedAt
        };

        return Ok(response);
    }

    // GET: api/rooms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomListResponse>>> GetRooms()
    {
        var rooms = await _context.Rooms
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        var response = rooms.Select(r => new RoomListResponse
        {
            Id = r.Id,
            Name = r.Name,
            CreatedBy = r.CreatedBy,
            CreatedAt = r.CreatedAt
        }).ToList();

        return Ok(response);
    }

    // GET: api/rooms/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<RoomResponse>> GetRoom(int id)
    {
        var room = await _context.Rooms.FindAsync(id);

        if (room == null)
        {
            return NotFound(new { message = "Oda bulunamadı" });
        }

        var response = new RoomResponse
        {
            Id = room.Id,
            Name = room.Name,
            CreatedBy = room.CreatedBy,
            CreatedAt = room.CreatedAt
        };

        return Ok(response);
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}

// DTOs
public class CreateRoomRequest
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
}

public class JoinRoomRequest
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Rumuz { get; set; } = string.Empty;
}

public class RoomResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class RoomListResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
