# Test Backend API with Sentiment Analysis

$baseUrl = "https://sentiment-backend-api.onrender.com"

Write-Host "=== Testing Backend API ===" -ForegroundColor Cyan

# 1. Health Check
Write-Host "`n1. Health Check..." -ForegroundColor Yellow
$health = Invoke-RestMethod -Uri "$baseUrl/health" -Method Get
Write-Host "Health: $($health.status)" -ForegroundColor Green

# 2. Create User
Write-Host "`n2. Creating User..." -ForegroundColor Yellow
$user = Invoke-RestMethod -Uri "$baseUrl/api/users" -Method Post -Body (@{rumuz="TestUser"} | ConvertTo-Json) -ContentType "application/json"
Write-Host "User Created: $($user.rumuz)" -ForegroundColor Green

# 3. Create Room
Write-Host "`n3. Creating Room..." -ForegroundColor Yellow
$room = Invoke-RestMethod -Uri "$baseUrl/api/rooms" -Method Post -Body (@{name="TestRoom"; password="test123"; createdBy="TestUser"} | ConvertTo-Json) -ContentType "application/json"
Write-Host "Room Created: $($room.name) (ID: $($room.id))" -ForegroundColor Green

# 4. Send Message with Sentiment Analysis
Write-Host "`n4. Sending Message (with sentiment analysis)..." -ForegroundColor Yellow
$message = Invoke-RestMethod -Uri "$baseUrl/api/messages" -Method Post -Body (@{rumuz="TestUser"; text="Bu harika bir gün!"; roomId=$room.id} | ConvertTo-Json) -ContentType "application/json"
Write-Host "Message Sent!" -ForegroundColor Green
Write-Host "  Text: $($message.text)" -ForegroundColor White
Write-Host "  Sentiment: $($message.sentimentLabel) (Score: $($message.sentimentScore))" -ForegroundColor Magenta

# 5. Get Messages
Write-Host "`n5. Getting Messages..." -ForegroundColor Yellow
$messages = Invoke-RestMethod -Uri "$baseUrl/api/messages?roomId=$($room.id)" -Method Get
Write-Host "Total Messages: $($messages.Count)" -ForegroundColor Green
foreach ($msg in $messages) {
    Write-Host "  - [$($msg.rumuz)]: $($msg.text) | Sentiment: $($msg.sentimentLabel)" -ForegroundColor White
}

Write-Host "`n=== All Tests Passed! ===" -ForegroundColor Green
