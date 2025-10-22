#!/bin/bash

# Test script for Render-deployed Backend API
# Usage: ./test_render_api.sh https://your-app-name.onrender.com

if [ -z "$1" ]; then
    echo "Usage: ./test_render_api.sh <RENDER_URL>"
    echo "Example: ./test_render_api.sh https://sentiment-chat-backend.onrender.com"
    exit 1
fi

API_URL=$1
echo "Testing API at: $API_URL"
echo "================================"

# Test 1: Health Check
echo ""
echo "Test 1: Health Check"
echo "GET $API_URL/api/health"
curl -s "$API_URL/api/health" | jq '.'
echo ""

# Test 2: Register User
echo ""
echo "Test 2: Register User"
echo "POST $API_URL/api/users"
REGISTER_RESPONSE=$(curl -s -X POST "$API_URL/api/users" \
  -H "Content-Type: application/json" \
  -d '{"rumuz": "testuser123"}')
echo "$REGISTER_RESPONSE" | jq '.'
echo ""

# Test 3: Get All Users
echo ""
echo "Test 3: Get All Users"
echo "GET $API_URL/api/users"
curl -s "$API_URL/api/users" | jq '.'
echo ""

# Test 4: Send Message with Positive Sentiment
echo ""
echo "Test 4: Send Message (Positive Sentiment)"
echo "POST $API_URL/api/messages"
curl -s -X POST "$API_URL/api/messages" \
  -H "Content-Type: application/json" \
  -d '{"rumuz": "testuser123", "text": "This is an amazing day! I love it!"}' | jq '.'
echo ""

# Test 5: Send Message with Negative Sentiment
echo ""
echo "Test 5: Send Message (Negative Sentiment)"
echo "POST $API_URL/api/messages"
curl -s -X POST "$API_URL/api/messages" \
  -H "Content-Type: application/json" \
  -d '{"rumuz": "testuser123", "text": "This is terrible and awful."}' | jq '.'
echo ""

# Test 6: Send Message with Neutral Sentiment
echo ""
echo "Test 6: Send Message (Neutral Sentiment)"
echo "POST $API_URL/api/messages"
curl -s -X POST "$API_URL/api/messages" \
  -H "Content-Type: application/json" \
  -d '{"rumuz": "testuser123", "text": "The weather is cloudy today."}' | jq '.'
echo ""

# Test 7: Get All Messages
echo ""
echo "Test 7: Get All Messages"
echo "GET $API_URL/api/messages"
curl -s "$API_URL/api/messages" | jq '.'
echo ""

# Test 8: Test Duplicate User
echo ""
echo "Test 8: Test Duplicate User (Should Fail)"
echo "POST $API_URL/api/users"
curl -s -X POST "$API_URL/api/users" \
  -H "Content-Type: application/json" \
  -d '{"rumuz": "testuser123"}' | jq '.'
echo ""

echo "================================"
echo "All tests completed!"
