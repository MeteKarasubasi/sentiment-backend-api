# 🚀 Frontend Vercel Deployment Rehberi

## ✅ Hazırlık Durumu

Frontend Vercel'e deploy edilmeye hazır! Tüm konfigürasyonlar tamamlanmış durumda.

## 📋 Deployment Adımları

### Adım 1: Vercel Hesabı Oluştur

1. https://vercel.com adresine git
2. **"Sign Up"** butonuna tıkla
3. **GitHub ile giriş yap** (önerilen)
4. GitHub hesabınla bağlan

### Adım 2: Yeni Proje Oluştur

1. Vercel Dashboard'a git: https://vercel.com/dashboard
2. **"Add New..."** butonuna tıkla
3. **"Project"** seç
4. GitHub repository'ni seç: `sentiment-backend-api` (veya repo adın)

### Adım 3: Proje Ayarları

Vercel otomatik olarak ayarları algılayacak, ama kontrol et:

```
Framework Preset: Vite
Root Directory: frontend
Build Command: npm run build
Output Directory: dist
Install Command: npm install
Node.js Version: 18.x (veya 20.x)
```

### Adım 4: Environment Variables Ekle

**Environment Variables** bölümünde:

```
Name: VITE_API_URL
Value: https://sentiment-chat-backend.onrender.com
```

**ÖNEMLİ:** `VITE_` prefix'i olmalı!

### Adım 5: Deploy Et!

1. **"Deploy"** butonuna tıkla
2. Build işlemini izle (2-3 dakika)
3. Deploy tamamlandığında URL'i kaydet

### Adım 6: Test Et

Deploy tamamlandıktan sonra:

1. Vercel URL'ini aç (örn: `https://sentiment-chat-xyz.vercel.app`)
2. Rumuz gir ve kayıt ol
3. Oda oluştur veya katıl
4. Mesaj gönder
5. Duygu analizi badge'lerini kontrol et

## 🎯 Beklenen Sonuç

✅ Web uygulaması canlıda  
✅ Backend API'ye bağlanıyor  
✅ Mesajlaşma çalışıyor  
✅ Duygu analizi gösteriliyor  
✅ Oda sistemi aktif  

## 📝 Deployment Sonrası

### URL'i Kaydet

Vercel size bir URL verecek:
```
https://sentiment-chat-[random].vercel.app
```

Bu URL'i kaydet ve README.md'ye ekle!

### Custom Domain (Opsiyonel)

Kendi domain'in varsa:
1. Vercel Dashboard → Project Settings → Domains
2. Domain ekle
3. DNS ayarlarını yap

## 🔧 Troubleshooting

### Problem: Build Hatası

**Çözüm:**
- Logs'u kontrol et
- `npm run build` komutunu local'de test et
- Node.js versiyonunu kontrol et

### Problem: API Bağlanamıyor

**Çözüm:**
- Environment variable'ı kontrol et: `VITE_API_URL`
- Backend API'nin çalıştığını doğrula
- Browser console'da CORS hatası var mı kontrol et

### Problem: 404 Hatası

**Çözüm:**
- `vercel.json` dosyasının rewrites ayarını kontrol et
- SPA routing için gerekli

## ✅ Checklist

- [ ] Vercel hesabı oluşturuldu
- [ ] GitHub repository bağlandı
- [ ] Root directory `frontend` olarak ayarlandı
- [ ] Environment variable `VITE_API_URL` eklendi
- [ ] Deploy başarılı
- [ ] URL kaydedildi
- [ ] Test edildi ve çalışıyor

## 🎉 Tebrikler!

Frontend başarıyla deploy edildi! Şimdi sırada:
- Mobile APK'yı GitHub Release'e yükle
- README.md'yi güncelle
- Tüm deployment'ları test et

---

**Deployment URL'ini kaydetmeyi unutma!** 📝
