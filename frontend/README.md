# Sentiment Chat App - Frontend

React web application for the Sentiment Chat App with real-time sentiment analysis.

## Features

- User registration with unique username (rumuz)
- Real-time chat interface
- Sentiment analysis display (pozitif/nötr/negatif)
- Color-coded sentiment indicators
- Responsive design

## Tech Stack

- React 19
- Vite (with Rolldown)
- Axios for API calls
- CSS for styling

## Prerequisites

- Node.js 18+ and npm
- Backend API running (default: http://localhost:5000)

## Setup

1. Install dependencies:
```bash
npm install
```

2. Configure environment variables:
```bash
cp .env.example .env
```

Edit `.env` and set your backend API URL:
```
VITE_API_URL=http://localhost:5000
```

3. Start development server:
```bash
npm run dev
```

The app will be available at http://localhost:5173

## Available Scripts

- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run preview` - Preview production build
- `npm run lint` - Run ESLint

## API Service

The `src/services/api.js` module provides the following functions:

- `registerUser(rumuz)` - Register a new user
- `sendMessage(rumuz, text)` - Send a message with sentiment analysis
- `getMessages(page, pageSize)` - Get all messages with pagination
- `getUserById(id)` - Get user by ID
- `getAllUsers()` - Get all users
- `healthCheck()` - Check API health

## Environment Variables

- `VITE_API_URL` - Backend API base URL (default: http://localhost:5000)

## Deployment

### Production Deployment (Vercel)

This app is deployed on Vercel. For detailed deployment instructions, see:

- **Quick Start:** [VERCEL_QUICK_START.md](VERCEL_QUICK_START.md) - 10-minute guide
- **Detailed Guide:** [VERCEL_DEPLOYMENT.md](VERCEL_DEPLOYMENT.md) - Comprehensive instructions
- **Checklist:** [DEPLOYMENT_CHECKLIST.md](DEPLOYMENT_CHECKLIST.md) - Step-by-step tracking

**Quick Deploy:**

1. Push code to GitHub
2. Import to Vercel: https://vercel.com/new
3. Configure:
   - Root Directory: `frontend` (if monorepo)
   - Framework: Vite (auto-detected)
   - Environment Variable: `VITE_API_URL=https://sentiment-chat-backend.onrender.com`
4. Deploy

**Test Deployment:**
```powershell
.\test_vercel_deployment.ps1 -AppUrl "https://your-app.vercel.app"
```

### Configuration Files

- `vercel.json` - Vercel deployment configuration
- `.env.production` - Production environment variables
- `.env` - Local development variables

## Project Structure

```
frontend/
├── src/
│   ├── assets/          # Static assets
│   ├── components/      # React components (to be added)
│   ├── services/        # API service layer
│   │   └── api.js      # API client with axios
│   ├── App.jsx         # Main app component
│   ├── App.css         # App styles
│   ├── main.jsx        # Entry point
│   └── index.css       # Global styles
├── public/             # Public assets
├── .env                # Environment variables (not committed)
├── .env.example        # Environment variables template
└── package.json        # Dependencies and scripts
```

## Components

### Implemented Components ✅

- **UserLogin** - User registration with rumuz
- **ChatWindow** - Main chat container
- **MessageList** - Displays message history
- **MessageItem** - Individual message with sentiment badge
- **MessageInput** - Text input and send button

### Features ✅

- ✅ User registration
- ✅ Real-time message sending
- ✅ Sentiment analysis display
- ✅ Color-coded sentiment badges (green/red/gray)
- ✅ Auto-scroll to latest message
- ✅ Message persistence
- ✅ Responsive design

## Testing

Run tests:
```bash
npm test
```

Test files:
- `src/services/api.test.js` - API service tests
- `src/components/MessageItem.test.jsx` - MessageItem component tests
- `src/components/MessageInput.test.jsx` - MessageInput component tests
