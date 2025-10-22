# Quick Start: Deploy Backend to Render

This is a condensed guide for deploying the backend API to Render. For detailed instructions, see [RENDER_DEPLOYMENT.md](RENDER_DEPLOYMENT.md).

## Prerequisites

✅ AI Service deployed on Hugging Face Spaces  
✅ GitHub account  
✅ Render account (https://render.com)

## Step 1: Push to GitHub (5 minutes)

```bash
cd backend
git init
git add .
git commit -m "Backend ready for Render deployment"
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO.git
git branch -M main
git push -u origin main
```

## Step 2: Create Render Web Service (2 minutes)

1. Go to https://dashboard.render.com
2. Click **New +** → **Web Service**
3. Connect your GitHub repository
4. Select your repository

## Step 3: Configure Service (3 minutes)

**Basic Settings:**
- Name: `sentiment-chat-backend`
- Root Directory: `backend` (if monorepo, otherwise leave empty)
- Runtime: `.NET`

**Build & Deploy:**
- Build Command: `dotnet publish -c Release -o out`
- Start Command: `dotnet out/backend.dll`

**Environment Variables:**
```
ASPNETCORE_ENVIRONMENT = Production
ASPNETCORE_URLS = http://0.0.0.0:$PORT
AiService__Url = https://your-space.hf.space/api/predict
ConnectionStrings__DefaultConnection = Data Source=/var/data/chat.db
```

**Disk (for SQLite):**
- Name: `chat-data`
- Mount Path: `/var/data`
- Size: `1 GB`

## Step 4: Deploy (5 minutes)

1. Click **Create Web Service**
2. Wait for build to complete
3. Note your URL: `https://your-app.onrender.com`

## Step 5: Test (2 minutes)

### Quick Test:
```bash
curl https://your-app.onrender.com/api/health
```

### Full Test Suite:
```powershell
.\test_render_api.ps1 -ApiUrl "https://your-app.onrender.com"
```

## Expected Results

✅ Health check returns `{"status":"Healthy"}`  
✅ Can register users  
✅ Can send messages with sentiment analysis  
✅ Can retrieve messages  
✅ Swagger UI accessible at `/swagger`

## Common Issues

**Build fails:** Check Runtime is set to `.NET`  
**App crashes:** Verify environment variables (use `__` not `:`)  
**AI fails:** Check `AiService__Url` is correct  
**Database resets:** Ensure disk is mounted at `/var/data`

## Next Steps

✅ Save your Render URL  
✅ Proceed to Task 7: React Frontend  
✅ Configure frontend with your backend URL

## Support

- Full guide: [RENDER_DEPLOYMENT.md](RENDER_DEPLOYMENT.md)
- Checklist: [DEPLOYMENT_CHECKLIST.md](DEPLOYMENT_CHECKLIST.md)
- Render docs: https://render.com/docs/deploy-dotnet

---

**Total Time:** ~15-20 minutes  
**Cost:** Free tier available
