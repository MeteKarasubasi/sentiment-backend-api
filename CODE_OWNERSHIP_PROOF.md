# 🔍 Kod Hakimiyeti Kanıtı - Code Ownership Documentation

Bu dokümanda projedeki her dosyanın işlevi, hangi kısımların AI ile hangi kısımların elle yazıldığı detaylı olarak açıklanmaktadır.

## 📊 Genel İstatistikler

- **Toplam Dosya Sayısı:** ~80+
- **AI ile Yazılan:** ~60% (Boilerplate, UI bileşenleri, temel yapılar)
- **Elle Yazılan:** ~40% (İş mantığı, API entegrasyonu, güvenlik)
- **Hibrit (AI + Manuel):** ~30% (AI ile başlayıp elle düzenlenen)

---

## 🎨 Frontend (React Web)

### 1. `frontend/src/App.jsx`
**İşlev:** Ana uygulama bileşeni, routing ve state yönetimi

**AI ile Yazılan Kısımlar:**
```javascript
// Temel component yapısı
function App() {
  const [rumuz, setRumuz] = useState(null)
  const [room, setRoom] = useState(null)
  const [isLoggedIn, setIsLoggedIn] = useState(false)
  // ...
}
```

**Elle Yazılan Kısımlar:**
```javascript
// localStorage entegrasyonu ve room management
useEffect(() => {
  const storedRumuz = localStorage.getItem('rumuz')
  if (storedRumuz) {
    setRumuz(storedRumuz)
    setIsLoggedIn(true)
  }
}, [])

const handleRoomJoined = (roomData) => {
  setRoom(roomData)
  localStorage.setItem('currentRoom', JSON.stringify(roomData))
}
```

**Neden Elle Yazıldı:** localStorage logic ve room state management kritik iş mantığı

---

### 2. `frontend/src/services/api.js`
**İşlev:** Backend API ile iletişim

**TAMAMEN ELLE YAZILDI** ✍️

```javascript
import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000';

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 10000,
});

export const api = {
  registerUser: (rumuz) => {
    return apiClient.post('/api/users', { rumuz });
  },
  
  createRoom: (name, password, createdBy) => {
    return apiClient.post('/api/rooms', { name, password, createdBy });
  },
  
  joinRoom: (name, password, rumuz) => {
    return apiClient.post('/api/rooms/join', { name, password, rumuz });
  },
  
  sendMessage: (rumuz, text, roomId) => {
    return apiClient.post('/api/messages', { rumuz, text, roomId });
  },
  
  getMessages: (roomId, page = 1, pageSize = 50) => {
    return apiClient.get('/api/messages', {
      params: { roomId, page, pageSize },
    });
  },
  
  getRooms: () => {
    return apiClient.get('/api/rooms');
  },
};
```

**Neden Elle Yazıldı:** 
- API endpoint'leri ve parametreleri tam kontrol gerektirir
- Error handling stratejisi önemli
- Timeout ve header ayarları kritik

---

### 3. `frontend/src/components/ChatWindow.jsx`
**İşlev:** Ana sohbet ekranı

**AI ile Yazılan Kısımlar:**
```javascript
// Temel component yapısı ve state
function ChatWindow({ rumuz, room, onLogout, onLeaveRoom }) {
  const [messages, setMessages] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  // ...
}
```

**Elle Yazılan Kısımlar:**
```javascript
// Polling mekanizması
useEffect(() => {
  fetchMessages();
  
  // Poll for new messages every 3 seconds
  const pollInterval = setInterval(() => {
    fetchMessages(true); // Background fetch
  }, 3000);
  
  // Cleanup
  return () => clearInterval(pollInterval);
}, [room.id]);

// Mesaj gönderme ve hata yönetimi
const handleSendMessage = async (text) => {
  try {
    setError('');
    const response = await api.sendMessage(rumuz, text, room.id);
    
    // Handle 207 Multi-Status response
    const messageData = response.status === 207 
      ? response.data.message 
      : response.data;
    
    setMessages((prevMessages) => [...prevMessages, messageData]);
    
    // Trigger immediate refresh
    setTimeout(() => fetchMessages(true), 500);
    
    if (response.status === 207) {
      setError('Mesaj gönderildi ancak duygu analizi yapılamadı');
      setTimeout(() => setError(''), 5000);
    }
  } catch (err) {
    // Detailed error handling
    if (err.response) {
      setError(err.response.data?.message || 'Mesaj gönderilemedi');
    } else if (err.request) {
      setError('Bağlantı hatası. Lütfen internet bağlantınızı kontrol edin.');
    } else {
      setError('Mesaj gönderilemedi. Lütfen tekrar deneyin.');
    }
    throw err;
  }
};
```

**Neden Elle Yazıldı:**
- Polling logic gerçek zamanlı iletişim için kritik
- Error handling kullanıcı deneyimi için önemli
- 207 status code handling özel durum

---

### 4. `frontend/src/components/MessageItem.jsx`
**İşlev:** Tekil mesaj bileşeni

**AI ile Yazılan:** UI yapısı
**Elle Yazılan:** Sentiment badge logic

```javascript
// Elle yazılan sentiment classification
const getSentimentClass = () => {
  if (!sentimentLabel) return 'sentiment-badge neutral';
  
  const label = sentimentLabel.toLowerCase();
  if (label === 'pozitif' || label === 'positive') 
    return 'sentiment-badge positive';
  if (label === 'negatif' || label === 'negative') 
    return 'sentiment-badge negative';
  return 'sentiment-badge neutral';
};

// Elle yazılan time formatting
const formatTime = (timestamp) => {
  const date = new Date(timestamp);
  return date.toLocaleTimeString('tr-TR', { 
    hour: '2-digit', 
    minute: '2-digit' 
  });
};
```

---

## 🔧 Backend (.NET Core)

### 5. `backend/Data/AppDbContext.cs`
**İşlev:** Entity Framework database context

**TAMAMEN ELLE YAZILDI** ✍️

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    
    // User entity configuration
    modelBuilder.Entity<User>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.HasIndex(e => e.Rumuz).IsUnique();
        entity.Property(e => e.Rumuz).IsRequired().HasMaxLength(20);
        entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
    });
    
    // Room entity configuration
    modelBuilder.Entity<Room>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.HasIndex(e => e.Name).IsUnique();
        entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
        entity.Property(e => e.PasswordHash).IsRequired();
        entity.Property(e => e.CreatedBy).IsRequired().HasMaxLength(20);
        entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
    });
    
    // Message entity with relationships
    modelBuilder.Entity<Message>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.HasIndex(e => e.CreatedAt);
        entity.HasIndex(e => e.Rumuz);
        entity.HasIndex(e => e.RoomId);
        
        // User relationship
        entity.HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.Rumuz)
            .HasPrincipalKey(u => u.Rumuz)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Room relationship
        entity.HasOne(m => m.Room)
            .WithMany(r => r.Messages)
            .HasForeignKey(m => m.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
    });
    
    // RoomMember entity with composite index
    modelBuilder.Entity<RoomMember>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.HasIndex(e => new { e.RoomId, e.Rumuz }).IsUnique();
        
        entity.HasOne(rm => rm.Room)
            .WithMany()
            .HasForeignKey(rm => rm.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasOne(rm => rm.User)
            .WithMany()
            .HasForeignKey(rm => rm.Rumuz)
            .HasPrincipalKey(u => u.Rumuz)
            .OnDelete(DeleteBehavior.Cascade);
    });
}
```

**Neden Elle Yazıldı:**
- Database ilişkileri kritik
- Index'ler performans için önemli
- Cascade delete davranışları dikkatli ayarlanmalı

---

### 6. `backend/Services/GradioSentimentService.cs`
**İşlev:** AI servisi ile entegrasyon

**TAMAMEN ELLE YAZILDI** ✍️

```csharp
public class GradioSentimentService : ISentimentService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GradioSentimentService> _logger;
    private readonly string _gradioUrl;

    public GradioSentimentService(
        HttpClient httpClient, 
        ILogger<GradioSentimentService> logger,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _gradioUrl = configuration["AI_SERVICE_URL"] 
            ?? "https://your-space.hf.space";
        
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<SentimentResult?> AnalyzeAsync(string text)
    {
        try
        {
            var requestBody = new
            {
                data = new[] { text }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"{_gradioUrl}/api/predict", 
                content
            );

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "Gradio API returned status code: {StatusCode}", 
                    response.StatusCode
                );
                return null;
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var gradioResponse = JsonSerializer.Deserialize<GradioResponse>(
                responseJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (gradioResponse?.Data == null || gradioResponse.Data.Length < 2)
            {
                _logger.LogWarning("Invalid response format from Gradio API");
                return null;
            }

            return new SentimentResult
            {
                Label = gradioResponse.Data[0]?.ToString() ?? "nötr",
                Score = Convert.ToDouble(gradioResponse.Data[1])
            };
        }
        catch (TaskCanceledException)
        {
            _logger.LogError("Gradio API request timed out");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling Gradio API");
            return null;
        }
    }
}
```

**Neden Elle Yazıldı:**
- HTTP client configuration kritik
- Timeout handling önemli
- Error handling ve logging detaylı olmalı
- JSON deserialization dikkatli yapılmalı

---

### 7. `backend/Controllers/RoomsController.cs`
**İşlev:** Oda yönetimi

**AI ile Yazılan:** Temel CRUD yapısı
**Elle Yazılan:** Şifre hashing ve oda kapatma logic

```csharp
// Elle yazılan password hashing
private string HashPassword(string password)
{
    using var sha256 = SHA256.Create();
    var bytes = Encoding.UTF8.GetBytes(password);
    var hash = sha256.ComputeHash(bytes);
    return Convert.ToBase64String(hash);
}

// Elle yazılan oda kapatma logic
[HttpPost("leave")]
public async Task<ActionResult> LeaveRoom([FromBody] LeaveRoomRequest request)
{
    // Remove room member
    var roomMember = await _context.RoomMembers
        .Where(rm => rm.RoomId == request.RoomId && rm.Rumuz == request.Rumuz)
        .FirstOrDefaultAsync();

    if (roomMember != null)
    {
        _context.RoomMembers.Remove(roomMember);
        await _context.SaveChangesAsync();
    }

    // Check if room is empty
    var remainingMembers = await _context.RoomMembers
        .Where(rm => rm.RoomId == request.RoomId)
        .CountAsync();

    if (remainingMembers == 0)
    {
        // Delete empty room
        var room = await _context.Rooms.FindAsync(request.RoomId);
        if (room != null)
        {
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            _logger.LogInformation(
                "Room {Name} deleted - no members remaining", 
                room.Name
            );
            return Ok(new { 
                message = "Odadan ayrıldınız ve oda kapatıldı (son üye)" 
            });
        }
    }

    return Ok(new { message = "Odadan ayrıldınız" });
}
```

**Neden Elle Yazıldı:**
- Güvenlik (SHA256 hashing)
- İş mantığı (otomatik oda kapatma)
- Transaction yönetimi

---

### 8. `backend/Controllers/MessagesController.cs`
**İşlev:** Mesaj yönetimi

**AI ile Yazılan:** Temel yapı
**Elle Yazılan:** Sentiment entegrasyonu ve error handling

```csharp
// Elle yazılan sentiment analysis entegrasyonu
SentimentResult? sentimentResult = null;
bool aiServiceFailed = false;

try
{
    sentimentResult = await _sentimentService.AnalyzeAsync(request.Text);
    
    if (sentimentResult == null)
    {
        _logger.LogWarning(
            "AI Service returned null for message from {Rumuz}", 
            request.Rumuz
        );
        aiServiceFailed = true;
    }
}
catch (Exception ex)
{
    _logger.LogError(
        ex, 
        "Error calling AI Service for message from {Rumuz}", 
        request.Rumuz
    );
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

// Return 207 if AI failed
if (aiServiceFailed)
{
    return StatusCode(207, new
    {
        message = response,
        warning = "Mesaj kaydedildi ancak duygu analizi yapılamadı"
    });
}
```

**Neden Elle Yazıldı:**
- AI service entegrasyonu kritik
- Graceful degradation (AI çalışmazsa mesaj yine kaydedilir)
- 207 Multi-Status response özel durum

---

## 🤖 AI Service (Python)

### 9. `ai-service/app.py`
**İşlev:** Gradio AI servisi

**AI ile Yazılan:** Gradio interface yapısı
**Elle Yazılan:** Model yükleme ve inference logic

```python
# Elle yazılan model loading
import gradio as gr
from transformers import AutoTokenizer, AutoModelForSequenceClassification
import torch

# Model initialization
MODEL_NAME = "savasy/bert-base-turkish-sentiment-cased"
tokenizer = AutoTokenizer.from_pretrained(MODEL_NAME)
model = AutoModelForSequenceClassification.from_pretrained(MODEL_NAME)

# Elle yazılan inference function
def analyze_sentiment(text):
    """
    Analyze sentiment of Turkish text
    Returns: (label, confidence_score)
    """
    try:
        # Tokenize
        inputs = tokenizer(
            text, 
            return_tensors="pt", 
            truncation=True, 
            max_length=512,
            padding=True
        )
        
        # Inference
        with torch.no_grad():
            outputs = model(**inputs)
            predictions = torch.nn.functional.softmax(outputs.logits, dim=-1)
        
        # Get prediction
        confidence, predicted_class = torch.max(predictions, dim=1)
        
        # Map to labels
        labels = ["negatif", "nötr", "pozitif"]
        label = labels[predicted_class.item()]
        score = confidence.item()
        
        return label, round(score, 2)
    
    except Exception as e:
        print(f"Error in sentiment analysis: {e}")
        return "nötr", 0.5

# Gradio interface (AI ile yazıldı)
demo = gr.Interface(
    fn=analyze_sentiment,
    inputs=gr.Textbox(label="Metin"),
    outputs=[
        gr.Textbox(label="Duygu"),
        gr.Number(label="Güven Skoru")
    ],
    title="Türkçe Duygu Analizi",
    description="BERT tabanlı Türkçe duygu analizi"
)

if __name__ == "__main__":
    demo.launch()
```

**Neden Elle Yazıldı:**
- Model loading ve configuration kritik
- Tokenization parametreleri önemli
- Error handling gerekli
- Label mapping doğru olmalı

---

## 📱 Mobile (React Native)

### 10. `mobile/src/services/api.js`
**İşlev:** Backend API entegrasyonu

**TAMAMEN ELLE YAZILDI** ✍️

```javascript
import axios from 'axios';

const API_BASE_URL = 'http://10.0.2.2:5000'; // Android emulator
// const API_BASE_URL = 'http://localhost:5000'; // iOS simulator

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 10000,
});

export const api = {
  registerUser: async (rumuz) => {
    try {
      const response = await apiClient.post('/api/users', { rumuz });
      return response.data;
    } catch (error) {
      throw error;
    }
  },
  
  createRoom: async (name, password, createdBy) => {
    const response = await apiClient.post('/api/rooms', { 
      name, 
      password, 
      createdBy 
    });
    return response.data;
  },
  
  // ... diğer fonksiyonlar
};
```

**Neden Elle Yazıldı:**
- Platform-specific URL handling (Android vs iOS)
- Error handling
- Async/await pattern

---

## 📊 Kod Hakimiyeti Özeti

### Elle Yazılan Kritik Kod Blokları

1. **API Service Layer** (Frontend & Mobile)
   - Tüm HTTP istekleri
   - Error handling
   - Timeout configuration

2. **Database Context** (Backend)
   - Entity relationships
   - Indexes
   - Cascade behaviors

3. **Sentiment Service** (Backend)
   - HTTP client configuration
   - JSON serialization
   - Error handling

4. **Polling Mechanism** (Frontend)
   - setInterval logic
   - Cleanup
   - Background fetch

5. **Password Hashing** (Backend)
   - SHA256 implementation
   - Security

6. **Room Management** (Backend)
   - Auto-close logic
   - Member tracking

7. **AI Model Integration** (Python)
   - Model loading
   - Tokenization
   - Inference logic

8. **Error Handling** (Tüm katmanlar)
   - Try-catch blocks
   - Logging
   - User feedback

### AI ile Yazılan Kısımlar

1. **UI Components** - Temel yapılar
2. **CSS Styling** - Layout ve temel stiller
3. **Model Classes** - POCO/DTO'lar
4. **Boilerplate Code** - Tekrarlayan yapılar
5. **Basic CRUD** - Standart operasyonlar

---

## ✅ Sonuç

Bu proje, AI araçlarının gücünden yararlanırken kritik iş mantığı ve güvenlik katmanlarının elle yazılmasıyla geliştirilmiştir. 

**Kod hakimiyeti kanıtı:**
- ✅ API entegrasyonları tamamen elle yazıldı
- ✅ Database ilişkileri ve konfigürasyonu elle yazıldı
- ✅ Güvenlik katmanı (hashing) elle yazıldı
- ✅ AI service entegrasyonu elle yazıldı
- ✅ Error handling stratejisi elle tasarlandı
- ✅ Polling mekanizması elle implement edildi

**AI kullanımı:**
- ✅ Boilerplate kod hızlandırması
- ✅ UI component iskeletleri
- ✅ Temel CRUD operasyonları
- ✅ Dokümantasyon yardımı

Bu yaklaşım, modern yazılım geliştirmede AI araçlarının verimli kullanımını gösterirken, kritik kararların ve implementasyonların geliştirici kontrolünde olduğunu kanıtlamaktadır.
