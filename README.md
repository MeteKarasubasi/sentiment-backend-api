# Sentiment Chat - Duygu Analizi Destekli Sohbet Uygulaması

Gerçek zamanlı duygu analizi ile çalışan, oda tabanlı sohbet uygulaması. React Web, React Native Mobile, .NET Core API ve Python AI servisi ile geliştirilmiştir.

## 🎯 Proje Özeti

Bu proje, kullanıcıların şifre korumalı odalarda sohbet edebileceği ve her mesajın otomatik olarak duygu analizi yapılarak pozitif/nötr/negatif olarak etiketlendiği bir chat uygulamasıdır.

## 🏗️ Mimari

```
┌─────────────────────┐
│   React Web App     │ ←→ Polling (3s)
│   (Vercel)          │
└──────────┬──────────┘
           │
           ↓ REST API
┌─────────────────────┐
│   .NET Core API     │ ←→ SQLite Database
│   (Render)          │
└──────────┬──────────┘
           │
           ↓ HTTP Request
┌─────────────────────┐
│   Python AI Service │
│   (HF Spaces)       │ → BERT Sentiment Analysis
└─────────────────────┘
```

## 📁 Klasör Yapısı

```
sentiment-chat/
├── frontend/          # React Web Uygulaması (Vite)
├── mobile/           # React Native CLI Uygulaması
├── backend/          # .NET Core 8 Web API
├── ai-service/       # Python Gradio AI Servisi
└── README.md         # Bu dosya
```

## 🚀 Demo Linkleri

### Çalışır Uygulamalar
- **Web Chat (Frontend):** https://sentiment-chat-frontend.vercel.app (Vercel)
- **Backend API:** https://sentiment-backend-api.onrender.com (Render)
- **AI Service:** https://huggingface.co/spaces/Mete1923/sentiment-api-wrapper (Hugging Face Spaces)
- **Mobile APK:** Build edilebilir - `mobile/BUILD_APK.md` dosyasına bakın

### API Endpoints
- **Health Check:** https://sentiment-backend-api.onrender.com/health
- **Swagger Docs:** https://sentiment-backend-api.onrender.com/swagger
- **AI Service Test:** https://huggingface.co/spaces/Mete1923/sentiment-api-wrapper

## 🛠️ Kullanılan Teknolojiler ve AI Araçları

### Frontend (React Web)
- **Framework:** React 19 + Vite
- **HTTP Client:** Axios
- **Styling:** Vanilla CSS
- **AI Araçları:** 
  - Kiro AI - Bileşen yapısı ve state yönetimi
  - GitHub Copilot - CSS styling ve animasyonlar
- **Elle Yazılan Kod:** API servis fonksiyonları (`frontend/src/services/api.js`)

### Mobile (React Native)
- **Framework:** React Native CLI
- **Navigation:** React Navigation
- **HTTP Client:** Axios
- **AI Araçları:**
  - Kiro AI - Ekran bileşenleri ve navigation yapısı
- **Elle Yazılan Kod:** API entegrasyonu (`mobile/src/services/api.js`)

### Backend (.NET Core)
- **Framework:** .NET Core 8 Web API
- **Database:** SQLite + Entity Framework Core
- **Authentication:** SHA256 Password Hashing
- **AI Araçları:**
  - Kiro AI - Controller yapısı ve routing
  - GitHub Copilot - LINQ sorguları
- **Elle Yazılan Kod:** 
  - Database context ve model ilişkileri (`backend/Data/AppDbContext.cs`)
  - Sentiment service entegrasyonu (`backend/Services/GradioSentimentService.cs`)

### AI Service (Python)
- **Framework:** Gradio
- **ML Model:** savasy/bert-base-turkish-sentiment-cased (Hugging Face)
- **Deployment:** Hugging Face Spaces
- **AI Araçları:**
  - Kiro AI - Gradio interface yapısı
- **Elle Yazılan Kod:** Model yükleme ve inference logic (`ai-service/app.py`)

## 📦 Kurulum

### Gereksinimler
- Node.js 18+
- .NET Core 8 SDK
- Python 3.9+
- Git

### 1. Repository'yi Klonlayın
```bash
git clone <repository-url>
cd sentiment-chat
```

### 2. Frontend Kurulumu
```bash
cd frontend
npm install
cp .env.example .env
# .env dosyasında VITE_API_URL'i düzenleyin
npm run dev
```
Frontend: http://localhost:5174

### 3. Backend Kurulumu
```bash
cd backend
dotnet restore
dotnet run
```
Backend: http://localhost:5000
Swagger: http://localhost:5000/swagger

### 4. AI Service Kurulumu
```bash
cd ai-service
pip install -r requirements.txt
python app.py
```
AI Service: http://localhost:7860

### 5. Mobile Kurulumu
```bash
cd mobile
npm install

# Android
npm run android

# iOS (Mac gerekli)
npm run ios
```

## 🎮 Kullanım

### Web Uygulaması
1. Tarayıcıda http://localhost:5174 adresini açın
2. Rumuz (kullanıcı adı) girin
3. Yeni oda oluşturun veya mevcut odaya katılın
4. Mesaj gönderin ve duygu analizini görün

### Mobil Uygulama
1. Uygulamayı başlatın
2. Rumuz girin
3. Oda seçin
4. Sohbet edin

## 📚 Dosya Yapısı ve İşlevleri

### Frontend (`frontend/`)

#### Temel Dosyalar
- **`src/App.jsx`** - Ana uygulama bileşeni, routing ve state yönetimi
  - *AI ile yazıldı:* Temel yapı ve state management
  - *Elle yazıldı:* localStorage entegrasyonu
  
- **`src/services/api.js`** - Backend API çağrıları
  - *Tamamen elle yazıldı* - HTTP istekleri ve error handling

#### Bileşenler (`src/components/`)
- **`UserLogin.jsx`** - Kullanıcı giriş ekranı
  - *AI ile yazıldı:* Form yapısı ve validasyon
  
- **`RoomSelection.jsx`** - Oda seçimi ve oluşturma
  - *AI ile yazıldı:* UI bileşenleri
  - *Elle yazıldı:* Oda katılma logic
  
- **`ChatWindow.jsx`** - Ana sohbet ekranı
  - *AI ile yazıldı:* Temel yapı
  - *Elle yazıldı:* Polling mekanizması ve mesaj gönderme
  
- **`MessageList.jsx`** - Mesaj listesi
  - *AI ile yazıldı:* Render logic
  
- **`MessageItem.jsx`** - Tekil mesaj bileşeni
  - *AI ile yazıldı:* UI
  - *Elle yazıldı:* Sentiment badge logic
  
- **`MessageInput.jsx`** - Mesaj giriş alanı
  - *AI ile yazıldı:* Form handling

#### Stiller
- **`*.css`** - Tüm stil dosyaları
  - *AI ile yazıldı:* Temel stiller ve layout
  - *Elle yazıldı:* Animasyonlar ve responsive tasarım

### Backend (`backend/`)

#### Controllers (`Controllers/`)
- **`UsersController.cs`** - Kullanıcı yönetimi
  - *AI ile yazıldı:* CRUD operasyonları
  - *Elle yazıldı:* Validasyon logic
  
- **`RoomsController.cs`** - Oda yönetimi
  - *AI ile yazıldı:* Temel CRUD
  - *Elle yazıldı:* Şifre hashing ve oda kapatma logic
  
- **`MessagesController.cs`** - Mesaj yönetimi
  - *AI ile yazıldı:* Temel yapı
  - *Elle yazıldı:* Sentiment service entegrasyonu ve error handling

#### Models (`Models/`)
- **`User.cs`** - Kullanıcı modeli
- **`Room.cs`** - Oda modeli
- **`RoomMember.cs`** - Oda üyelik modeli
- **`Message.cs`** - Mesaj modeli
  - *Tümü AI ile yazıldı*

#### Data (`Data/`)
- **`AppDbContext.cs`** - Entity Framework context
  - *AI ile yazıldı:* Temel yapı
  - *Elle yazıldı:* İlişkiler ve index'ler

#### Services (`Services/`)
- **`ISentimentService.cs`** - Sentiment service interface
- **`GradioSentimentService.cs`** - Gradio AI service entegrasyonu
  - *Tamamen elle yazıldı* - HTTP client ve error handling
  
- **`HuggingFaceSentimentService.cs`** - Alternatif HF implementation
  - *Elle yazıldı*

#### Diğer
- **`Program.cs`** - Uygulama başlangıç noktası
  - *AI ile yazıldı:* Temel yapı
  - *Elle yazıldı:* CORS ve database configuration

### AI Service (`ai-service/`)

- **`app.py`** - Gradio uygulaması
  - *AI ile yazıldı:* Gradio interface
  - *Elle yazıldı:* Model yükleme ve inference logic
  
- **`requirements.txt`** - Python bağımlılıkları
  - *Elle yazıldı*
  
- **`README.md`** - AI servis dokümantasyonu
  - *Elle yazıldı*

### Mobile (`mobile/`)

#### Screens (`src/screens/`)
- **`LoginScreen.js`** - Giriş ekranı
  - *AI ile yazıldı:* UI bileşenleri
  
- **`ChatScreen.js`** - Sohbet ekranı
  - *AI ile yazıldı:* Temel yapı
  - *Elle yazıldı:* API entegrasyonu

#### Components (`src/components/`)
- **`MessageList.js`** - Mesaj listesi
- **`MessageItem.js`** - Mesaj öğesi
- **`MessageInput.js`** - Mesaj girişi
  - *AI ile yazıldı*

#### Services (`src/services/`)
- **`api.js`** - API servisi
  - *Tamamen elle yazıldı*

## 🔑 Özellikler

### Temel Özellikler
- ✅ Kullanıcı kaydı (sadece rumuz)
- ✅ Şifre korumalı oda sistemi
- ✅ Gerçek zamanlı mesajlaşma (polling)
- ✅ Otomatik duygu analizi
- ✅ Mesaj geçmişi
- ✅ Responsive tasarım

### Gelişmiş Özellikler
- ✅ Otomatik oda kapatma (son üye ayrılınca)
- ✅ Görsel mesaj ayrımı (kendi/diğerleri)
- ✅ Hata yönetimi (AI servisi çalışmazsa mesaj yine kaydedilir)
- ✅ SHA256 şifre hashleme
- ✅ Otomatik kullanıcı oluşturma
- ✅ Animasyonlu geçişler

## 🗄️ Veritabanı Şeması

### Users
- `Id` (PK)
- `Rumuz` (Unique)
- `CreatedAt`

### Rooms
- `Id` (PK)
- `Name` (Unique)
- `PasswordHash`
- `CreatedBy`
- `CreatedAt`

### RoomMembers
- `Id` (PK)
- `RoomId` (FK)
- `Rumuz` (FK)
- `JoinedAt`

### Messages
- `Id` (PK)
- `Rumuz` (FK)
- `RoomId` (FK)
- `Text`
- `SentimentLabel` (pozitif/nötr/negatif)
- `SentimentScore` (0-1)
- `CreatedAt`

## 🔐 Güvenlik

- Oda şifreleri SHA256 ile hash'lenir
- CORS politikası yapılandırılmış
- SQL injection koruması (EF Core parametreli sorgular)
- Input validasyonu tüm endpoint'lerde

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

## 📱 APK Build (Android)

```bash
cd mobile/android
./gradlew assembleRelease
# APK: android/app/build/outputs/apk/release/app-release.apk
```

## 🚀 Deployment

### Frontend (Vercel)
1. Vercel hesabı oluşturun
2. GitHub repository'yi bağlayın
3. Root directory: `frontend`
4. Build command: `npm run build`
5. Output directory: `dist`
6. Environment variables: `VITE_API_URL`

### Backend (Render)
1. Render hesabı oluşturun
2. New Web Service oluşturun
3. GitHub repository'yi bağlayın
4. Root directory: `backend`
5. Build command: `dotnet publish -c Release -o out`
6. Start command: `dotnet out/backend.dll`

### AI Service (Hugging Face Spaces)
1. Hugging Face hesabı oluşturun
2. New Space oluşturun (Gradio)
3. `ai-service/` klasörünü upload edin
4. Otomatik deploy olur

## 🤖 AI Araçları Kullanımı

### Kiro AI
- Proje yapısı oluşturma
- Bileşen iskeletleri
- Controller ve model yapıları
- Routing ve navigation

### GitHub Copilot
- CSS styling
- LINQ sorguları
- Boilerplate kod
- Dokümantasyon

### Elle Yazılan Kritik Kod
1. **API Service Layer** - Tüm HTTP istekleri
2. **Database Context** - İlişkiler ve konfigürasyon
3. **Sentiment Service** - AI entegrasyonu
4. **Polling Mechanism** - Gerçek zamanlı güncelleme
5. **Password Hashing** - Güvenlik
6. **Error Handling** - Hata yönetimi

## 📊 Performans

- Polling interval: 3 saniye
- API response time: ~100-200ms
- AI inference time: ~1-2 saniye
- Database: SQLite (development), PostgreSQL önerilir (production)

## 🐛 Bilinen Sorunlar

- Polling yerine WebSocket kullanılabilir (daha verimli)
- AI servisi bazen yavaş olabilir (Hugging Face Spaces free tier)
- Mobil uygulama background'da polling durur

## 🔮 Gelecek Geliştirmeler

- [ ] WebSocket/SignalR entegrasyonu
- [ ] Kullanıcı "yazıyor..." göstergesi
- [ ] Mesaj düzenleme/silme
- [ ] Dosya paylaşımı
- [ ] Emoji picker
- [ ] Push notifications
- [ ] Mesaj arama
- [ ] Oda yöneticisi yetkileri

## 👥 Katkıda Bulunanlar

Bu proje, AI destekli geliştirme araçları (Kiro AI, GitHub Copilot) kullanılarak geliştirilmiştir. Kritik iş mantığı ve güvenlik katmanları elle yazılmıştır.

## 📄 Lisans

MIT License

## 📞 İletişim

Sorularınız için issue açabilirsiniz.

---

**Not:** Bu proje eğitim amaçlıdır. Production kullanımı için ek güvenlik önlemleri alınmalıdır.
