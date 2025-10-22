# 🚀 Deployment Adımları - Sırayla Takip Edin

Bu dokümanda deployment işlemlerini adım adım yapacağız.

## ✅ Ön Hazırlık (Şimdi Yapın)

### 1. GitHub'a Push

Terminal'de şu komutları çalıştırın:

```bash
# Git repository'yi başlat (eğer başlatmadıysanız)
git init

# Tüm dosyaları ekle
git add .

# Commit yap
git commit -m "Initial commit: Sentiment Chat App"

# GitHub'da yeni repository oluşturun (tarayıcıda):
# https://github.com/new
# Repository adı: sentiment-chat-app
# Public seçin
# README eklemeyin (zaten var)

# Remote ekle (kendi username'inizi yazın)
git remote add origin https://github.com/[USERNAME]/sentiment-chat-app.git

# Push yap
git branch -M main
git push -u origin main
```

**✅ Tamamlandı mı?** GitHub'da repository'nizi kontrol edin.

---

## 🎯 ADIM 1: AI Service (Hugging Face Spaces)

### Neden İlk?
Backend bu servise bağlı, önce AI service hazır olmalı.

### Yapılacaklar:

1. **Hugging Face'e gidin:** https://huggingface.co
2. **Giriş yapın** (GitHub ile giriş yapabilirsiniz)
3. **New Space oluşturun:**
   - Spaces → "Create new Space"
   - Space name: `sentiment-chat-ai`
   - License: MIT
   - SDK: **Gradio**
   - Hardware: CPU basic (free)
   - Visibility: Public

4. **Dosyaları Upload edin:**
   
   Space'e şu dosyaları yükleyin:
   - `ai-service/app.py`
   - `ai-service/requirements.txt`
   - `ai-service/README.md`

   **Nasıl Upload Edilir:**
   - Space sayfasında "Files" sekmesi
   - "Add file" → "Upload files"
   - Dosyaları sürükle-bırak
   - "Commit changes to main" butonuna tıkla

5. **Build'i bekleyin:**
   - 2-3 dakika sürer
   - "Building" → "Running" olacak
   - Gradio interface açılacak

6. **Test edin:**
   - Interface'de "Bu harika bir gün!" yazın
   - "Submit" butonuna tıklayın
   - Sonuç: "pozitif" ve bir skor görmeli

7. **URL'i kaydedin:**
   ```
   https://huggingface.co/spaces/[USERNAME]/sentiment-chat-ai
   ```

**✅ AI Service Hazır!**

---

## 🎯 ADIM 2: Backend API (Render)

### Yapılacaklar:

1. **Render'a gidin:** https://render.com
2. **GitHub ile giriş yapın**
3. **New Web Service oluşturun:**
   - Dashboard → "New +" → "Web Service"
   - "Build and deploy from a Git repository"
   - GitHub repository'nizi seçin: `sentiment-chat-app`

4. **Ayarları yapın:**
   ```
   Name: sentiment-chat-api
   Region: Frankfurt (EU Central) veya Oregon (US West)
   Branch: main
   Root Directory: backend
   Runtime: .NET
   Build Command: dotnet publish -c Release -o out
   Start Command: dotnet out/backend.dll
   Instance Type: Free
   ```

5. **Environment Variables ekleyin:**
   
   "Advanced" → "Add Environment Variable":
   ```
   PORT=5000
   ASPNETCORE_ENVIRONMENT=Production
   AI_SERVICE_URL=https://[USERNAME]-sentiment-chat-ai.hf.space
   ```
   
   **Önemli:** AI_SERVICE_URL'i kendi Hugging Face Space URL'iniz ile değiştirin!

6. **Create Web Service butonuna tıklayın**

7. **Deploy'u bekleyin:**
   - İlk deploy 5-10 dakika sürer
   - Logs'u izleyin
   - "Live" yazısını görünce hazır

8. **URL'i kaydedin:**
   ```
   https://sentiment-chat-api.onrender.com
   ```

9. **Test edin:**
   
   Tarayıcıda açın:
   ```
   https://sentiment-chat-api.onrender.com/api/health
   ```
   
   Sonuç: `{"status":"healthy",...}` görmeli

**✅ Backend API Hazır!**

---

## 🎯 ADIM 3: Frontend (Vercel)

### Yapılacaklar:

1. **Vercel'e gidin:** https://vercel.com
2. **GitHub ile giriş yapın**
3. **New Project oluşturun:**
   - "Add New..." → "Project"
   - GitHub repository'nizi import edin: `sentiment-chat-app`

4. **Ayarları yapın:**
   ```
   Project Name: sentiment-chat
   Framework Preset: Vite
   Root Directory: frontend
   Build Command: npm run build
   Output Directory: dist
   Install Command: npm install
   ```

5. **Environment Variables ekleyin:**
   
   "Environment Variables" bölümünde:
   ```
   Name: VITE_API_URL
   Value: https://sentiment-chat-api.onrender.com
   ```
   
   **Önemli:** Kendi Render backend URL'inizi yazın!

6. **Deploy butonuna tıklayın**

7. **Deploy'u bekleyin:**
   - 2-3 dakika sürer
   - "Building" → "Ready" olacak

8. **URL'i kaydedin:**
   ```
   https://sentiment-chat.vercel.app
   ```

9. **Test edin:**
   
   Tarayıcıda açın ve:
   - Rumuz girin
   - Oda oluşturun
   - Mesaj gönderin
   - Duygu analizini görün

**✅ Frontend Hazır!**

---

## 🎯 ADIM 4: Mobile APK Build

### Yapılacaklar:

1. **API URL'i güncelleyin:**
   
   `mobile/src/services/api.js` dosyasını açın:
   ```javascript
   const API_BASE_URL = 'https://sentiment-chat-api.onrender.com';
   ```

2. **Build yapın:**
   ```bash
   cd mobile/android
   ./gradlew assembleRelease
   ```
   
   Windows'ta:
   ```bash
   cd mobile\android
   gradlew.bat assembleRelease
   ```

3. **APK konumu:**
   ```
   mobile/android/app/build/outputs/apk/release/app-release.apk
   ```

4. **APK'yı test edin:**
   - APK'yı Android cihaza yükleyin
   - Uygulamayı açın
   - Rumuz girin
   - Mesaj gönderin

5. **GitHub Release oluşturun:**
   
   GitHub'da:
   - Releases → "Create a new release"
   - Tag: `v1.0.0`
   - Title: `Sentiment Chat v1.0.0`
   - Description: "Initial release"
   - APK dosyasını upload edin
   - "Publish release"

**✅ Mobile APK Hazır!**

---

## 📝 ADIM 5: README'yi Güncelle

`README.md` dosyasındaki demo linklerini güncelleyin:

```markdown
## 🌐 Live Demo

- **Web Application:** https://sentiment-chat.vercel.app
- **Backend API:** https://sentiment-chat-api.onrender.com
- **API Documentation:** https://sentiment-chat-api.onrender.com/swagger
- **AI Service:** https://huggingface.co/spaces/[USERNAME]/sentiment-chat-ai
- **Mobile APK:** https://github.com/[USERNAME]/sentiment-chat-app/releases/tag/v1.0.0
```

Commit ve push yapın:
```bash
git add README.md
git commit -m "docs: Update demo links"
git push
```

---

## ✅ Deployment Tamamlandı!

### Final Checklist:

- [ ] AI Service çalışıyor (Hugging Face)
- [ ] Backend API çalışıyor (Render)
- [ ] Frontend çalışıyor (Vercel)
- [ ] Mobile APK build edildi
- [ ] GitHub Release oluşturuldu
- [ ] README güncellenmiş
- [ ] Tüm linkler test edilmiş

### Test Senaryosu:

1. **Frontend'i aç:** https://sentiment-chat.vercel.app
2. **Rumuz gir:** "TestUser"
3. **Oda oluştur:** "Test Odası", şifre: "1234"
4. **Mesaj gönder:** "Bu harika bir gün!"
5. **Duygu analizini gör:** "pozitif" etiketi görünmeli

### Sorun Giderme:

**Problem:** Backend 500 hatası
- Render logs'u kontrol edin
- Environment variables doğru mu?
- AI_SERVICE_URL doğru mu?

**Problem:** Frontend API'ye bağlanamıyor
- VITE_API_URL doğru mu?
- Backend çalışıyor mu?
- CORS ayarları doğru mu?

**Problem:** AI Service yavaş
- Hugging Face Spaces free tier yavaş olabilir
- İlk istek 10-20 saniye sürebilir (cold start)

---

## 🎉 Tebrikler!

Tüm servisler başarıyla deploy edildi!

### Teslim İçin Hazır:

```
GitHub Repository: https://github.com/[USERNAME]/sentiment-chat-app
Web App: https://sentiment-chat.vercel.app
Backend API: https://sentiment-chat-api.onrender.com
AI Service: https://huggingface.co/spaces/[USERNAME]/sentiment-chat-ai
Mobile APK: https://github.com/[USERNAME]/sentiment-chat-app/releases
```

**Projeniz teslime hazır! 🚀**
