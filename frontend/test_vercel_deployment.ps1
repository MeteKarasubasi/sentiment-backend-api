# PowerShell script to test Vercel deployment
# Usage: .\test_vercel_deployment.ps1 -AppUrl "https://your-app.vercel.app"

param(
    [Parameter(Mandatory=$true)]
    [string]$AppUrl
)

Write-Host "`n=== Vercel Deployment Test ===" -ForegroundColor Cyan
Write-Host "Testing: $AppUrl`n" -ForegroundColor Cyan

# Remove trailing slash if present
$AppUrl = $AppUrl.TrimEnd('/')

# Test 1: Check if app is accessible
Write-Host "Test 1: Checking if app is accessible..." -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri $AppUrl -Method Get -UseBasicParsing
    if ($response.StatusCode -eq 200) {
        Write-Host "✅ App is accessible (Status: 200)" -ForegroundColor Green
    } else {
        Write-Host "⚠️  App returned status: $($response.StatusCode)" -ForegroundColor Yellow
    }
} catch {
    Write-Host "❌ Failed to access app: $_" -ForegroundColor Red
    exit 1
}

# Test 2: Check if HTML contains React root
Write-Host "`nTest 2: Checking if React app loads..." -ForegroundColor Yellow
if ($response.Content -match 'id="root"') {
    Write-Host "✅ React root element found" -ForegroundColor Green
} else {
    Write-Host "⚠️  React root element not found" -ForegroundColor Yellow
}

# Test 3: Check if backend API is accessible from frontend
Write-Host "`nTest 3: Checking backend API connectivity..." -ForegroundColor Yellow
$backendUrl = "https://sentiment-chat-backend.onrender.com"
try {
    $healthResponse = Invoke-RestMethod -Uri "$backendUrl/api/health" -Method Get
    Write-Host "✅ Backend API is accessible" -ForegroundColor Green
    Write-Host "   Status: $($healthResponse.status)" -ForegroundColor Gray
} catch {
    Write-Host "❌ Backend API not accessible: $_" -ForegroundColor Red
}

# Test 4: Test user registration through backend
Write-Host "`nTest 4: Testing user registration..." -ForegroundColor Yellow
$testRumuz = "vercel-test-$(Get-Random -Minimum 1000 -Maximum 9999)"
$userBody = @{
    rumuz = $testRumuz
} | ConvertTo-Json

try {
    $userResponse = Invoke-RestMethod -Uri "$backendUrl/api/users" -Method Post -Body $userBody -ContentType "application/json"
    Write-Host "✅ User registration successful" -ForegroundColor Green
    Write-Host "   User ID: $($userResponse.id)" -ForegroundColor Gray
    Write-Host "   Rumuz: $($userResponse.rumuz)" -ForegroundColor Gray
    $userId = $userResponse.id
} catch {
    Write-Host "❌ User registration failed: $_" -ForegroundColor Red
    $userId = $null
}

# Test 5: Test message sending with sentiment analysis
if ($userId) {
    Write-Host "`nTest 5: Testing message sending with sentiment..." -ForegroundColor Yellow
    
    $testMessages = @(
        @{ text = "Bu harika bir gün!"; expected = "pozitif" },
        @{ text = "Çok kötü bir deneyim"; expected = "negatif" },
        @{ text = "Normal bir mesaj"; expected = "nötr" }
    )
    
    foreach ($msg in $testMessages) {
        $messageBody = @{
            rumuz = $testRumuz
            text = $msg.text
        } | ConvertTo-Json
        
        try {
            $msgResponse = Invoke-RestMethod -Uri "$backendUrl/api/messages" -Method Post -Body $messageBody -ContentType "application/json"
            Write-Host "✅ Message sent: '$($msg.text)'" -ForegroundColor Green
            Write-Host "   Sentiment: $($msgResponse.sentimentLabel) (Score: $($msgResponse.sentimentScore))" -ForegroundColor Gray
        } catch {
            Write-Host "⚠️  Message failed: '$($msg.text)' - $_" -ForegroundColor Yellow
        }
        Start-Sleep -Milliseconds 500
    }
}

# Test 6: Test message retrieval
Write-Host "`nTest 6: Testing message retrieval..." -ForegroundColor Yellow
try {
    $messagesResponse = Invoke-RestMethod -Uri "$backendUrl/api/messages" -Method Get
    $messageCount = $messagesResponse.Count
    Write-Host "✅ Messages retrieved successfully" -ForegroundColor Green
    Write-Host "   Total messages: $messageCount" -ForegroundColor Gray
    
    if ($messageCount -gt 0) {
        $latestMessage = $messagesResponse[-1]
        Write-Host "   Latest message: '$($latestMessage.text)'" -ForegroundColor Gray
        Write-Host "   Sentiment: $($latestMessage.sentimentLabel)" -ForegroundColor Gray
    }
} catch {
    Write-Host "❌ Message retrieval failed: $_" -ForegroundColor Red
}

# Test 7: Check for common deployment issues
Write-Host "`nTest 7: Checking for common issues..." -ForegroundColor Yellow

# Check if CORS is properly configured
Write-Host "   Checking CORS configuration..." -ForegroundColor Gray
try {
    $headers = @{
        "Origin" = $AppUrl
    }
    $corsResponse = Invoke-WebRequest -Uri "$backendUrl/api/health" -Method Options -Headers $headers -UseBasicParsing
    if ($corsResponse.Headers["Access-Control-Allow-Origin"]) {
        Write-Host "   ✅ CORS headers present" -ForegroundColor Green
    } else {
        Write-Host "   ⚠️  CORS headers not found" -ForegroundColor Yellow
    }
} catch {
    Write-Host "   ⚠️  Could not verify CORS: $_" -ForegroundColor Yellow
}

# Summary
Write-Host "`n=== Test Summary ===" -ForegroundColor Cyan
Write-Host "Frontend URL: $AppUrl" -ForegroundColor White
Write-Host "Backend URL: $backendUrl" -ForegroundColor White
Write-Host "`nNext Steps:" -ForegroundColor Cyan
Write-Host "1. Open $AppUrl in your browser" -ForegroundColor White
Write-Host "2. Register with a rumuz" -ForegroundColor White
Write-Host "3. Send test messages" -ForegroundColor White
Write-Host "4. Verify sentiment labels appear" -ForegroundColor White
Write-Host "5. Check browser console for errors (F12)" -ForegroundColor White
Write-Host "`nDeployment test complete! 🚀`n" -ForegroundColor Green
