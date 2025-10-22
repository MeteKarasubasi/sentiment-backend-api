using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.Services;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ISentimentService _sentimentService;
    private readonly ILogger<MessagesController> _logger;

    public MessagesController(
        AppDbContext context, 
        ISentimentService sentimentService,
        ILogger<MessagesController> logger)
    {
        _context = context;
        _sentimentService = sentimentService;
        _logger = logger;
    }

    // POST: api/messages
    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreateMessage([FromBody] CreateMessageRequest request)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(request.Rumuz))
        {
            return BadRequest(new { message = "Rumuz boş olamaz" });
        }

        if (string.IsNullOrWhiteSpace(request.Text))
        {
            return BadRequest(new { message = "Mesaj metni boş olamaz" });
        }

        if (request.RoomId <= 0)
        {
            return BadRequest(new { message = "Geçerli bir oda seçilmeli" });
        }

        // Bu rumuzla kayıtlı bir kullanıcı var mı kontrol et, yoksa oluştur
        var kullanici = await _context.Users
            .Where(k => k.Rumuz == request.Rumuz)
            .FirstOrDefaultAsync();
        
        if (kullanici == null)
        {
            // Kullanıcı yoksa otomatik oluştur
            kullanici = new User
            {
                Rumuz = request.Rumuz,
                CreatedAt = DateTime.UtcNow
            };
            _context.Users.Add(kullanici);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Auto-created user: {Rumuz}", request.Rumuz);
        }

        // Oda var mı kontrol et
        var odaVarMi = await _context.Rooms
            .Where(r => r.Id == request.RoomId)
            .AnyAsync();
        
        if (!odaVarMi)
        {
            return BadRequest(new { message = "Oda bulunamadı" });
        }

        // Call AI Service for sentiment analysis
        SentimentResult? sentimentResult = null;
        bool aiServiceFailed = false;

        try
        {
            sentimentResult = await _sentimentService.AnalyzeAsync(request.Text);
            
            if (sentimentResult == null)
            {
                _logger.LogWarning("AI Service returned null for message from {Rumuz}", request.Rumuz);
                aiServiceFailed = true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling AI Service for message from {Rumuz}", request.Rumuz);
            aiServiceFailed = true;
        }

        // Create message with or without sentiment
        var message = new Message
        {
            Rumuz = request.Rumuz,
            Text = request.Text,
            RoomId = request.RoomId,
            SentimentLabel = sentimentResult?.Label,
            SentimentScore = sentimentResult?.Score,
            CreatedAt = DateTime.UtcNow
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Message created with ID: {Id}, Rumuz: {Rumuz}, Sentiment: {Sentiment}", 
            message.Id, message.Rumuz, message.SentimentLabel ?? "null");

        var response = new MessageResponse
        {
            Id = message.Id,
            Rumuz = message.Rumuz,
            Text = message.Text,
            SentimentLabel = message.SentimentLabel,
            SentimentScore = message.SentimentScore,
            CreatedAt = message.CreatedAt
        };

        // Return 207 Multi-Status if AI Service was unavailable
        if (aiServiceFailed)
        {
            return StatusCode(207, new
            {
                message = response,
                warning = "Mesaj kaydedildi ancak duygu analizi yapılamadı"
            });
        }

        return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, response);
    }

    // GET: api/messages
    [HttpGet]
    public async Task<ActionResult<PagedMessagesResponse>> GetMessages(
        [FromQuery] int roomId,
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 50)
    {
        // Validate room ID
        if (roomId <= 0)
        {
            return BadRequest(new { message = "Geçerli bir oda ID'si gerekli" });
        }

        // Validate pagination parameters
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize < 1 || pageSize > 100)
        {
            pageSize = 50;
        }

        // Önce toplam mesaj sayısını öğren (sadece bu odadaki)
        var toplamMesajSayisi = await _context.Messages
            .Where(m => m.RoomId == roomId)
            .CountAsync();

        // Sayfalama için kaç mesaj atlayacağımızı hesapla
        int atlanacakMesajSayisi = (page - 1) * pageSize;

        // Mesajları en eskiden en yeniye doğru sıralayıp, sayfalama uygula (sadece bu odadaki)
        var mesajListesi = await _context.Messages
            .Where(m => m.RoomId == roomId)
            .OrderBy(mesaj => mesaj.CreatedAt)
            .Skip(atlanacakMesajSayisi)
            .Take(pageSize)
            .ToListAsync();

        // Response objelerine dönüştür
        var mesajCevaplari = mesajListesi.Select(m => new MessageResponse
        {
            Id = m.Id,
            Rumuz = m.Rumuz,
            Text = m.Text,
            SentimentLabel = m.SentimentLabel,
            SentimentScore = m.SentimentScore,
            CreatedAt = m.CreatedAt
        }).ToList();

        // Sayfa bilgileriyle birlikte cevabı hazırla
        var sayfaliCevap = new PagedMessagesResponse
        {
            Messages = mesajCevaplari,
            Page = page,
            PageSize = pageSize,
            TotalCount = toplamMesajSayisi,
            TotalPages = (int)Math.Ceiling(toplamMesajSayisi / (double)pageSize)
        };

        return Ok(sayfaliCevap);
    }

    // GET: api/messages/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<MessageResponse>> GetMessage(int id)
    {
        // ID'ye göre mesajı bul
        var bulunanMesaj = await _context.Messages.FindAsync(id);

        if (bulunanMesaj == null)
        {
            return NotFound(new { message = "Mesaj bulunamadı" });
        }

        // Mesaj bilgilerini response'a aktar
        var mesajCevabi = new MessageResponse
        {
            Id = bulunanMesaj.Id,
            Rumuz = bulunanMesaj.Rumuz,
            Text = bulunanMesaj.Text,
            SentimentLabel = bulunanMesaj.SentimentLabel,
            SentimentScore = bulunanMesaj.SentimentScore,
            CreatedAt = bulunanMesaj.CreatedAt
        };

        return Ok(mesajCevabi);
    }

    // GET: api/messages/health
    [HttpGet("health")]
    public IActionResult GetHealth()
    {
        return Ok(new
        {
            status = "healthy",
            timestamp = DateTime.UtcNow,
            service = "MessagesController"
        });
    }
}

// DTOs
public class CreateMessageRequest
{
    public string Rumuz { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public int RoomId { get; set; }
}

public class MessageResponse
{
    public int Id { get; set; }
    public string Rumuz { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string? SentimentLabel { get; set; }
    public double? SentimentScore { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class PagedMessagesResponse
{
    public List<MessageResponse> Messages { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
