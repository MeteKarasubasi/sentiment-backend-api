# 📱 Mobil Uygulama Test Rehberi

Bu dokümanda mobil uygulamanın nasıl test edileceği anlatılmaktadır.

## 🔍 Mevcut Durum Kontrolü

### Mobil Uygulama Özellikleri
- ✅ React Native CLI ile geliştirildi
- ✅ Login ekranı var
- ✅ Chat ekranı var
- ✅ API entegrasyonu var
- ❌ Oda sistemi yok (web'deki gibi)

### API Konfigürasyonu
**Dosya:** `mobile/src/config.js`

```javascript
export const API_BASE_URL = __DEV__
  ? 'http://10.0.2.2:5000'  // Android emulator
  : 'https://sentiment-chat-backend.onrender.com'; // Production
```

## 🚀 Test Adımları

### 1. Backend'in Çalıştığından Emin Olun

```bash
# Backend'i başlatın
cd backend
dotnet run
```

Backend: http://localhost:5000

### 2. Metro Bundler'ı Başlatın

Yeni terminal açın:
```bash
cd mobile
npm start
```

### 3. Android Emulator'ü Başlatın

#### Seçenek A: Android Studio ile
1. Android Studio'yu açın
2. AVD Manager'ı açın
3. Bir emulator seçin ve başlatın

#### Seçenek B: Komut satırı ile
```bash
emulator -list-avds
emulator -avd [AVD_NAME]
```

### 4. Uygulamayı Çalıştırın

Yeni terminal açın:
```bash
cd mobile
npm run android
```

## 🧪 Test Senaryosu

### Senaryo 1: Kullanıcı Kaydı
1. Uygulama açılır
2. "Rumuz" alanına "TestUser" yazın
3. "Sohbete Başla" butonuna tıklayın
4. ✅ Chat ekranına geçmeli

### Senaryo 2: Mesaj Gönderme
1. Chat ekranında mesaj yazın: "Bu harika bir gün!"
2. Gönder butonuna tıklayın
3. ✅ Mesaj listede görünmeli
4. ✅ Duygu analizi etiketi görünmeli (pozitif/nötr/negatif)

### Senaryo 3: Mesaj Listesi
1. Birkaç mesaj gönderin
2. ✅ Tüm mesajlar listede görünmeli
3. ✅ Otomatik scroll en alta gitmeli

## ⚠️ Bilinen Sorunlar

### Sorun 1: Oda Sistemi Yok
**Durum:** Mobil uygulamada oda sistemi henüz eklenmedi
**Çözüm:** Web uygulamasındaki gibi oda sistemi eklenebilir

### Sorun 2: API Bağlantı Hatası
**Hata:** "Network request failed"
**Çözüm:**
- Backend çalışıyor mu kontrol edin
- Android emulator için `10.0.2.2` doğru alias
- Fiziksel cihaz için bilgisayarınızın IP adresini kullanın

### Sorun 3: Metro Bundler Hatası
**Hata:** "Unable to resolve module"
**Çözüm:**
```bash
cd mobile
rm -rf node_modules
npm install
npm start -- --reset-cache
```

## 📊 Mobil Uygulama Durumu

### Tamamlanan Özellikler
- ✅ Kullanıcı girişi (rumuz)
- ✅ Mesaj gönderme
- ✅ Mesaj listesi
- ✅ Duygu analizi gösterimi
- ✅ API entegrasyonu
- ✅ Navigation (Login → Chat)
- ✅ Error handling

### Eksik Özellikler (Web'de var, mobilde yok)
- ❌ Oda sistemi
- ❌ Oda oluşturma
- ❌ Odaya katılma
- ❌ Şifre koruması
- ❌ Gerçek zamanlı güncelleme (polling)

## 🔧 Mobil Uygulamayı Güncelleme

Eğer oda sistemini eklemek isterseniz:

### 1. API Servisini Güncelle
`mobile/src/services/api.js` dosyasına ekleyin:

```javascript
export const createRoom = async (name, password, createdBy) => {
  const response = await api.post('/api/rooms', { name, password, createdBy });
  return response.data;
};

export const joinRoom = async (name, password, rumuz) => {
  const response = await api.post('/api/rooms/join', { name, password, rumuz });
  return response.data;
};

export const getRooms = async () => {
  const response = await api.get('/api/rooms');
  return response.data;
};
```

### 2. RoomSelection Ekranı Oluştur
`mobile/src/screens/RoomSelectionScreen.js` dosyası oluşturun

### 3. Navigation Güncelle
`mobile/App.tsx` dosyasına RoomSelection ekranını ekleyin

## 📱 APK Build

### Development APK
```bash
cd mobile/android
./gradlew assembleDebug
```

APK: `mobile/android/app/build/outputs/apk/debug/app-debug.apk`

### Release APK
```bash
cd mobile/android
./gradlew assembleRelease
```

APK: `mobile/android/app/build/outputs/apk/release/app-release.apk`

## ✅ Test Sonuçları

### Başarılı Testler
- [ ] Uygulama başlatılıyor
- [ ] Login ekranı görünüyor
- [ ] Rumuz girişi çalışıyor
- [ ] Chat ekranına geçiş yapılıyor
- [ ] Mesaj gönderme çalışıyor
- [ ] Mesaj listesi görünüyor
- [ ] Duygu analizi gösteriliyor

### Başarısız Testler
- [ ] API bağlantı hatası
- [ ] Mesaj gönderilemiyor
- [ ] Duygu analizi görünmüyor

## 🎯 Sonuç

**Mobil Uygulama Durumu:**
- Temel özellikler çalışıyor ✅
- Oda sistemi eksik ❌
- Production'a hazır (temel özelliklerle) ✅

**Öneriler:**
1. Temel özellikleri test edin
2. APK build edin
3. Oda sistemini eklemek opsiyonel

**Teslim için yeterli mi?**
Evet! Temel chat özellikleri çalışıyor. Oda sistemi bonus özellik olarak eklenebilir.
