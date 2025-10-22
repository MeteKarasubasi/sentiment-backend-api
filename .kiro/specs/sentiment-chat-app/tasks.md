# Implementation Plan

- [x] 1. AI Service kurulumu ve deployment





  - Hugging Face Spaces'te yeni bir Space oluştur (Python/Gradio)
  - `ai-service/app.py` dosyasını oluştur ve cardiffnlp/twitter-roberta-base-sentiment modelini yükle
  - Gradio interface ile POST endpoint oluştur (metin girişi alıp sentiment JSON döndürecek)
  - İngilizce label'ları Türkçe'ye map et (POSITIVE→pozitif, NEGATIVE→negatif, NEUTRAL→nötr)
  - `ai-service/requirements.txt` dosyasını oluştur (gradio, transformers, torch)
  - Hugging Face Spaces'e deploy et ve endpoint URL'ini test et
  - _Requirements: 1.1, 1.2, 1.3, 1.4_

- [x] 2. Backend API proje yapısı ve veritabanı kurulumu






  - `backend/` klasöründe .NET Core 8.0 Web API projesi oluştur
  - Entity Framework Core ve SQLite NuGet paketlerini ekle
  - `Models/User.cs` ve `Models/Message.cs` entity modellerini oluştur
  - `Data/AppDbContext.cs` ile DbContext yapılandırması yap
  - SQLite migration oluştur ve veritabanı şemasını oluştur (Users ve Messages tabloları)
  - CORS yapılandırması ekle (localhost ve Vercel domain için)
  - _Requirements: 2.1, 2.2, 3.3, 7.2, 7.3_

- [x] 3. Backend API - Kullanıcı yönetimi endpoint'leri






  - `Controllers/UsersController.cs` oluştur
  - POST /api/users endpoint'i implement et (rumuz validation: 3-20 karakter, unique kontrolü)
  - GET /api/users endpoint'i implement et
  - GET /api/users/{id} endpoint'i implement et
  - Duplicate rumuz için 409 Conflict response döndür
  - _Requirements: 2.1, 2.2, 2.3, 2.4, 2.5_

- [x] 4. Backend API - AI Service entegrasyonu





  - `Services/ISentimentService.cs` interface oluştur
  - `Services/SentimentService.cs` implement et
  - HttpClient ile Hugging Face AI Service'e POST request at
  - AI Service response'unu parse et ve SentimentResult modeline map et
  - Timeout ve error handling ekle (5 saniye timeout)
  - AI Service URL'ini appsettings.json'dan oku
  - Program.cs'te HttpClient ve SentimentService'i dependency injection ile kaydet
  - _Requirements: 1.1, 1.2, 1.5, 3.2_

- [x] 5. Backend API - Mesaj yönetimi endpoint'leri





  - `Controllers/MessagesController.cs` oluştur
  - POST /api/messages endpoint'i implement et (rumuz ve text alacak)
  - Mesaj kaydederken SentimentService'i çağır ve sentiment bilgisini al
  - AI Service unavailable ise mesajı null sentiment ile kaydet ve 207 Multi-Status döndür
  - Message'ı veritabanına kaydet (text, rumuz, sentimentLabel, sentimentScore, timestamp)
  - GET /api/messages endpoint'i implement et (pagination desteği ile)
  - Mesajları CreatedAt'e göre sırala (oldest to newest)
  - GET /api/health endpoint'i ekle
  - _Requirements: 3.1, 3.2, 3.3, 3.4, 3.5, 4.1, 4.2, 4.3, 4.4, 4.5, 7.4_

- [x] 5.1 Backend API unit testleri






  - xUnit test projesi oluştur
  - UsersController testleri yaz (validation, duplicate rumuz)
  - MessagesController testleri yaz (AI integration mock ile)
  - SentimentService testleri yaz (HTTP client mock ile)
  - _Requirements: 2.1-2.5, 3.1-3.5_

- [x] 6. Backend API Render deployment





  - GitHub repository oluştur ve backend kodunu push et
  - Render'da yeni Web Service oluştur ve GitHub repo'yu bağla
  - Build command ayarla: `dotnet publish -c Release -o out`
  - Start command ayarla: `dotnet out/backend.dll`
  - Environment variable ekle: AI_SERVICE_URL (Hugging Face endpoint)
  - Deploy et ve API endpoint'lerini test et (Postman veya curl ile)
  - _Requirements: 7.1, 7.2, 7.3, 7.4, 7.5_

- [x] 7. React Web Frontend proje kurulumu






  - `frontend/` klasöründe React projesi oluştur (Vite veya CRA)
  - Axios paketini ekle
  - `src/services/api.js` oluştur ve API base URL yapılandır
  - API service fonksiyonları yaz (registerUser, sendMessage, getMessages)
  - Environment variable için .env dosyası oluştur (REACT_APP_API_URL)
  - _Requirements: 5.1, 5.3_

- [x] 8. React Web Frontend - Kullanıcı login ekranı





  - `src/components/UserLogin.jsx` component'i oluştur
  - Rumuz input field ve kayıt butonu ekle
  - POST /api/users çağrısı yap
  - Başarılı kayıt sonrası rumuz'u localStorage'a kaydet
  - Chat ekranına yönlendir
  - _Requirements: 5.2_
-

- [x] 9. React Web Frontend - Chat arayüzü




  - `src/components/ChatWindow.jsx` container component'i oluştur
  - `src/components/MessageList.jsx` oluştur (mesaj listesi gösterimi)
  - `src/components/MessageItem.jsx` oluştur (tek mesaj, sentiment badge ile)
  - `src/components/MessageInput.jsx` oluştur (text input ve gönder butonu)
  - Sentiment'e göre renk kodlaması ekle (pozitif: yeşil, nötr: gri, negatif: kırmızı)
  - Component mount olduğunda GET /api/messages çağır
  - Mesaj gönderildiğinde POST /api/messages çağır ve listeyi güncelle
  - Auto-scroll to bottom özelliği ekle
  - _Requirements: 5.1, 5.3, 5.4_

- [x] 9.1 React Web Frontend testleri






  - Jest ve React Testing Library kur
  - MessageItem component testi yaz
  - MessageInput component testi yaz (send button click)
  - API service mock testleri yaz
  - _Requirements: 5.1-5.4_

- [x] 10. React Web Frontend Vercel deployment





  - Vercel hesabı oluştur ve GitHub repo'yu bağla
  - Build settings yapılandır (build command: `npm run build`)
  - Environment variable ekle: REACT_APP_API_URL (Render API URL)
  - Deploy et ve web uygulamasını test et
  - End-to-end flow test et (kullanıcı kaydı, mesaj gönderme, sentiment görüntüleme)
  - _Requirements: 5.5_

- [x] 11. React Native mobil uygulama kurulumu





  - React Native CLI ile yeni proje oluştur
  - Axios ve gerekli UI kütüphanelerini ekle (React Native Paper veya Native Base)
  - `src/services/api.js` oluştur (web ile aynı API fonksiyonları)
  - API base URL'i config dosyasında tanımla
  - _Requirements: 6.1, 6.2_

- [x] 12. React Native - Login ve Chat ekranları














  - `src/screens/LoginScreen.js` oluştur (rumuz input ve kayıt)
  - `src/screens/ChatScreen.js` oluştur
  - `src/components/MessageList.js` oluştur (FlatList ile)
  - `src/components/MessageItem.js` oluştur (sentiment badge ile)
  - `src/components/MessageInput.js` oluştur
  - Navigation yapılandır (Login → Chat)
  - Sentiment renk kodlaması ekle
  - Pull-to-refresh özelliği ekle
  - _Requirements: 6.1, 6.2, 6.3_

- [x] 13. React Native Android build ve test





  - Android emulator'da uygulamayı çalıştır ve test et
  - API entegrasyonunu test et (Render backend'e bağlantı)
  - Release APK build et: `cd android && ./gradlew assembleRelease`
  - APK'yı fiziksel cihazda test et
  - _Requirements: 6.4, 6.5_

- [ ] 14. Proje dokümantasyonu
  - Ana dizinde `README.md` oluştur
  - Proje genel açıklaması yaz
  - Kurulum adımları ekle (AI service, backend, frontend, mobile için ayrı ayrı)
  - Kullanılan teknolojiler ve AI araçları listele (Hugging Face model, ChatGPT, Copilot vb.)
  - Demo linkleri ekle (Vercel, Render, Hugging Face Spaces)
  - Kod hakimiyeti bölümü ekle:
    - Her klasördeki ana dosyaların (Program.cs, App.js, app.py) açıklaması
    - AI ile yazılan vs elle yazılan kod bölümlerini belirt
    - Kritik kod bloklarını (örn: HttpClient ile AI Service çağrısı) vurgula ve açıkla
  - Ekran görüntüleri ekle (web ve mobil)
  - _Requirements: 8.1, 8.2, 8.3, 8.4, 8.5_

- [ ]* 14.1 Ek dokümantasyon
  - API endpoint'leri için Swagger/OpenAPI dokümantasyonu ekle
  - Architecture diagram'ı README'ye ekle
  - Troubleshooting bölümü ekle
  - _Requirements: 8.1_
