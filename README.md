# Sentiment Chat - Duygu Analizi ile Sohbet Uygulaması

Kullanıcıların mesajlaşarak sohbet edebildiği, yazışmaların AI tarafından duygu analizi yapılarak canlı olarak gösterildiği full-stack web + mobil uygulama.

## 🎯 Proje Özeti

Bu proje, kullanıcıların rumuz (takma ad) ile kayıt olup mesajlaşabildiği, her mesajın AI tarafından analiz edilerek duygusunun (pozitif/negatif/nötr) belirlendiği bir sohbet platformudur. Tüm servisler ücretsiz platformlarda deploy edilmiştir.

## ✨ Özellikler

- 💬 **Gerçek Zamanlı Mesajlaşma**: Kullanıcılar rumuz ile kayıt olup mesaj gönderebilir
- 🤖 **AI Duygu Analizi**: Her mesaj otomatik olarak analiz edilir (pozitif/negatif/nötr)
- 🌐 **Web Uygulaması**: React ile geliştirilmiş responsive web arayüzü
- 📱 **Mobil Uygulama**: React Native CLI ile geliştirilmiş Android uygulaması
- 🎨 **Renkli Gösterim**: Duygulara göre renk kodlaması (yeşil/kırmızı/gri)
- 📊 **Güven Skoru**: Her duygu analizi için güven yüzdesi gösterimi

## 🏗️ Teknoloji Stack'i

### Frontend (Web)
- **Framework**: React 19 + Vite
- **HTTP Client**: Axios
- **Styling**: CSS3
- **Deployment**: Vercel
- **Test**: Vitest + React Testing Library

### Frontend (Mobile)
- **Framework**: React Native 0.82 (CLI)
- **Navigation**: React Navigation
- **UI**: React Native Paper
- **Platform**: Android (APK)

### Backend API
- **Framework**: .NET Core 8.0 Web API
- **Database**: SQLite + Entity Framework Core
- **Deployment**: Render (Free Web Service)
- **Test**: xUnit

### AI Service
- **Framework**: Python 3.10 + Gradio
- **Model**: cardiffnlp/twitter-roberta-base-sentiment (Hugging Face)
- **Deployment**: Hugging Face Spaces
- **Libraries**: transformers, torch

## 📁 Proje Yapısı

```
sentiment-chat/
├── frontend/              # React web uygulaması
│   ├── src/
│   │   ├── components/   # UI bileşenleri
│   │   ├── services/     # API servisleri
│   │   └── App.jsx       # Ana uygulama
│   └── package.json
│
├── mobile/               # React Native mobil uygulama
│   ├── src/
│   │   ├── screens/     # Ekranlar (Login, Chat)
│   │   ├── components/  # UI bileşenleri
│   │   └── services/    # API servisleri
│   ├── android/         # Android build dosyaları
│   └── package.json
│
├── backend/             # .NET Core API
│   ├── Controllers/     # API endpoint'leri
│   ├── Models/         # Veri modelleri
│   ├── Services/       # İş mantığı servisleri
│   ├── Data/           # Database context
│   └── Program.cs      # Uygulama giriş noktası
│
├── backend.Tests/      # Backend unit testleri
│   └── *.cs
│
└── ai-service/         # Python AI servisi
    ├── app.py          # Gradio uygulaması
    └── requirements.txt
```

## 🚀 Kurulum ve Çalıştırma

### 1. AI Service (Hugging Face Spaces)

AI servisi zaten Hugging Face Spaces'te deploy edilmiştir:
- **URL**: https://huggingface.co/spaces/[your-space-name]

Yerel olarak çalıştırmak için:
```bash
cd ai-service
pip install -r requirements.txt
python app.py
```

### 2. Backend API (.NET Core)

**Gereksinimler**: .NET 8.0 SDK

```bash
cd backend

# Bağımlılıkları yükle
dotnet restore

# Database migration'ları uygula
dotnet ef database update

# Uygulamayı çalıştır
dotnet run
```

Backend http://localhost:5000 adresinde çalışacaktır.

**Environment Variables**:
```bash
AiService__Url=https://your-huggingface-space.hf.space/api/predict
ConnectionStrings__DefaultConnection=Data Source=chat.db
```

### 3. Frontend (Web)

**Gereksinimler**: Node.js 20+

```bash
cd frontend

# Bağımlılıkları yükle
npm install

# Development server'ı başlat
npm run dev
```

Web uygulaması http://localhost:5173 adresinde çalışacaktır.

**Environment Variables** (`.env` dosyası):
```
VITE_API_URL=http://localhost:5000
```

### 4. Mobile (React Native)

**Gereksinimler**: Node.js 20+, JDK 17, Android SDK

```bash
cd mobile

# Bağımlılıkları yükle
npm install

# Android emulator'da çalıştır
npm run android

# Veya release APK build et
cd android
./gradlew assembleRelease
```

APK dosyası: `mobile/android/app/build/outputs/apk/release/app-release.apk`

## 🌐 Demo Linkleri

- **Web Uygulaması**: https://sentiment-chat-frontend.vercel.app
- **Backend API**: https://sentiment-chat-backend.onrender.com
- **AI Service**: https://huggingface.co/spaces/[your-space-name]
- **API Docs**: https://sentiment-chat-backend.onrender.com/swagger
- **Mobile APK**: [Download APK](mobile/android/app/build/outputs/apk/release/app-release.apk)

## 📖 API Endpoint'leri

### Users
- `POST /api/users` - Yeni kullanıcı kaydı (rumuz)
- `GET /api/users` - Tüm kullanıcıları listele
- `GET /api/users/{id}` - Belirli bir kullanıcıyı getir

### Messages
- `POST /api/messages` - Yeni mesaj gönder (AI analizi ile)
- `GET /api/messages` - Mesajları listele (sayfalama ile)
- `GET /api/messages/{id}` - Belirli bir mesajı getir

### Health
- `GET /api/health` - API sağlık kontrolü

## 🧪 Test

### Backend Testleri
```bash
cd backend.Tests
dotnet test
```

### Frontend Testleri
```bash
cd frontend
npm test
```

## 🤖 AI Araçları Kullanımı

Bu projede aşağıdaki AI araçları kullanılmıştır:

### Kiro AI Assistant
- Proje yapısının oluşturulması
- Kod iskeletlerinin hazırlanması
- Test dosyalarının yazılması
- Deployment dokümantasyonları

### Elle Yazılan Kod Bölümleri

Aşağıdaki kritik kod bölümleri AI yardımı olmadan elle yazılmıştır:

#### 1. Database Sorguları (backend/Controllers/)

**UsersController.cs** - Kullanıcı sorgulama:
```csharp
// Önce bu rumuzun daha önce alınıp alınmadığını kontrol edelim
var ayniRumuzluKullanici = await _context.Users
    .Where(kullanici => kullanici.Rumuz == request.Rumuz)
    .FirstOrDefaultAsync();
```

**MessagesController.cs** - Mesaj sayfalama:
```csharp
// Sayfalama için kaç mesaj atlayacağımızı hesapla
int atlanacakMesajSayisi = (page - 1) * pageSize;

// Mesajları en eskiden en yeniye doğru sıralayıp, sayfalama uygula
var mesajListesi = await _context.Messages
    .OrderBy(mesaj => mesaj.CreatedAt)
    .Skip(atlanacakMesajSayisi)
    .Take(pageSize)
    .ToListAsync();
```

#### 2. AI Service Entegrasyonu (backend/Services/SentimentService.cs)

HttpClient ile AI servisine istek atma mantığı:
```csharp
var response = await _httpClient.PostAsJsonAsync(_aiServiceUrl, requestData);
var result = await response.Content.ReadFromJsonAsync<SentimentResponse>();
```

#### 3. Frontend API Çağrıları (frontend/src/services/api.js)

Axios ile backend'e istek gönderme:
```javascript
export const sendMessage = async (rumuz, text) => {
  const response = await api.post('/api/messages', { rumuz, text });
  return response.data;
};
```

## 💡 Kod Hakimiyeti

### Backend (Program.cs)
Ana uygulama yapılandırması:
- CORS ayarları
- Dependency Injection
- Database context
- Swagger/OpenAPI
- Middleware pipeline

### Frontend (App.jsx)
- React state yönetimi
- Component lifecycle
- API entegrasyonu
- Routing mantığı

### AI Service (app.py)
- Gradio interface
- Model yükleme
- Sentiment analizi
- Label mapping (İngilizce → Türkçe)

## 🎨 Ekran Görüntüleri

### Web Uygulaması
![Web Login](docs/screenshots/web-login.png)
![Web Chat](docs/screenshots/web-chat.png)

### Mobil Uygulama
![Mobile Login](docs/screenshots/mobile-login.png)
![Mobile Chat](docs/screenshots/mobile-chat.png)

## 🔧 Troubleshooting

### Backend Çalışmıyor
- .NET 8.0 SDK kurulu olduğundan emin olun
- `dotnet --version` ile kontrol edin
- Database migration'ları uygulandı mı: `dotnet ef database update`

### Frontend Bağlanamıyor
- Backend çalışıyor mu kontrol edin
- `.env` dosyasında API URL doğru mu
- CORS ayarları backend'de yapılandırıldı mı

### AI Service Yanıt Vermiyor
- Hugging Face Space uyuyor olabilir (ilk istek 30-60 saniye sürebilir)
- Model yükleme süresi uzun olabilir
- Backend timeout ayarlarını kontrol edin (5 saniye)

### Mobile APK Kurulmuyor
- "Bilinmeyen Kaynaklardan Yükleme" ayarı açık mı
- Android 5.0 veya üzeri gerekli
- Yeterli depolama alanı var mı

## 📚 Öğrenilen Konular

Bu proje ile şunları öğrendim:

1. **Full-Stack Geliştirme**: React → .NET → Python AI zinciri
2. **API Entegrasyonu**: RESTful API tasarımı ve kullanımı
3. **Database Yönetimi**: Entity Framework Core ile CRUD işlemleri
4. **AI/ML Entegrasyonu**: Hugging Face modellerinin kullanımı
5. **Deployment**: Ücretsiz platformlarda (Vercel, Render, HF Spaces) deployment
6. **Mobile Development**: React Native ile cross-platform uygulama
7. **Testing**: Unit test ve integration test yazımı
8. **Error Handling**: Graceful degradation ve timeout yönetimi

## 🤝 Katkıda Bulunma

Bu proje eğitim amaçlı geliştirilmiştir. Önerileriniz için issue açabilirsiniz.

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.

## 👤 Geliştirici

**Mete Karasubasi**
- GitHub: [@MeteKarasubasi](https://github.com/MeteKarasubasi)

---

**Not**: Bu proje, AI araçları ile kod üretme ve full-stack development yeteneklerini göstermek amacıyla geliştirilmiştir. Kritik kod bölümleri (DB sorguları, API çağrıları) elle yazılarak kod hakimiyeti kanıtlanmıştır.
