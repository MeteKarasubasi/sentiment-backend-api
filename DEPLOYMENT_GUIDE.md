# Deployment Rehberi - Sentiment Chat

Bu rehber, projenin tüm bileşenlerini ücretsiz platformlara deploy etme adımlarını içerir.

## 📋 Deployment Durumu

### ✅ Tamamlanan Deployment'lar

1. **Backend API** - Render
   - URL: https://sentiment-chat-backend.onrender.com
   - Status: ✅ Çalışıyor
   - Health Check: https://sentiment-chat-backend.onrender.com/api/health

2. **AI Service** - Hugging Face Spaces
   - Status: ✅ Deploy edildi
   - Model: cardiffnlp/twitter-roberta-base-sentiment

### ⏳ Bekleyen Deployment'lar

1. **Frontend (Web)** - Vercel
2. **Mobile APK** - GitHub Releases

---

## 🌐 Frontend Vercel Deployment

### Ön Hazırlık

Frontend kodu zaten hazır ve GitHub'da:
- Repository: https://github.com/MeteKarasubasi/sentiment-backend-api
- Klasör: `frontend/`

### Adım 1: Vercel Hesabı Oluştur

1. https://vercel.com adresine git
2. "Sign Up" butonuna tıkla
3. GitHub hesabınla giriş yap
4. Gerekli izinleri ver

### Adım 2: Yeni Proje Oluştur

1. Vercel Dashboard'da "Add New..." → "Project" seç
2. GitHub repository'ni seç: `sentiment-backend-api`
3. "Import" butonuna tıkla

### Adım 3: Proje Ayarları

**Framework Preset**: Vite

**Root Directory**: `frontend`

**Build Command**: 
```bash
npm run build
```

**Output Directory**: 
```bash
dist
```

**Install Command**: 
```bash
npm install
```

### Adım 4: Environment Variables

Aşağıdaki environment variable'ı ekle:

| Name | Value |
|------|-------|
| `VITE_API_URL` | `https://sentiment-chat-backend.onrender.com` |

### Adım 5: Deploy

1. "Deploy" butonuna tıkla
2. Build işleminin tamamlanmasını bekle (2-3 dakika)
3. Deploy tamamlandığında URL'i not al

### Adım 6: Test

Deploy edilen URL'i aç ve test et:
1. Rumuz ile kayıt ol
2. Mesaj gönder
3. Duygu analizinin çalıştığını kontrol et

---

## 🔧 Backend Render Deployment (Zaten Tamamlandı)

Backend zaten deploy edilmiş durumda:
- URL: https://sentiment-chat-backend.onrender.com
- Swagger: https://sentiment-chat-backend.onrender.com/swagger

### Güncelleme Yapmak İçin

Eğer backend kodunda değişiklik yaptıysan:

```bash
cd backend
git add .
git commit -m "Backend güncellemesi"
git push origin main
```

Render otomatik olarak yeni kodu deploy edecektir.

---

## 🤖 AI Service Hugging Face Deployment (Zaten Tamamlandı)

AI servisi zaten Hugging Face Spaces'te deploy edilmiş.

### Space URL'ini Bulmak İçin

1. https://huggingface.co adresine git
2. Profil → Spaces
3. Sentiment analysis space'ini bul
4. URL'i kopyala (örn: `https://huggingface.co/spaces/username/space-name`)

---

## 📱 Mobile APK GitHub Release

Android APK'yı GitHub Releases'e yüklemek için:

### Adım 1: APK'yı Hazırla

APK zaten build edilmiş:
```
mobile/android/app/build/outputs/apk/release/app-release.apk
```

### Adım 2: GitHub Release Oluştur

1. GitHub repository'ye git: https://github.com/MeteKarasubasi/sentiment-backend-api
2. "Releases" → "Create a new release"
3. Tag: `v1.0.0`
4. Title: `Sentiment Chat Mobile v1.0.0`
5. Description:
```markdown
# Sentiment Chat Mobile - Android APK

İlk stabil sürüm.

## Özellikler
- Kullanıcı kaydı (rumuz ile)
- Mesajlaşma
- AI duygu analizi (pozitif/negatif/nötr)
- Renkli duygu gösterimi

## Kurulum
1. APK'yı indirin
2. "Bilinmeyen Kaynaklardan Yükleme" ayarını açın
3. APK'yı açıp yükleyin

## Gereksinimler
- Android 5.0 veya üzeri
- 100 MB boş alan
- İnternet bağlantısı
```

6. APK dosyasını sürükle-bırak ile yükle
7. "Publish release" butonuna tıkla

---

## ✅ Deployment Checklist

### Backend
- [x] Render'a deploy edildi
- [x] Health endpoint çalışıyor
- [x] Database (SQLite) yapılandırıldı
- [x] AI Service entegrasyonu çalışıyor
- [x] CORS yapılandırıldı

### Frontend
- [ ] Vercel'e deploy edildi
- [ ] Environment variables ayarlandı
- [ ] Backend API'ye bağlanıyor
- [ ] Kullanıcı kaydı çalışıyor
- [ ] Mesajlaşma çalışıyor
- [ ] Duygu analizi gösteriliyor

### AI Service
- [x] Hugging Face Spaces'te deploy edildi
- [x] Model yükleniyor
- [x] API endpoint çalışıyor
- [x] Türkçe label mapping çalışıyor

### Mobile
- [x] APK build edildi
- [ ] GitHub Release'e yüklendi
- [ ] Kurulum talimatları eklendi

---

## 🔗 Tüm URL'ler (Deployment Sonrası)

Deployment'lar tamamlandıktan sonra README.md dosyasını güncelle:

```markdown
## 🌐 Demo Linkleri

- **Web Uygulaması**: https://[your-app].vercel.app
- **Backend API**: https://sentiment-chat-backend.onrender.com
- **API Docs**: https://sentiment-chat-backend.onrender.com/swagger
- **AI Service**: https://huggingface.co/spaces/[username]/[space-name]
- **Mobile APK**: https://github.com/MeteKarasubasi/sentiment-backend-api/releases/tag/v1.0.0
```

---

## 🐛 Troubleshooting

### Vercel Build Hatası

**Hata**: `Module not found`
**Çözüm**: 
```bash
cd frontend
npm install
git add package-lock.json
git commit -m "Update dependencies"
git push
```

### Vercel Environment Variable Hatası

**Hata**: API'ye bağlanamıyor
**Çözüm**: 
1. Vercel Dashboard → Project → Settings → Environment Variables
2. `VITE_API_URL` değişkenini kontrol et
3. Redeploy yap

### Render Backend Uyuyor

**Hata**: İlk istek çok uzun sürüyor
**Çözüm**: 
- Bu normal (free tier 15 dakika sonra uyur)
- İlk istek 30-60 saniye sürebilir
- Sonraki istekler hızlı olacak

### CORS Hatası

**Hata**: `Access-Control-Allow-Origin` hatası
**Çözüm**: 
Backend'de CORS ayarlarını kontrol et (`Program.cs`):
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
```

---

## 📊 Deployment Metrikleri

### Backend (Render Free Tier)
- **Build Time**: 2-5 dakika
- **Cold Start**: 30-60 saniye
- **Response Time**: 100-500ms (warm)
- **Uptime**: 750 saat/ay (yeterli)

### Frontend (Vercel Free Tier)
- **Build Time**: 1-2 dakika
- **Deploy Time**: 30 saniye
- **Response Time**: 50-200ms
- **Bandwidth**: 100 GB/ay

### AI Service (HF Spaces Free Tier)
- **Cold Start**: 30-60 saniye
- **Inference Time**: 1-3 saniye
- **Uptime**: 24/7 (uyuma yok)

---

## 🎯 Sonraki Adımlar

1. ✅ Backend deploy edildi
2. ⏳ Frontend'i Vercel'e deploy et
3. ⏳ APK'yı GitHub Release'e yükle
4. ⏳ README.md'deki URL'leri güncelle
5. ⏳ Ekran görüntüleri ekle
6. ✅ Projeyi test et

---

**Son Güncelleme**: 22 Ekim 2025
