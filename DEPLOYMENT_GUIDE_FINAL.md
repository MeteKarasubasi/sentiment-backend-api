# 🚀 Deployment Rehberi - Sentiment Chat

Bu dokümanda projenin tüm bileşenlerinin nasıl deploy edileceği adım adım anlatılmaktadır.

## 📋 Deployment Checklist

- [ ] AI Service (Hugging Face Spaces)
- [ ] Backend API (Render)
- [ ] Frontend Web (Vercel)
- [ ] Mobile APK Build (Android)
- [ ] Environment Variables Ayarları
- [ ] Test ve Doğrulama

## 1️⃣ AI Service Deployment (Hugging Face Spaces)

### Adım 1: Hugging Face Hesabı
1. https://huggingface.co adresine gidin
2. Hesap oluşturun veya giriş yapın

### Adım 2: Yeni Space Oluşturma
1. "New Space" butonuna tıklayın
2. Space adı: `sentiment-chat-ai`
3. SDK: **Gradio** seçin
4. Visibility: Public veya Private

### Adım 3: Dosyaları Upload Etme
```bash
cd ai-service
```

Aşağıdaki dosyaları Space'e upload edin:
- `app.py`
- `requirements.txt`
- `README.md`

### Adım 4: Otomatik Build
- Hugging Face otomatik olarak build edecek
- 2-3 dakika içinde hazır olacak
- URL: `https://huggingface.co/spaces/[username]/sentiment-chat-ai`

### Adım 5: Test
```bash
curl -X POST "https://[username]-sentiment-chat-ai.hf.space/api/predict" \
  -H "Content-Type: application/json" \
  -d '{"data": ["Bu harika bir gün!"]}'
```

**✅ AI Service URL'i kaydedin:** `https://[username]-sentiment-chat-ai.hf.space`

---

## 2️⃣ Backend API Deployment (Render)

### Adım 1: Render Hesabı
1. https://render.com adresine gidin
2. GitHub ile giriş yapın

### Adım 2: Yeni Web Service
1. "New +" → "Web Service"
2. GitHub repository'nizi bağlayın
3. Service adı: `sentiment-chat-api`

### Adım 3: Ayarlar
```
Name: sentiment-chat-api
Region: Frankfurt (EU Central)
Branch: main
Root Directory: backend
Runtime: .NET
Build Command: dotnet publish -c Release -o out
Start Command: dotnet out/backend.dll
Instance Type: Free
```

### Adım 4: Environment Variables
```
PORT=5000
ASPNETCORE_ENVIRONMENT=Production
AI_SERVICE_URL=https://[username]-sentiment-chat-ai.hf.space
```

### Adım 5: Deploy
- "Create Web Service" butonuna tıklayın
- İlk deploy 5-10 dakika sürer
- URL: `https://sentiment-chat-api.onrender.com`

### Adım 6: Test
```bash
curl https://sentiment-chat-api.onrender.com/api/health
```

**✅ Backend API URL'i kaydedin:** `https://sentiment-chat-api.onrender.com`

---

## 3️⃣ Frontend Deployment (Vercel)

### Adım 1: Vercel Hesabı
1. https://vercel.com adresine gidin
2. GitHub ile giriş yapın

### Adım 2: Yeni Proje
1. "Add New..." → "Project"
2. GitHub repository'nizi import edin
3. Proje adı: `sentiment-chat`

### Adım 3: Ayarlar
```
Framework Preset: Vite
Root Directory: frontend
Build Command: npm run build
Output Directory: dist
Install Command: npm install
```

### Adım 4: Environment Variables
```
VITE_API_URL=https://sentiment-chat-api.onrender.com
```

### Adım 5: Deploy
- "Deploy" butonuna tıklayın
- 2-3 dakika içinde hazır
- URL: `https://sentiment-chat.vercel.app`

### Adım 6: Custom Domain (Opsiyonel)
1. Settings → Domains
2. Kendi domain'inizi ekleyin

**✅ Frontend URL'i kaydedin:** `https://sentiment-chat.vercel.app`

---

## 4️⃣ Mobile APK Build (Android)

### Adım 1: Gereksinimler
```bash
# Android Studio ve SDK kurulu olmalı
# Java JDK 11+ kurulu olmalı
```

### Adım 2: Environment Variables
`mobile/.env` dosyası oluşturun:
```
API_URL=https://sentiment-chat-api.onrender.com
```

### Adım 3: API URL Güncelleme
`mobile/src/services/api.js` dosyasını güncelleyin:
```javascript
const API_BASE_URL = 'https://sentiment-chat-api.onrender.com';
```

### Adım 4: Build
```bash
cd mobile/android
./gradlew assembleRelease
```

### Adım 5: APK Konumu
```
mobile/android/app/build/outputs/apk/release/app-release.apk
```

### Adım 6: APK İmzalama (Production için)
```bash
keytool -genkey -v -keystore my-release-key.keystore \
  -alias my-key-alias -keyalg RSA -keysize 2048 -validity 10000

# android/gradle.properties dosyasına ekleyin:
MYAPP_RELEASE_STORE_FILE=my-release-key.keystore
MYAPP_RELEASE_KEY_ALIAS=my-key-alias
MYAPP_RELEASE_STORE_PASSWORD=****
MYAPP_RELEASE_KEY_PASSWORD=****
```

**✅ APK dosyasını test edin**

---

## 5️⃣ Tüm Sistem Test

### Test 1: AI Service
```bash
curl -X POST "https://[username]-sentiment-chat-ai.hf.space/api/predict" \
  -H "Content-Type: application/json" \
  -d '{"data": ["Merhaba dünya"]}'
```

Beklenen: `{"data": ["pozitif", 0.85]}`

### Test 2: Backend Health
```bash
curl https://sentiment-chat-api.onrender.com/api/health
```

Beklenen: `{"status":"healthy"}`

### Test 3: Backend User Creation
```bash
curl -X POST "https://sentiment-chat-api.onrender.com/api/users" \
  -H "Content-Type: application/json" \
  -d '{"rumuz": "TestUser"}'
```

### Test 4: Frontend
1. https://sentiment-chat.vercel.app adresini açın
2. Rumuz girin
3. Oda oluşturun
4. Mesaj gönderin
5. Duygu analizini kontrol edin

### Test 5: Mobile APK
1. APK'yı Android cihaza yükleyin
2. Uygulamayı açın
3. Rumuz girin
4. Mesaj gönderin

---

## 6️⃣ Production Ayarları

### Backend (Render)
```bash
# PostgreSQL Database ekleyin (Free tier)
# Environment Variables:
DATABASE_URL=postgresql://...
ASPNETCORE_ENVIRONMENT=Production
```

### Frontend (Vercel)
```bash
# Environment Variables:
VITE_API_URL=https://sentiment-chat-api.onrender.com
NODE_ENV=production
```

### CORS Ayarları
Backend `Program.cs`:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("Production", policy =>
    {
        policy.WithOrigins("https://sentiment-chat.vercel.app")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

---

## 7️⃣ Monitoring ve Logs

### Render Logs
```
Dashboard → sentiment-chat-api → Logs
```

### Vercel Logs
```
Dashboard → sentiment-chat → Deployments → View Function Logs
```

### Hugging Face Logs
```
Space → App → Logs
```

---

## 8️⃣ Troubleshooting

### Problem: Backend 500 Error
**Çözüm:** Render logs kontrol edin, database bağlantısını kontrol edin

### Problem: Frontend API bağlanamıyor
**Çözüm:** CORS ayarlarını kontrol edin, API URL'i doğru mu?

### Problem: AI Service yavaş
**Çözüm:** Hugging Face Spaces free tier yavaş olabilir, paid plan düşünün

### Problem: Mobile APK çalışmıyor
**Çözüm:** API URL'i production URL'e güncelleyin

---

## 9️⃣ Demo Linkleri (Örnek)

Deployment sonrası bu linkleri güncelleyin:

```markdown
## 🌐 Live Demo

- **Web App:** https://sentiment-chat.vercel.app
- **Backend API:** https://sentiment-chat-api.onrender.com
- **API Docs:** https://sentiment-chat-api.onrender.com/swagger
- **AI Service:** https://huggingface.co/spaces/[username]/sentiment-chat-ai
- **Mobile APK:** [Google Drive Link]
```

---

## 🔟 Güvenlik Kontrol Listesi

- [ ] API keys environment variables'da
- [ ] CORS sadece production domain'e izin veriyor
- [ ] Şifreler hash'leniyor
- [ ] SQL injection koruması aktif
- [ ] Rate limiting eklenmiş (opsiyonel)
- [ ] HTTPS kullanılıyor
- [ ] Sensitive data loglanmıyor

---

## 📊 Maliyet Tahmini

### Free Tier (Başlangıç)
- Hugging Face Spaces: **Ücretsiz**
- Render: **Ücretsiz** (750 saat/ay)
- Vercel: **Ücretsiz** (100GB bandwidth)
- **Toplam: $0/ay**

### Paid Tier (Production)
- Hugging Face Spaces Pro: **$9/ay**
- Render Starter: **$7/ay**
- Vercel Pro: **$20/ay**
- **Toplam: ~$36/ay**

---

## ✅ Deployment Tamamlandı!

Tüm adımları tamamladıysanız, sisteminiz artık production'da çalışıyor!

### Son Kontroller
1. ✅ AI Service çalışıyor
2. ✅ Backend API çalışıyor
3. ✅ Frontend çalışıyor
4. ✅ Mobile APK build edildi
5. ✅ Tüm linkler çalışıyor
6. ✅ End-to-end test başarılı

### README'yi Güncelleyin
`README.md` dosyasındaki demo linklerini güncelleyin:
```markdown
## 🚀 Demo Linkleri

- **Web Chat:** https://sentiment-chat.vercel.app
- **Backend API:** https://sentiment-chat-api.onrender.com
- **AI Service:** https://huggingface.co/spaces/[username]/sentiment-chat-ai
- **Mobile APK:** [Link]
```

---

**Tebrikler! Projeniz başarıyla deploy edildi! 🎉**
