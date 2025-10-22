# 📋 Deployment Adım Adım Rehber

Bu dokümanda projenin tüm bileşenlerini sırayla deploy edeceğiz.

## 🎯 Deployment Sırası

1. ✅ **AI Service** - Hugging Face Spaces (TAMAMLANDI)
2. ✅ **Backend API** - Render (TAMAMLANDI)
3. ⏳ **Frontend Web** - Vercel (ŞİMDİ)
4. ⏳ **Mobile APK** - GitHub Release (SONRA)
5. ⏳ **README Güncelleme** - Demo URL'leri (EN SON)

---

## 1️⃣ AI Service ✅ TAMAMLANDI

**URL:** https://huggingface.co/spaces/Mete1923/emotion  
**Wrapper API:** https://sentiment-api-wrapper.onrender.com  
**Status:** Çalışıyor ✅

---

## 2️⃣ Backend API ✅ TAMAMLANDI

**URL:** https://sentiment-chat-backend.onrender.com  
**Swagger:** https://sentiment-chat-backend.onrender.com/swagger  
**Health:** https://sentiment-chat-backend.onrender.com/api/health  
**Status:** Çalışıyor ✅

---

## 3️⃣ Frontend Web ⏳ ŞİMDİ DEPLOY EDİLECEK

### Hazırlık Durumu ✅

- ✅ Build başarılı (237 KB)
- ✅ `vercel.json` hazır
- ✅ `.env.production` hazır
- ✅ Backend URL konfigüre edilmiş

### Deployment Adımları

#### A. Vercel'e Git

1. Tarayıcıda aç: https://vercel.com
2. **"Sign Up"** veya **"Log In"** tıkla
3. **"Continue with GitHub"** seç
4. GitHub hesabınla giriş yap

#### B. Yeni Proje Oluştur

1. Dashboard'da **"Add New..."** → **"Project"**
2. GitHub repository'ni seç
3. Repository adı: `sentiment-backend-api` (veya senin repo adın)

#### C. Konfigürasyon

**Import Git Repository** ekranında:

```
Project Name: sentiment-chat (veya istediğin isim)
Framework Preset: Vite (otomatik algılanır)
Root Directory: frontend (ÖNEMLİ!)
Build Command: npm run build
Output Directory: dist
Install Command: npm install
```

**Root Directory'yi mutlaka `frontend` olarak ayarla!**

#### D. Environment Variables

**Environment Variables** bölümünde **"Add"** tıkla:

```
Name: VITE_API_URL
Value: https://sentiment-chat-backend.onrender.com
```

**Dikkat:** `VITE_` prefix'i şart!

#### E. Deploy

1. **"Deploy"** butonuna tıkla
2. Build loglarını izle (2-3 dakika)
3. "Congratulations!" mesajını bekle
4. **URL'i kopyala ve kaydet!**

#### F. Test Et

1. Vercel URL'ini aç
2. Rumuz gir: `TestUser`
3. Oda oluştur: `TestRoom`
4. Mesaj gönder: `Bu harika bir test!`
5. Duygu badge'ini kontrol et (yeşil "pozitif" olmalı)

### Beklenen URL Formatı

```
https://sentiment-chat-[random-id].vercel.app
```

veya custom domain ayarladıysan:

```
https://yourdomain.com
```

### ✅ Başarı Kriterleri

- [ ] Deployment başarılı
- [ ] URL açılıyor
- [ ] Login ekranı görünüyor
- [ ] Kullanıcı kaydı çalışıyor
- [ ] Oda sistemi çalışıyor
- [ ] Mesaj gönderme çalışıyor
- [ ] Duygu analizi gösteriliyor
- [ ] Console'da hata yok

---

## 4️⃣ Mobile APK - GitHub Release ⏳ SONRA

### Hazırlık Durumu ✅

- ✅ APK build edildi
- ✅ Konum: `mobile/android/app/build/outputs/apk/debug/app-debug.apk`
- ✅ Boyut: ~117 MB
- ✅ Test edildi ve çalışıyor

### Deployment Adımları (Sonra Yapılacak)

1. GitHub repository'ye git
2. **"Releases"** → **"Create a new release"**
3. Tag: `v1.0.0`
4. Title: `Sentiment Chat v1.0.0 - Android APK`
5. APK'yı upload et
6. Release notes yaz
7. **"Publish release"**

---

## 5️⃣ README Güncelleme ⏳ EN SON

Tüm deployment'lar tamamlandıktan sonra `README.md`'yi güncelleyeceğiz:

### Eklenecek Bölümler

```markdown
## 🌐 Live Demo

- **Web App:** https://sentiment-chat-[id].vercel.app
- **Backend API:** https://sentiment-chat-backend.onrender.com
- **API Docs:** https://sentiment-chat-backend.onrender.com/swagger
- **AI Service:** https://huggingface.co/spaces/Mete1923/emotion
- **Mobile APK:** https://github.com/[username]/[repo]/releases/tag/v1.0.0
```

---

## 📊 Deployment Durumu

| Bileşen | Platform | Status | URL |
|---------|----------|--------|-----|
| AI Service | Hugging Face | ✅ Çalışıyor | https://huggingface.co/spaces/Mete1923/emotion |
| AI Wrapper | Render | ✅ Çalışıyor | https://sentiment-api-wrapper.onrender.com |
| Backend API | Render | ✅ Çalışıyor | https://sentiment-chat-backend.onrender.com |
| Frontend Web | Vercel | ⏳ Deploy edilecek | - |
| Mobile APK | GitHub | ⏳ Upload edilecek | - |

---

## 🎯 Şu Anda Yapılacak

**Frontend Vercel Deployment:**

1. https://vercel.com adresine git
2. GitHub ile giriş yap
3. Yeni proje oluştur
4. Repository'yi seç
5. Root directory: `frontend`
6. Environment variable ekle: `VITE_API_URL`
7. Deploy et
8. URL'i kaydet

**Tahmini Süre:** 10-15 dakika

---

## 💡 Önemli Notlar

### Vercel Free Tier

- ✅ Unlimited deployments
- ✅ 100 GB bandwidth/ay
- ✅ Automatic HTTPS
- ✅ Global CDN
- ✅ Automatic git deployments

### Deployment Sonrası

1. URL'i test et
2. Tüm özellikleri kontrol et
3. URL'i kaydet
4. README'ye ekle
5. Sonraki adıma geç (Mobile APK)

---

## 🆘 Yardım

Sorun yaşarsan:

1. Vercel build logs'u kontrol et
2. Environment variable'ları doğrula
3. Backend API'nin çalıştığını test et
4. Browser console'u kontrol et

---

**Şimdi Frontend'i Vercel'e deploy et! 🚀**

Deployment tamamlandığında URL'i bana söyle, sonraki adıma geçelim.
