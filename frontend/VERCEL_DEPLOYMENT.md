# Frontend Vercel Deployment Guide

## Overview

This guide walks you through deploying the React frontend to Vercel.

## Prerequisites

✅ Backend API deployed on Render: https://sentiment-chat-backend.onrender.com  
✅ GitHub account  
✅ Vercel account (https://vercel.com)

## Step 1: Push Frontend to GitHub

If you haven't already pushed your code to GitHub:

```bash
cd frontend
git init
git add .
git commit -m "Frontend ready for Vercel deployment"
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO.git
git branch -M main
git push -u origin main
```

**Note:** If this is part of a monorepo, make sure the entire project is pushed to GitHub.

## Step 2: Create Vercel Project

### Option A: Vercel Dashboard (Recommended)

1. Go to https://vercel.com/dashboard
2. Click **"Add New..."** → **"Project"**
3. Import your GitHub repository
4. Select your repository from the list

### Option B: Vercel CLI

```bash
npm install -g vercel
cd frontend
vercel
```

## Step 3: Configure Build Settings

Vercel should auto-detect Vite, but verify these settings:

**Framework Preset:** Vite  
**Root Directory:** `frontend` (if monorepo, otherwise leave as `.`)  
**Build Command:** `npm run build`  
**Output Directory:** `dist`  
**Install Command:** `npm install`

## Step 4: Configure Environment Variables

Add the following environment variable in Vercel:

**Key:** `VITE_API_URL`  
**Value:** `https://sentiment-chat-backend.onrender.com`  
**Environment:** Production (and Preview if desired)

### How to Add Environment Variables:

1. In your Vercel project dashboard
2. Go to **Settings** → **Environment Variables**
3. Add the variable:
   - Name: `VITE_API_URL`
   - Value: `https://sentiment-chat-backend.onrender.com`
   - Select: Production, Preview, Development (as needed)
4. Click **Save**

## Step 5: Deploy

1. Click **"Deploy"**
2. Wait for build to complete (1-3 minutes)
3. Note your deployment URL: `https://your-app.vercel.app`

## Step 6: Test Deployment

### Quick Test:
Visit your Vercel URL and verify:
- ✅ Login page loads
- ✅ Can register a user (rumuz)
- ✅ Chat interface appears
- ✅ Can send messages
- ✅ Sentiment labels appear (pozitif/nötr/negatif)
- ✅ Messages are retrieved from backend

### Manual End-to-End Test:

1. **Open the deployed URL** in your browser
2. **Register a user:**
   - Enter a rumuz (e.g., "testuser")
   - Click "Kayıt Ol"
3. **Send messages:**
   - Type: "Bu harika bir gün!" (positive)
   - Type: "Çok kötü bir deneyim" (negative)
   - Type: "Normal bir mesaj" (neutral)
4. **Verify:**
   - Messages appear in the chat
   - Sentiment badges show correct colors
   - Messages persist (refresh page)

### Browser Console Test:

Open browser DevTools (F12) and check:
- No CORS errors
- API requests succeed (Network tab)
- No JavaScript errors (Console tab)

## Troubleshooting

### Issue: CORS Errors

**Symptom:** Browser console shows CORS policy errors  
**Solution:** Verify backend CORS is configured to allow your Vercel domain

### Issue: API Requests Fail

**Symptom:** Messages don't send, users can't register  
**Solution:** 
- Check `VITE_API_URL` environment variable is set correctly
- Verify backend is running: `curl https://sentiment-chat-backend.onrender.com/api/health`
- Check browser Network tab for actual error

### Issue: Environment Variable Not Working

**Symptom:** App tries to connect to localhost  
**Solution:**
- Redeploy after adding environment variables
- Verify variable name is exactly `VITE_API_URL` (case-sensitive)
- Check variable is set for Production environment

### Issue: Build Fails

**Symptom:** Vercel build fails  
**Solution:**
- Check build logs in Vercel dashboard
- Verify `package.json` has correct scripts
- Ensure all dependencies are in `package.json`

### Issue: Blank Page After Deploy

**Symptom:** Deployment succeeds but page is blank  
**Solution:**
- Check browser console for errors
- Verify `dist` folder is being generated
- Check `vercel.json` rewrites configuration

## Configuration Files

### vercel.json
```json
{
  "buildCommand": "npm run build",
  "outputDirectory": "dist",
  "framework": "vite",
  "rewrites": [
    {
      "source": "/(.*)",
      "destination": "/index.html"
    }
  ]
}
```

### .env.production
```
VITE_API_URL=https://sentiment-chat-backend.onrender.com
```

## Vercel Features

### Automatic Deployments
- Every push to `main` branch triggers a new deployment
- Pull requests get preview deployments
- Rollback to previous deployments anytime

### Custom Domain (Optional)
1. Go to **Settings** → **Domains**
2. Add your custom domain
3. Configure DNS records as instructed

### Analytics (Optional)
- Enable Vercel Analytics in project settings
- Track page views and performance

## Performance Optimization

### Recommended Settings:
- ✅ Enable compression (automatic)
- ✅ Enable caching (automatic)
- ✅ Use CDN (automatic)

### Optional Optimizations:
- Add `vercel.json` headers for caching
- Enable Vercel Speed Insights
- Configure image optimization

## Security

### HTTPS
- Automatic HTTPS for all deployments
- SSL certificates managed by Vercel

### Environment Variables
- Never commit `.env.production` to Git
- Use Vercel dashboard to manage secrets

## Monitoring

### Deployment Logs
- View build logs in Vercel dashboard
- Check runtime logs for errors

### Health Check
- Monitor backend health: https://sentiment-chat-backend.onrender.com/api/health
- Set up Vercel monitoring (optional)

## Next Steps

After successful deployment:

1. ✅ Save your Vercel URL
2. ✅ Test all features thoroughly
3. ✅ Update main project README with deployment URL
4. ✅ Proceed to Task 11: React Native mobile app
5. ✅ Share the app with users!

## Useful Commands

### Redeploy from CLI:
```bash
vercel --prod
```

### View Logs:
```bash
vercel logs
```

### Remove Deployment:
```bash
vercel remove [deployment-url]
```

## Resources

- Vercel Documentation: https://vercel.com/docs
- Vite Deployment Guide: https://vitejs.dev/guide/static-deploy.html#vercel
- Vercel CLI Reference: https://vercel.com/docs/cli

## Support

If you encounter issues:
1. Check Vercel build logs
2. Verify environment variables
3. Test backend API directly
4. Check browser console for errors
5. Review Vercel documentation

---

**Estimated Time:** 10-15 minutes  
**Cost:** Free tier available  
**Difficulty:** Easy

Good luck with your deployment! 🚀
