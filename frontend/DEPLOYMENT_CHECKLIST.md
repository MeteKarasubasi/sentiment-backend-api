# Frontend Vercel Deployment Checklist

Use this checklist to track your deployment progress.

## Pre-Deployment

### Code Preparation
- [x] Frontend application built and tested locally
- [x] All components working correctly
- [x] API integration tested with backend
- [x] Environment variables configured
- [x] Build command verified: `npm run build`

### Configuration Files
- [x] `vercel.json` created with correct settings
- [x] `.env.production` created with backend URL
- [x] `.gitignore` includes `.env` files
- [x] `package.json` has correct build scripts

### Backend Verification
- [ ] Backend is deployed and accessible
- [ ] Test backend health: `curl https://sentiment-chat-backend.onrender.com/api/health`
- [ ] Backend CORS allows frontend domain
- [ ] Backend API endpoints working

## GitHub Setup

- [ ] Code pushed to GitHub repository
- [ ] Repository is public or accessible to Vercel
- [ ] All files committed (check `git status`)
- [ ] Latest changes pushed to `main` branch

## Vercel Account Setup

- [ ] Vercel account created at https://vercel.com
- [ ] GitHub account connected to Vercel
- [ ] Can access Vercel dashboard

## Vercel Project Configuration

### Import Project
- [ ] Clicked "Add New..." → "Project" in Vercel dashboard
- [ ] Selected GitHub repository
- [ ] Repository imported successfully

### Build Settings
- [ ] Framework Preset: **Vite** (auto-detected)
- [ ] Root Directory: `frontend` (if monorepo) or `.` (if standalone)
- [ ] Build Command: `npm run build`
- [ ] Output Directory: `dist`
- [ ] Install Command: `npm install`

### Environment Variables
- [ ] Added `VITE_API_URL` environment variable
- [ ] Value: `https://sentiment-chat-backend.onrender.com`
- [ ] Applied to: Production (minimum)
- [ ] Saved successfully

## Deployment

- [ ] Clicked "Deploy" button
- [ ] Build started successfully
- [ ] Build completed without errors (check logs)
- [ ] Deployment URL generated (e.g., `https://your-app.vercel.app`)
- [ ] Saved deployment URL for reference

## Post-Deployment Testing

### Basic Functionality
- [ ] Deployment URL opens in browser
- [ ] Login page loads correctly
- [ ] No console errors in browser DevTools
- [ ] CSS styles applied correctly
- [ ] Images/assets load correctly

### User Registration
- [ ] Can enter a rumuz (username)
- [ ] "Kayıt Ol" button works
- [ ] Redirects to chat screen after registration
- [ ] No CORS errors in console

### Chat Functionality
- [ ] Chat interface displays correctly
- [ ] Message input field works
- [ ] Can type messages
- [ ] "Gönder" button works
- [ ] Messages appear in chat list

### Sentiment Analysis
- [ ] Send positive message (e.g., "Bu harika bir gün!")
- [ ] Sentiment badge appears (should show "pozitif" or similar)
- [ ] Send negative message (e.g., "Çok kötü bir deneyim")
- [ ] Sentiment badge appears (should show "negatif" or similar)
- [ ] Send neutral message (e.g., "Normal bir mesaj")
- [ ] Sentiment badge appears (should show "nötr" or similar)
- [ ] Sentiment colors display correctly (green/red/gray)

### Data Persistence
- [ ] Refresh the page
- [ ] Messages still appear
- [ ] Can send new messages after refresh
- [ ] Message history persists

### API Integration
- [ ] Open browser DevTools → Network tab
- [ ] Send a message
- [ ] Verify API request to backend succeeds (200 status)
- [ ] Check response contains sentiment data
- [ ] No CORS errors

### Cross-Browser Testing (Optional)
- [ ] Test in Chrome
- [ ] Test in Firefox
- [ ] Test in Safari (if available)
- [ ] Test in Edge

### Mobile Responsiveness (Optional)
- [ ] Open DevTools → Toggle device toolbar
- [ ] Test on mobile viewport
- [ ] UI adapts to small screens
- [ ] All features work on mobile

## Troubleshooting

### If Build Fails
- [ ] Check Vercel build logs for errors
- [ ] Verify `package.json` dependencies
- [ ] Test build locally: `npm run build`
- [ ] Check for TypeScript/ESLint errors

### If CORS Errors Occur
- [ ] Verify backend CORS configuration
- [ ] Check backend allows Vercel domain
- [ ] Test backend directly: `curl https://sentiment-chat-backend.onrender.com/api/health`

### If Environment Variables Don't Work
- [ ] Verify variable name: `VITE_API_URL` (case-sensitive)
- [ ] Check variable is set for Production environment
- [ ] Redeploy after adding variables
- [ ] Check build logs show correct API URL

### If Page is Blank
- [ ] Check browser console for errors
- [ ] Verify `vercel.json` rewrites configuration
- [ ] Check `dist` folder is generated correctly
- [ ] Test build locally: `npm run build && npm run preview`

## Documentation

- [ ] Update main project README with Vercel URL
- [ ] Document deployment process
- [ ] Add screenshots of deployed app
- [ ] Note any issues encountered and solutions

## Optional Enhancements

- [ ] Set up custom domain
- [ ] Enable Vercel Analytics
- [ ] Configure automatic deployments for branches
- [ ] Set up preview deployments for PRs
- [ ] Add deployment status badge to README

## Final Verification

- [ ] All features working end-to-end
- [ ] No console errors
- [ ] Performance is acceptable
- [ ] User experience is smooth
- [ ] Ready for user testing

## Success Criteria

✅ Deployment URL accessible  
✅ User can register with rumuz  
✅ User can send messages  
✅ Sentiment analysis works  
✅ Messages persist across refreshes  
✅ No CORS or API errors  
✅ UI displays correctly  

---

**Deployment Status:** [ ] Complete  
**Deployment URL:** ___________________________  
**Deployed Date:** ___________________________  
**Deployed By:** ___________________________

## Next Steps

After successful deployment:
1. Share the URL with team/users
2. Proceed to Task 11: React Native mobile app
3. Monitor for any issues
4. Gather user feedback

---

**Estimated Time:** 10-15 minutes  
**Difficulty:** Easy  
**Cost:** Free tier available

Congratulations on your deployment! 🎉
