# Test script for Render-deployed Backend API
# Usage: .\test_render_api.ps1 -ApiUrl "https://your-app-name.onrender.com"

param(
    [Parameter(Mandatory=$true)]
    [string]$ApiUrl
)

Write-Host "Testing API at: $ApiUrl" -ForegroundColor Cyan
Write-Host "================================" -ForegroundColor Cyan

# Test 1: Health Check
Write-Host "`nTest 1: Health Check" -ForegroundColor Yellow
Write-Host "GET $ApiUrl/api/health"
try {
    $response = Invoke-RestMethod -Uri "$ApiUrl/api/health" -Method Get
    $response | ConvertTo-Json
} catch {
    Write-Host "Error: $_" -ForegroundColor Red
}

# Test 2: Register User
Write-Host "`nTest 2: Register User" -ForegroundColor Yellow
Write-Host "POST $ApiUrl/api/users"
try {
    $body = @{
        rumuz = "testuser123"
    } | ConvertTo-Json
    
    $response = Invoke-RestMethod -Uri "$ApiUrl/api/users" -Method Post -Body $body -ContentType "application/json"
    $response | ConvertTo-Json
} catch {
    Write-Host "Error: $_" -ForegroundColor Red
}

# Test 3: Get All Users
Write-Host "`nTest 3: Get All Users" -ForegroundColor Yellow
Write-Host "GET $ApiUrl/api/users"
try {
    $response = Invoke-RestMethod -Uri "$ApiUrl/api/users" -Method Get
    $response | ConvertTo-Json
} catch {
    Write-Host "Error: $_" -ForegroundColor Red
}

# Test 4: Send Message with Positive Sentiment
Write-Host "`nTest 4: Send Message (Positive Sentiment)" -ForegroundColor Yellow
Write-Host "POST $ApiUrl/api/messages"
try {
    $body = @{
        rumuz = "testuser123"
        text = "This is an amazing day! I love it!"
    } | ConvertTo-Json
    
    $response = Invoke-RestMethod -Uri "$ApiUrl/api/messages" -Method Post -Body $body -ContentType "application/json"
    $response | ConvertTo-Json
} catch {
    Write-Host "Error: $_" -ForegroundColor Red
}

# Test 5: Send Message with Negative Sentiment
Write-Host "`nTest 5: Send Message (Negative Sentiment)" -ForegroundColor Yellow
Write-Host "POST $ApiUrl/api/messages"
try {
    $body = @{
        rumuz = "testuser123"
        text = "This is terrible and awful."
    } | ConvertTo-Json
    
    $response = Invoke-RestMethod -Uri "$ApiUrl/api/messages" -Method Post -Body $body -ContentType "application/json"
    $response | ConvertTo-Json
} catch {
    Write-Host "Error: $_" -ForegroundColor Red
}

# Test 6: Send Message with Neutral Sentiment
Write-Host "`nTest 6: Send Message (Neutral Sentiment)" -ForegroundColor Yellow
Write-Host "POST $ApiUrl/api/messages"
try {
    $body = @{
        rumuz = "testuser123"
        text = "The weather is cloudy today."
    } | ConvertTo-Json
    
    $response = Invoke-RestMethod -Uri "$ApiUrl/api/messages" -Method Post -Body $body -ContentType "application/json"
    $response | ConvertTo-Json
} catch {
    Write-Host "Error: $_" -ForegroundColor Red
}

# Test 7: Get All Messages
Write-Host "`nTest 7: Get All Messages" -ForegroundColor Yellow
Write-Host "GET $ApiUrl/api/messages"
try {
    $response = Invoke-RestMethod -Uri "$ApiUrl/api/messages" -Method Get
    $response | ConvertTo-Json
} catch {
    Write-Host "Error: $_" -ForegroundColor Red
}

# Test 8: Test Duplicate User
Write-Host "`nTest 8: Test Duplicate User (Should Fail)" -ForegroundColor Yellow
Write-Host "POST $ApiUrl/api/users"
try {
    $body = @{
        rumuz = "testuser123"
    } | ConvertTo-Json
    
    $response = Invoke-RestMethod -Uri "$ApiUrl/api/users" -Method Post -Body $body -ContentType "application/json"
    $response | ConvertTo-Json
} catch {
    Write-Host "Expected Error (409 Conflict): $_" -ForegroundColor Green
}

Write-Host "`n================================" -ForegroundColor Cyan
Write-Host "All tests completed!" -ForegroundColor Cyan
