# ✅ Frontend Vercel Deployment Checklist

## 📋 Ön Hazırlık

- [x] Frontend build başarılı
- [x] `vercel.json` dosyası hazır
- [x] `.env.production` dosyası hazır
- [x] Backend API çalışıyor
- [x] AI Service çalışıyor

## 🚀 Deployment Adımları

### 1. Vercel Hesabı

- [ ] https://vercel.com adresine git
- [ ] "Sign Up" veya "Log In" tıkla
- [ ] GitHub ile giriş yap
- [ ] GitHub authorization'ı onayla

### 2. Proje Oluştur

- [ ] Dashboard'da "Add New..." tıkla
- [ ] "Project" seç
- [ ] GitHub repository'ni bul ve seç
- [ ] Repository: `sentiment-backend-api` (veya senin repo adın)

### 3. Konfigürasyon

- [ ] **Framework Preset:** Vite (otomatik algılanmalı)
- [ ] **Root Directory:** `frontend` olarak ayarla ⚠️ ÖNEMLİ!
- [ ] **Build Command:** `npm run build` (varsayılan)
- [ ] **Output Directory:** `dist` (varsayılan)
- [ ] **Install Command:** `npm install` (varsayılan)

### 4. Environment Variables

- [ ] "Environment Variables" bölümünü bul
- [ ] "Add" butonuna tıkla
- [ ] **Name:** `VITE_API_URL`
- [ ] **Value:** `https://sentiment-chat-backend.onrender.com`
- [ ] "Add" ile kaydet

### 5. Deploy

- [ ] Tüm ayarları kontrol et
- [ ] "Deploy" butonuna tıkla
- [ ] Build loglarını izle (2-3 dakika)
- [ ] "Congratulations!" mesajını bekle
- [ ] Deployment URL'ini kopyala

### 6. Test

- [ ] Vercel URL'ini tarayıcıda aç
- [ ] Login ekranı görünüyor mu?
- [ ] Rumuz gir ve "Kayıt Ol" tıkla
- [ ] Oda seçim ekranı açıldı mı?
- [ ] Yeni oda oluştur
- [ ] Mesaj gönder
- [ ] Duygu badge'i görünüyor mu?
- [ ] Mesajlar yükleniyor mu?
- [ ] Console'da hata var mı? (F12)

## 📝 Deployment Bilgileri

### Vercel URL'i Kaydet

```
Deployment URL: https://sentiment-chat-____________.vercel.app
```

**Bu URL'i kaydet! README'ye ekleyeceğiz.**

### Deployment Detayları

```
Project Name: ___________________
Deployment Date: ___________________
Build Time: ___________________
Status: ___________________
```

## ✅ Başarı Kriterleri

Deployment başarılı sayılır eğer:

- [x] Build hatasız tamamlandı
- [ ] URL açılıyor
- [ ] Login çalışıyor
- [ ] Oda sistemi çalışıyor
- [ ] Mesajlaşma çalışıyor
- [ ] Duygu analizi gösteriliyor
- [ ] Backend'e bağlanıyor
- [ ] Console'da kritik hata yok

## 🔧 Sorun Giderme

### Build Hatası

**Semptom:** Build failed  
**Çözüm:**
1. Vercel logs'u oku
2. Local'de `npm run build` test et
3. Node.js versiyonunu kontrol et

### API Bağlanamıyor

**Semptom:** Network error, CORS error  
**Çözüm:**
1. Environment variable'ı kontrol et
2. Backend API'nin çalıştığını doğrula
3. Browser console'u kontrol et

### 404 Hatası

**Semptom:** Sayfa yenilediğinde 404  
**Çözüm:**
1. `vercel.json` dosyasını kontrol et
2. Rewrites ayarı olmalı

### Root Directory Hatası

**Semptom:** Build files not found  
**Çözüm:**
1. Root Directory'yi `frontend` olarak ayarla
2. Redeploy et

## 📊 Deployment Sonrası

### Yapılacaklar

- [ ] URL'i test et
- [ ] Tüm özellikleri kontrol et
- [ ] URL'i kaydet
- [ ] Screenshot al (opsiyonel)
- [ ] README'ye URL ekle (sonra)
- [ ] Sonraki adıma geç (Mobile APK)

### Vercel Dashboard

Deployment sonrası Vercel Dashboard'da:

- **Deployments:** Tüm deployment'ları görebilirsin
- **Analytics:** Ziyaretçi istatistikleri
- **Logs:** Runtime logs
- **Settings:** Domain, environment variables

## 🎉 Tebrikler!

Frontend başarıyla deploy edildi! 

### Sonraki Adım

**Mobile APK GitHub Release:**
- APK'yı GitHub'a yükle
- Release oluştur
- Download linki al

---

**Deployment URL'ini kaydetmeyi unutma!** 📝

```
Frontend URL: _________________________________
```
