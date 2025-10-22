# Backend API Render Deployment Guide

This guide walks you through deploying the Sentiment Chat Backend API to Render.

## Prerequisites

1. A GitHub account
2. A Render account (sign up at https://render.com)
3. The AI Service URL from Hugging Face Spaces (from Task 1)

## Step 1: Prepare GitHub Repository

### Option A: Create a New Repository

1. Go to https://github.com/new
2. Create a new repository (e.g., `sentiment-chat-backend`)
3. Do NOT initialize with README, .gitignore, or license (we already have these)

### Option B: Use Existing Repository

If you already have a repository for the entire project, you can use that.

## Step 2: Push Backend Code to GitHub

Open a terminal in the `backend` directory and run:

```bash
# Initialize git repository (if not already done)
git init

# Add all files
git add .

# Commit the files
git commit -m "Initial backend commit for Render deployment"

# Add your GitHub repository as remote
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO_NAME.git

# Push to GitHub
git branch -M main
git push -u origin main
```

**Note:** If you're using a monorepo (all code in one repository), make sure the backend folder is included.

## Step 3: Create Render Web Service

1. Log in to your Render dashboard at https://dashboard.render.com
2. Click **"New +"** button in the top right
3. Select **"Web Service"**

## Step 4: Connect GitHub Repository

1. Click **"Connect a repository"** or **"Configure account"** if first time
2. Authorize Render to access your GitHub account
3. Select your repository from the list
4. Click **"Connect"**

## Step 5: Configure Web Service

Fill in the following settings:

### Basic Settings
- **Name:** `sentiment-chat-backend` (or your preferred name)
- **Region:** Choose closest to your users
- **Branch:** `main`
- **Root Directory:** `backend` (if using monorepo, otherwise leave empty)
- **Runtime:** `Docker` or `.NET`

### Build Settings
- **Build Command:** 
  ```
  dotnet publish -c Release -o out
  ```

### Deploy Settings
- **Start Command:**
  ```
  dotnet out/backend.dll
  ```

### Instance Type
- Select **"Free"** (or paid plan if you prefer)

## Step 6: Configure Environment Variables

Click **"Advanced"** and add the following environment variables:

1. **ASPNETCORE_ENVIRONMENT**
   - Value: `Production`

2. **ASPNETCORE_URLS**
   - Value: `http://0.0.0.0:$PORT`
   - Note: Render automatically provides the PORT variable

3. **AiService__Url**
   - Value: Your Hugging Face Spaces endpoint URL
   - Example: `https://your-space-name.hf.space/api/predict`
   - **IMPORTANT:** Use double underscore `__` for nested configuration

4. **ConnectionStrings__DefaultConnection**
   - Value: `Data Source=/var/data/chat.db`
   - This uses persistent disk storage

## Step 7: Add Persistent Disk (for SQLite Database)

1. Scroll down to **"Disks"** section
2. Click **"Add Disk"**
3. Configure:
   - **Name:** `chat-data`
   - **Mount Path:** `/var/data`
   - **Size:** `1 GB` (Free tier allows up to 1GB)

This ensures your SQLite database persists across deployments.

## Step 8: Deploy

1. Review all settings
2. Click **"Create Web Service"**
3. Render will start building and deploying your application
4. Wait for the deployment to complete (usually 2-5 minutes)

## Step 9: Verify Deployment

Once deployed, Render will provide you with a URL like:
```
https://sentiment-chat-backend.onrender.com
```

### Test the Health Endpoint

Open your browser or use curl:

```bash
curl https://your-app-name.onrender.com/api/health
```

Expected response:
```json
{
  "status": "Healthy",
  "timestamp": "2024-01-01T12:00:00Z"
}
```

### Test User Registration

```bash
curl -X POST https://your-app-name.onrender.com/api/users \
  -H "Content-Type: application/json" \
  -d '{"rumuz": "testuser"}'
```

Expected response:
```json
{
  "id": 1,
  "rumuz": "testuser",
  "createdAt": "2024-01-01T12:00:00Z"
}
```

### Test Message Sending

```bash
curl -X POST https://your-app-name.onrender.com/api/messages \
  -H "Content-Type: application/json" \
  -d '{"rumuz": "testuser", "text": "This is a great day!"}'
```

Expected response:
```json
{
  "id": 1,
  "rumuz": "testuser",
  "text": "This is a great day!",
  "sentimentLabel": "pozitif",
  "sentimentScore": 0.95,
  "createdAt": "2024-01-01T12:00:00Z"
}
```

### Test Message Retrieval

```bash
curl https://your-app-name.onrender.com/api/messages
```

## Step 10: Monitor and Troubleshoot

### View Logs

1. Go to your Render dashboard
2. Click on your web service
3. Click **"Logs"** tab to see real-time logs

### Common Issues

#### Issue: Build fails with "dotnet not found"
**Solution:** Make sure Runtime is set to `.NET` or use Docker

#### Issue: Application crashes on startup
**Solution:** Check logs for errors. Common causes:
- Missing environment variables
- Database connection issues
- Port binding issues

#### Issue: AI Service connection fails
**Solution:** 
- Verify `AiService__Url` environment variable is correct
- Test the Hugging Face endpoint directly
- Check Hugging Face Spaces is running

#### Issue: Database resets after deployment
**Solution:** 
- Ensure persistent disk is properly configured
- Verify mount path is `/var/data`
- Check ConnectionStrings uses `/var/data/chat.db`

#### Issue: CORS errors from frontend
**Solution:** 
- Backend already has `AllowAll` CORS policy
- If issues persist, check browser console for specific errors

## Step 11: Update Deployment

To deploy updates:

1. Make changes to your code locally
2. Commit and push to GitHub:
   ```bash
   git add .
   git commit -m "Your update message"
   git push
   ```
3. Render will automatically detect the push and redeploy

You can also manually trigger a deploy from the Render dashboard.

## Important Notes

### Free Tier Limitations

- **Spin down:** Free services spin down after 15 minutes of inactivity
- **Spin up time:** First request after spin down takes 30-60 seconds
- **Monthly hours:** 750 hours per month (enough for one service running 24/7)

### Database Backups

Render Free tier does not include automatic backups. Consider:
- Periodically downloading the database file
- Using a paid plan for automatic backups
- Implementing your own backup solution

### Environment Variables

Remember to use double underscore `__` for nested configuration in .NET:
- `AiService__Url` maps to `AiService:Url` in appsettings.json
- `ConnectionStrings__DefaultConnection` maps to `ConnectionStrings:DefaultConnection`

## Next Steps

After successful deployment:

1. Save your Render URL for frontend configuration
2. Test all API endpoints thoroughly
3. Proceed to Task 7: React Web Frontend setup
4. Configure frontend to use your Render backend URL

## Useful Links

- Render Documentation: https://render.com/docs
- Render .NET Guide: https://render.com/docs/deploy-dotnet
- Render Environment Variables: https://render.com/docs/environment-variables
- Render Persistent Disks: https://render.com/docs/disks

## Support

If you encounter issues:
1. Check Render logs first
2. Review this guide's troubleshooting section
3. Consult Render documentation
4. Check Render community forum
