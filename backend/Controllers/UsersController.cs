using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<UsersController> _logger;

    public UsersController(AppDbContext context, ILogger<UsersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<UserResponse>> CreateUser([FromBody] CreateUserRequest request)
    {
       
        if (string.IsNullOrWhiteSpace(request.Rumuz))
        {
            return BadRequest(new { message = "Rumuz boş olamaz" });
        }

        if (request.Rumuz.Length < 3 || request.Rumuz.Length > 20)
        {
            return BadRequest(new { message = "Rumuz 3-20 karakter arasında olmalıdır" });
        }

        var ayniRumuzluKullanici = await _context.Users
            .Where(kullanici => kullanici.Rumuz == request.Rumuz)
            .FirstOrDefaultAsync();

        if (ayniRumuzluKullanici != null)
        {
            return Conflict(new { message = "Bu rumuz zaten kullanılıyor" });
        }

      
        var user = new User
        {
            Rumuz = request.Rumuz,
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Kullanıcı şu rumuz ile oluşturuldu : {Rumuz}", user.Rumuz);

        var response = new UserResponse
        {
            Id = user.Id,
            Rumuz = user.Rumuz,
            CreatedAt = user.CreatedAt
        };

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, response);
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
    {
     
        var tumKullanicilar = await _context.Users
            .OrderBy(k => k.CreatedAt)
            .ToListAsync();

        var kullaniciListesi = tumKullanicilar.Select(k => new UserResponse
        {
            Id = k.Id,
            Rumuz = k.Rumuz,
            CreatedAt = k.CreatedAt
        }).ToList();

        return Ok(kullaniciListesi);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetUser(int id)
    {
   
        var bulunanKullanici = await _context.Users.FindAsync(id);

        if (bulunanKullanici == null)
        {
            return NotFound(new { message = "Kullanıcı bulunamadı" });
        }

    
        var kullaniciCevabi = new UserResponse
        {
            Id = bulunanKullanici.Id,
            Rumuz = bulunanKullanici.Rumuz,
            CreatedAt = bulunanKullanici.CreatedAt
        };

        return Ok(kullaniciCevabi);
    }
}

public class CreateUserRequest
{
    public string Rumuz { get; set; } = string.Empty;
}

public class UserResponse
{
    public int Id { get; set; }
    public string Rumuz { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
