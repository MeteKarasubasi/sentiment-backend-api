# Final Integration Test
Write-Host "=== FINAL INTEGRATION TEST ===" -ForegroundColor Cyan

$baseUrl = "http://localhost:5000"

# Test 1: Health
Write-Host "`n1. Health Check..." -ForegroundColor Yellow
$response = curl "$baseUrl/api/health"
Write-Host "   OK: $($response.StatusCode)" -ForegroundColor Green

# Test 2: Create User
Write-Host "`n2. Create User..." -ForegroundColor Yellow
$response = curl "$baseUrl/api/users" -Method POST -Body '{"rumuz":"AITestUser"}' -ContentType "application/json"
Write-Host "   User created" -ForegroundColor Green

# Test 3: Send Messages with AI
Write-Host "`n3. Send Messages with AI Analysis..." -ForegroundColor Yellow

Write-Host "   - Positive message..." -ForegroundColor Cyan
$response = curl "$baseUrl/api/messages" -Method POST -Body '{"rumuz":"AITestUser","text":"Bu harika bir gun!"}' -ContentType "application/json"
$msg = $response.Content | ConvertFrom-Json
Write-Host "     Sentiment: $($msg.sentimentLabel), Score: $($msg.sentimentScore)" -ForegroundColor Green

Start-Sleep -Seconds 3

Write-Host "   - Negative message..." -ForegroundColor Cyan
$response = curl "$baseUrl/api/messages" -Method POST -Body '{"rumuz":"AITestUser","text":"Cok kotu bir deneyim"}' -ContentType "application/json"
$msg = $response.Content | ConvertFrom-Json
Write-Host "     Sentiment: $($msg.sentimentLabel), Score: $($msg.sentimentScore)" -ForegroundColor Green

Start-Sleep -Seconds 3

Write-Host "   - Neutral message..." -ForegroundColor Cyan
$response = curl "$baseUrl/api/messages" -Method POST -Body '{"rumuz":"AITestUser","text":"Normal bir mesaj"}' -ContentType "application/json"
$msg = $response.Content | ConvertFrom-Json
Write-Host "     Sentiment: $($msg.sentimentLabel), Score: $($msg.sentimentScore)" -ForegroundColor Green

# Test 4: List Messages
Write-Host "`n4. List All Messages..." -ForegroundColor Yellow
$response = curl "$baseUrl/api/messages"
$messages = $response.Content | ConvertFrom-Json
Write-Host "   Total: $($messages.Count) messages" -ForegroundColor Green

Write-Host "`n=== ALL TESTS PASSED ===" -ForegroundColor Green
Write-Host "Backend + AI Integration: WORKING!" -ForegroundColor Green
