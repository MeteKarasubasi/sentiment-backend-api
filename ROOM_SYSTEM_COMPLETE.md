# Oda Sistemi Tamamlandı! 🎉

## Yapılan Değişiklikler

### Backend (C# / .NET)

1. **Yeni Model: Room.cs**
   - Oda adı, şifre hash'i, oluşturan kullanıcı
   - Her odanın kendi mesajları var

2. **Message Modeli Güncellendi**
   - `RoomId` eklendi
   - Her mesaj bir odaya ait

3. **Yeni Controller: RoomsController.cs**
   - `POST /api/rooms` - Yeni oda oluştur
   - `POST /api/rooms/join` - Odaya şifre ile katıl
   - `GET /api/rooms` - Tüm odaları listele
   - `GET /api/rooms/{id}` - Belirli bir odayı getir

4. **MessagesController Güncellendi**
   - Mesajlar artık `roomId` parametresi ile filtreleniyor
   - Sadece belirli bir odadaki mesajlar getiriliyor

5. **AppDbContext Güncellendi**
   - `Rooms` DbSet eklendi
   - İlişkiler yapılandırıldı

### Frontend (React)

1. **Yeni Bileşen: RoomSelection.jsx**
   - Oda listesi görüntüleme
   - Yeni oda oluşturma formu
   - Odaya katılma formu (şifre ile)
   - Responsive tasarım

2. **ChatWindow.jsx Güncellendi**
   - Artık `room` prop'u alıyor
   - Oda adı başlıkta gösteriliyor
   - "Odadan Ayrıl" butonu eklendi
   - Mesajlar sadece o odadan getiriliyor

3. **App.jsx Güncellendi**
   - 3 aşamalı akış:
     1. Login (Rumuz girişi)
     2. Room Selection (Oda seçimi/oluşturma)
     3. Chat (Sohbet)

4. **API Servisi Güncellendi**
   - `createRoom()` - Oda oluştur
   - `joinRoom()` - Odaya katıl
   - `getRooms()` - Odaları listele
   - `sendMessage()` ve `getMessages()` artık roomId kullanıyor

## Özellikler

### ✅ Oda Yönetimi
- Kullanıcılar yeni oda oluşturabilir
- Her oda şifre ile korunuyor
- Oda adları benzersiz olmalı (3-50 karakter)
- Şifreler SHA256 ile hash'leniyor

### ✅ Güvenlik
- Yanlış şifre ile odaya girilemez
- Her oda izole - sadece o odadaki mesajlar görünür
- Kullanıcı doğrulaması yapılıyor

### ✅ Kullanıcı Deneyimi
- Temiz ve modern arayüz
- Responsive tasarım
- Gerçek zamanlı mesajlaşma (3 saniyelik polling)
- Kendi mesajları sağda mor, diğerleri solda beyaz
- Animasyonlu geçişler

## Kullanım Senaryosu

### 1. Kullanıcı Girişi
```
1. Rumuz gir (örn: "Ahmet")
2. "Sohbete Başla" butonuna tıkla
```

### 2. Oda Oluşturma
```
1. "+ Yeni Oda Oluştur" butonuna tıkla
2. Oda adı gir (örn: "Arkadaşlar")
3. Şifre belirle (örn: "1234")
4. "Oluştur" butonuna tıkla
5. Otomatik olarak odaya gir
```

### 3. Odaya Katılma
```
1. Oda listesinden bir oda seç
2. "Katıl" butonuna tıkla
3. Şifreyi gir
4. "Katıl" butonuna tıkla
5. Doğru şifre ise odaya gir
```

### 4. Sohbet
```
1. Mesaj yaz ve gönder
2. Diğer kullanıcıların mesajlarını gör
3. Duygu analizi etiketlerini gör
4. "Odadan Ayrıl" ile oda seçimine dön
5. "Çıkış Yap" ile tamamen çık
```

## Test Senaryosu

### İki Kullanıcı ile Test

**Tarayıcı 1 (Chrome):**
1. http://localhost:5174 aç
2. Rumuz: "Ahmet"
3. "Yeni Oda Oluştur"
4. Oda adı: "Test Odası"
5. Şifre: "1234"
6. Mesaj gönder: "Merhaba!"

**Tarayıcı 2 (Chrome Incognito):**
1. http://localhost:5174 aç
2. Rumuz: "Ayşe"
3. "Test Odası" odasına katıl
4. Şifre: "1234"
5. Ahmet'in mesajını gör
6. Mesaj gönder: "Selam!"

**Tarayıcı 1'e dön:**
- 3 saniye içinde Ayşe'nin mesajını göreceksin

### Farklı Odalar Testi

**Tarayıcı 3 (Firefox):**
1. Rumuz: "Mehmet"
2. "Başka Oda" oluştur, şifre: "5678"
3. Mesaj gönder
4. Ahmet ve Ayşe'nin mesajlarını GÖRMEYECEK (farklı oda)

## Teknik Detaylar

### Veritabanı Yapısı
```
Users
- Id (PK)
- Rumuz (Unique)
- CreatedAt

Rooms
- Id (PK)
- Name (Unique)
- PasswordHash
- CreatedBy
- CreatedAt

Messages
- Id (PK)
- Rumuz (FK -> Users)
- RoomId (FK -> Rooms)
- Text
- SentimentLabel
- SentimentScore
- CreatedAt
```

### API Endpoints

**Rooms:**
- `POST /api/rooms` - Oda oluştur
- `POST /api/rooms/join` - Odaya katıl
- `GET /api/rooms` - Odaları listele
- `GET /api/rooms/{id}` - Oda detayı

**Messages:**
- `POST /api/messages` - Mesaj gönder (roomId gerekli)
- `GET /api/messages?roomId={id}` - Oda mesajlarını getir

**Users:**
- `POST /api/users` - Kullanıcı oluştur
- `GET /api/users` - Kullanıcıları listele

## Çalıştırma

### Backend
```bash
cd backend
dotnet run
```
Backend: http://localhost:5000

### Frontend
```bash
cd frontend
npm run dev
```
Frontend: http://localhost:5174

## Gelecek İyileştirmeler

- [ ] WebSocket/SignalR ile gerçek zamanlı mesajlaşma
- [ ] Oda üye listesi
- [ ] Kullanıcı "yazıyor..." göstergesi
- [ ] Mesaj düzenleme/silme
- [ ] Oda yöneticisi yetkileri
- [ ] Özel mesajlaşma
- [ ] Dosya paylaşımı
- [ ] Emoji picker
- [ ] Bildirimler
- [ ] Mesaj arama

## Notlar

- Eski test mesajları temizlendi (veritabanı yeniden oluşturuldu)
- Şifreler SHA256 ile hash'leniyor
- Her oda izole - mesajlar karışmıyor
- Polling 3 saniyede bir çalışıyor
- Responsive tasarım - mobil uyumlu
