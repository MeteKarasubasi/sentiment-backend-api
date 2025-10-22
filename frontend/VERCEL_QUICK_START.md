# Quick Start: Deploy Frontend to Vercel

This is a condensed guide for deploying the React frontend to Vercel. For detailed instructions, see [VERCEL_DEPLOYMENT.md](VERCEL_DEPLOYMENT.md).

## Prerequisites

✅ Backend deployed: https://sentiment-chat-backend.onrender.com  
✅ GitHub account  
✅ Vercel account (https://vercel.com)

## Step 1: Push to GitHub (if not already done)

```bash
cd frontend
git init
git add .
git commit -m "Frontend ready for Vercel"
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO.git
git branch -M main
git push -u origin main
```

## Step 2: Import to Vercel (3 minutes)

1. Go to https://vercel.com/dashboard
2. Click **"Add New..."** → **"Project"**
3. Select your GitHub repository
4. Configure:
   - **Root Directory:** `frontend` (if monorepo)
   - **Framework:** Vite (auto-detected)
   - **Build Command:** `npm run build`
   - **Output Directory:** `dist`

## Step 3: Add Environment Variable (1 minute)

In Vercel project settings:

**Key:** `VITE_API_URL`  
**Value:** `https://sentiment-chat-backend.onrender.com`  
**Environment:** Production

## Step 4: Deploy (2 minutes)

1. Click **"Deploy"**
2. Wait for build (1-3 minutes)
3. Note your URL: `https://your-app.vercel.app`

## Step 5: Test (3 minutes)

### Quick Browser Test:
1. Open your Vercel URL
2. Register with a rumuz
3. Send messages:
   - "Bu harika bir gün!" (should show pozitif)
   - "Çok kötü bir deneyim" (should show negatif)
4. Verify sentiment badges appear

### Automated Test:
```powershell
.\test_vercel_deployment.ps1 -AppUrl "https://your-app.vercel.app"
```

## Expected Results

✅ App loads without errors  
✅ Can register users  
✅ Can send messages  
✅ Sentiment analysis works  
✅ Messages persist  

## Common Issues

**CORS errors:** Backend CORS should allow all origins  
**API fails:** Check `VITE_API_URL` is set correctly  
**Blank page:** Check browser console for errors  
**Build fails:** Run `npm run build` locally first  

## Next Steps

✅ Save your Vercel URL  
✅ Update project README  
✅ Proceed to Task 11: React Native mobile app  

---

**Total Time:** ~10 minutes  
**Cost:** Free tier available

Good luck! 🚀
