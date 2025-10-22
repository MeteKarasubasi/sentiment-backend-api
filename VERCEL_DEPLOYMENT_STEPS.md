# Vercel Deployment - Adım Adım Rehber

## 🎯 Hedef
Frontend React uygulamasını Vercel'e deploy etmek.

## ✅ Ön Kontroller

### 1. GitHub Repository Durumu
- ✅ Repository: https://github.com/MeteKarasubasi/sentiment-backend-api
- ✅ Frontend kodu `frontend/` klasöründe
- ✅ Production environment variables hazır

### 2. Backend API Durumu
- ✅ Backend çalışıyor: https://sentiment-chat-backend.onrender.com
- ✅ Health check: https://sentiment-chat-backend.onrender.com/api/health

---

## 📝 Deployment Adımları

### Adım 1: Vercel'e Giriş Yap

1. Tarayıcıda https://vercel.com adresine git
2. Sağ üstteki **"Sign Up"** veya **"Login"** butonuna tıkla
3. **"Continue with GitHub"** seçeneğini seç
4. GitHub hesabınla giriş yap
5. Gerekli izinleri ver (repository erişimi)

### Adım 2: Yeni Proje Oluştur

1. Vercel Dashboard'da **"Add New..."** butonuna tıkla
2. Açılan menüden **"Project"** seç
3. **"Import Git Repository"** bölümünde GitHub'ı seç
4. Repository listesinde **"sentiment-backend-api"** repository'sini bul
5. Repository'nin yanındaki **"Import"** butonuna tıkla

> **Not**: Eğer repository listede görünmüyorsa:
> - "Adjust GitHub App Permissions" linkine tıkla
> - Repository erişim izinlerini güncelle
> - Sayfayı yenile

### Adım 3: Proje Ayarlarını Yapılandır

Aşağıdaki ayarları yapılandır:

#### Project Name
```
sentiment-chat-frontend
```
veya istediğin bir isim (URL'de kullanılacak)

#### Framework Preset
```
Vite
```
(Otomatik algılanmalı)

#### Root Directory
**ÖNEMLİ**: Root directory'yi değiştir!

1. **"Edit"** butonuna tıkla (Root Directory yanında)
2. Klasör listesinden **"frontend"** seç
3. **"Continue"** butonuna tıkla

#### Build and Output Settings

Aşağıdaki ayarlar otomatik doldurulmalı:

- **Build Command**: `npm run build`
- **Output Directory**: `dist`
- **Install Command**: `npm install`

> **Not**: Eğer otomatik dolmadıysa manuel gir.

### Adım 4: Environment Variables Ekle

1. **"Environment Variables"** bölümünü bul
2. Aşağıdaki değişkeni ekle:

| Name | Value |
|------|-------|
| `VITE_API_URL` | `https://sentiment-chat-backend.onrender.com` |

**Ekleme Adımları**:
- Name alanına: `VITE_API_URL`
- Value alanına: `https://sentiment-chat-backend.onrender.com`
- **"Add"** butonuna tıkla

### Adım 5: Deploy Et

1. Tüm ayarları kontrol et:
   - ✅ Root Directory: `frontend`
   - ✅ Framework: Vite
   - ✅ Environment Variable: `VITE_API_URL` eklendi

2. **"Deploy"** butonuna tıkla

3. Build işleminin başlamasını bekle

### Adım 6: Build İşlemini İzle

Build süreci 1-2 dakika sürecek. Ekranda şunları göreceksin:

```
Building...
├─ Installing dependencies
├─ Running build command
├─ Uploading build output
└─ Deployment ready
```

**Beklenen Çıktı**:
```
✓ Build completed successfully
✓ Deployment ready
```

### Adım 7: Deployment URL'ini Al

Build tamamlandığında:

1. **"Visit"** butonuna tıkla veya
2. Vercel tarafından verilen URL'i kopyala

URL formatı:
```
https://sentiment-chat-frontend.vercel.app
```
veya
```
https://sentiment-chat-frontend-[random].vercel.app
```

---

## 🧪 Test Adımları

Deployment tamamlandıktan sonra uygulamayı test et:

### 1. Uygulamayı Aç
Vercel URL'ini tarayıcıda aç.

### 2. Login Testi
1. Bir rumuz gir (örn: `testuser`)
2. **"Kayıt Ol"** butonuna tıkla
3. Chat ekranına yönlendirilmelisin

### 3. Mesaj Gönderme Testi

**Pozitif Mesaj**:
```
Bu harika! Çok mutluyum!
```
Beklenen: Yeşil "pozitif" badge

**Negatif Mesaj**:
```
Bu berbat. Hiç beğenmedim.
```
Beklenen: Kırmızı "negatif" badge

**Nötr Mesaj**:
```
Hava bugün bulutlu.
```
Beklenen: Gri "nötr" badge

### 4. Mesaj Listesi Testi
- Gönderilen mesajların listede göründüğünü kontrol et
- Duygu badge'lerinin doğru renkte olduğunu kontrol et
- Güven skorlarının göründüğünü kontrol et

---

## ❌ Olası Hatalar ve Çözümleri

### Hata 1: "Build Failed - Module not found"

**Çözüm**:
```bash
cd frontend
npm install
git add package-lock.json
git commit -m "Update dependencies"
git push origin main
```
Sonra Vercel'de "Redeploy" yap.

### Hata 2: "VITE_API_URL is not defined"

**Çözüm**:
1. Vercel Dashboard → Project → Settings
2. Environment Variables sekmesine git
3. `VITE_API_URL` değişkenini kontrol et
4. Yoksa ekle, yanlışsa düzelt
5. "Redeploy" yap

### Hata 3: "Failed to fetch" / CORS Error

**Çözüm**:
Backend'de CORS ayarlarını kontrol et. Backend zaten yapılandırılmış olmalı.

Test için:
```bash
curl https://sentiment-chat-backend.onrender.com/api/health
```

### Hata 4: "404 Not Found" on page refresh

**Çözüm**:
`vercel.json` dosyası zaten mevcut ve yapılandırılmış:
```json
{
  "rewrites": [{ "source": "/(.*)", "destination": "/" }]
}
```

Eğer hala sorun varsa:
1. Vercel Dashboard → Project → Settings
2. "Rewrites" bölümünü kontrol et

---

## 🔄 Yeniden Deploy (Redeploy)

Kod değişikliği yaptıktan sonra:

### Otomatik Deploy
```bash
git add .
git commit -m "Update frontend"
git push origin main
```
Vercel otomatik olarak yeni deploy başlatır.

### Manuel Deploy
1. Vercel Dashboard → Project
2. "Deployments" sekmesi
3. En son deployment'ın yanındaki "..." menüsü
4. "Redeploy" seç

---

## 📊 Deployment Sonrası

### 1. URL'i Kaydet
Deployment URL'ini not al:
```
https://[your-app].vercel.app
```

### 2. README.md'yi Güncelle
```markdown
## 🌐 Demo Linkleri

- **Web Uygulaması**: https://[your-app].vercel.app
- **Backend API**: https://sentiment-chat-backend.onrender.com
- **API Docs**: https://sentiment-chat-backend.onrender.com/swagger
```

### 3. Custom Domain (Opsiyonel)
Kendi domain'ini bağlamak için:
1. Vercel Dashboard → Project → Settings
2. "Domains" sekmesi
3. Domain ekle ve DNS ayarlarını yap

---

## ✅ Başarı Kriterleri

Deployment başarılı sayılır eğer:

- ✅ Build hatasız tamamlandı
- ✅ Uygulama URL'de açılıyor
- ✅ Login ekranı görünüyor
- ✅ Kullanıcı kaydı çalışıyor
- ✅ Mesaj gönderme çalışıyor
- ✅ Duygu analizi sonuçları görünüyor
- ✅ Backend API'ye bağlanıyor

---

## 📝 Deployment Bilgileri

### Vercel Free Tier Limitleri
- ✅ Unlimited deployments
- ✅ 100 GB bandwidth/month
- ✅ Automatic HTTPS
- ✅ Custom domains
- ✅ Instant rollbacks

### Build Süreleri
- İlk build: 1-2 dakika
- Sonraki build'ler: 30-60 saniye

### Deployment URL Yapısı
- Production: `https://[project-name].vercel.app`
- Preview: `https://[project-name]-[hash].vercel.app`

---

## 🎉 Tamamlandı!

Deployment başarılı olduktan sonra:

1. ✅ URL'i test et
2. ✅ Tüm özellikleri kontrol et
3. ✅ README.md'yi güncelle
4. ✅ Projeyi paylaş!

---

**Hazırlayan**: Kiro AI Assistant  
**Tarih**: 22 Ekim 2025  
**Proje**: Sentiment Chat
