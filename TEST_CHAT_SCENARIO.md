# Chat Uygulaması Test Senaryosu

## Yapılan Değişiklikler

### 1. Gerçek Zamanlı Mesajlaşma (Polling)
- Her 3 saniyede bir otomatik olarak yeni mesajlar çekiliyor
- Kullanıcılar birbirlerinin mesajlarını görebiliyor
- Mesaj gönderildikten sonra 500ms içinde tüm mesajlar yenileniyor

### 2. Görsel Ayrım
- **Kendi mesajlarınız**: Sağda, mor gradient arka plan
- **Diğer kullanıcıların mesajları**: Solda, beyaz arka plan
- Her mesaj maksimum %70 genişlikte
- Animasyonlu giriş efekti

### 3. Otomatik Scroll
- Yeni mesaj geldiğinde otomatik olarak en alta kayıyor

## Test Adımları

### Senaryo 1: İki Kullanıcı Sohbeti

1. **İlk Tarayıcı Penceresi** (Chrome)
   - http://localhost:5174 adresini aç
   - Rumuz: "Ahmet"
   - Mesaj gönder: "Merhaba, nasılsın?"

2. **İkinci Tarayıcı Penceresi** (Chrome Incognito veya Firefox)
   - http://localhost:5174 adresini aç
   - Rumuz: "Ayşe"
   - 3 saniye bekle (polling çalışsın)
   - Ahmet'in mesajını göreceksin (solda, beyaz)
   - Mesaj gönder: "İyiyim, teşekkürler!"

3. **İlk Pencereye Dön** (Ahmet)
   - 3 saniye içinde Ayşe'nin mesajını göreceksin
   - Kendi mesajın sağda mor, Ayşe'nin mesajı solda beyaz

### Senaryo 2: Çoklu Kullanıcı

1. Üç farklı tarayıcı/pencere aç
2. Farklı rumuzlar ile giriş yap: "Ali", "Veli", "Zeynep"
3. Her birinden mesaj gönder
4. Tüm pencerelerde mesajların görünüp görünmediğini kontrol et

## Beklenen Sonuçlar

✅ Kullanıcılar birbirlerinin mesajlarını görebiliyor
✅ Kendi mesajları sağda mor, diğerleri solda beyaz
✅ Her 3 saniyede otomatik güncelleme
✅ Mesaj gönderince 500ms içinde güncelleme
✅ Otomatik scroll en alta
✅ Duygu analizi etiketleri görünüyor
✅ Kullanıcı adı ve zaman damgası her mesajda

## Teknik Detaylar

- **Polling Interval**: 3000ms (3 saniye)
- **Post-Send Refresh**: 500ms
- **Backend API**: http://localhost:5000
- **Frontend**: http://localhost:5174
- **Database**: SQLite (chat.db)

## Gelecek İyileştirmeler

- WebSocket/SignalR ile gerçek zamanlı bağlantı
- Kullanıcı "yazıyor..." göstergesi
- Mesaj okundu bilgisi
- Emoji desteği
- Dosya paylaşımı
