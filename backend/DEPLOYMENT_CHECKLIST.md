# Backend Deployment Checklist

Use this checklist to ensure all steps are completed for Render deployment.

## Pre-Deployment

- [ ] AI Service is deployed and running on Hugging Face Spaces
- [ ] AI Service URL is available and tested
- [ ] Backend code is complete and tested locally
- [ ] All unit tests pass (`dotnet test` in backend.Tests)
- [ ] GitHub account is ready
- [ ] Render account is created (https://render.com)

## GitHub Setup

- [ ] Create GitHub repository (or use existing monorepo)
- [ ] Initialize git in backend directory (if needed)
- [ ] Add all files to git
- [ ] Commit changes
- [ ] Add GitHub remote
- [ ] Push code to GitHub main branch

### Commands:
```bash
cd backend
git init
git add .
git commit -m "Initial backend commit for Render deployment"
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO.git
git branch -M main
git push -u origin main
```

## Render Configuration

- [ ] Log in to Render dashboard
- [ ] Click "New +" → "Web Service"
- [ ] Connect GitHub repository
- [ ] Select repository from list

### Service Settings:
- [ ] Name: `sentiment-chat-backend` (or your choice)
- [ ] Region: Selected
- [ ] Branch: `main`
- [ ] Root Directory: `backend` (if monorepo)
- [ ] Runtime: `.NET` or `Docker`

### Build Settings:
- [ ] Build Command: `dotnet publish -c Release -o out`
- [ ] Start Command: `dotnet out/backend.dll`

### Instance:
- [ ] Instance Type: Free (or paid)

## Environment Variables

Add these in Render dashboard under "Environment":

- [ ] `ASPNETCORE_ENVIRONMENT` = `Production`
- [ ] `ASPNETCORE_URLS` = `http://0.0.0.0:$PORT`
- [ ] `AiService__Url` = `https://your-space.hf.space/api/predict`
- [ ] `ConnectionStrings__DefaultConnection` = `Data Source=/var/data/chat.db`

**Important:** Use double underscore `__` for nested config!

## Persistent Disk

- [ ] Click "Add Disk" in Disks section
- [ ] Name: `chat-data`
- [ ] Mount Path: `/var/data`
- [ ] Size: `1 GB`

## Deploy

- [ ] Review all settings
- [ ] Click "Create Web Service"
- [ ] Wait for build to complete (2-5 minutes)
- [ ] Check logs for any errors
- [ ] Note the deployed URL (e.g., https://sentiment-chat-backend.onrender.com)

## Post-Deployment Testing

### Test 1: Health Check
- [ ] Test: `curl https://your-app.onrender.com/api/health`
- [ ] Expected: `{"status":"Healthy","timestamp":"..."}`

### Test 2: Swagger UI
- [ ] Visit: `https://your-app.onrender.com/swagger`
- [ ] Verify: Swagger UI loads with all endpoints

### Test 3: User Registration
- [ ] Test: POST to `/api/users` with `{"rumuz": "testuser"}`
- [ ] Expected: User created with ID and timestamp

### Test 4: Message with Sentiment
- [ ] Test: POST to `/api/messages` with rumuz and text
- [ ] Expected: Message saved with sentiment label and score

### Test 5: Get Messages
- [ ] Test: GET `/api/messages`
- [ ] Expected: Array of messages with sentiment data

### Test 6: Duplicate User
- [ ] Test: POST same rumuz again
- [ ] Expected: 409 Conflict error

### Automated Testing
- [ ] Run test script: `.\test_render_api.ps1 -ApiUrl "https://your-app.onrender.com"`
- [ ] All tests pass

## Documentation

- [ ] Save Render URL for frontend configuration
- [ ] Update main project README with deployment URL
- [ ] Document any issues encountered and solutions

## Troubleshooting

If deployment fails, check:

- [ ] Build logs in Render dashboard
- [ ] Environment variables are correct (especially double underscores)
- [ ] AI Service URL is accessible
- [ ] Persistent disk is properly mounted
- [ ] .NET runtime is selected

## Next Steps

- [ ] Proceed to Task 7: React Web Frontend setup
- [ ] Configure frontend with Render backend URL
- [ ] Test end-to-end integration

## Notes

Record your deployment details:

- **Render URL:** _______________________________________________
- **Deployment Date:** _______________________________________________
- **AI Service URL:** _______________________________________________
- **Issues Encountered:** _______________________________________________
- **Solutions Applied:** _______________________________________________

---

## Quick Reference

### Render Dashboard
https://dashboard.render.com

### View Logs
Dashboard → Your Service → Logs tab

### Redeploy
Dashboard → Your Service → Manual Deploy → Deploy latest commit

### Environment Variables
Dashboard → Your Service → Environment tab

### Disk Management
Dashboard → Your Service → Disks tab
