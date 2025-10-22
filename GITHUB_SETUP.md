# 📦 GitHub Repository Hazırlık Rehberi

Bu dokümanda projenin GitHub'a nasıl yükleneceği ve teslim için nasıl hazırlanacağı anlatılmaktadır.

## 1️⃣ Repository Oluşturma

### Adım 1: GitHub'da Yeni Repository
1. https://github.com adresine gidin
2. "New repository" butonuna tıklayın
3. Repository adı: `sentiment-chat-app`
4. Description: `Real-time chat application with AI-powered sentiment analysis`
5. Public seçin
6. "Create repository" butonuna tıklayın

### Adım 2: Local Git Initialization
```bash
cd sentiment-chat
git init
git add .
git commit -m "Initial commit: Sentiment Chat App with AI"
git branch -M main
git remote add origin https://github.com/[username]/sentiment-chat-app.git
git push -u origin main
```

---

## 2️⃣ .gitignore Dosyası

Proje root'unda `.gitignore` dosyası oluşturun:

```gitignore
# Dependencies
node_modules/
.pnp
.pnp.js

# Testing
coverage/

# Production
build/
dist/

# Misc
.DS_Store
.env
.env.local
.env.development.local
.env.test.local
.env.production.local

# Logs
npm-debug.log*
yarn-debug.log*
yarn-error.log*
lerna-debug.log*

# IDE
.vscode/
.idea/
*.swp
*.swo
*~

# Backend
backend/bin/
backend/obj/
backend/*.db
backend/*.db-shm
backend/*.db-wal
backend/out/

# Mobile
mobile/android/app/build/
mobile/ios/build/
mobile/.expo/
mobile/.expo-shared/

# Python
ai-service/__pycache__/
ai-service/*.pyc
ai-service/.pytest_cache/
ai-service/venv/
ai-service/.venv/

# OS
Thumbs.db
```

---

## 3️⃣ Repository Yapısı

```
sentiment-chat-app/
├── .gitignore
├── README.md                          # Ana dokümantasyon
├── CODE_OWNERSHIP_PROOF.md            # Kod hakimiyeti kanıtı
├── DEPLOYMENT_GUIDE_FINAL.md          # Deployment rehberi
├── LICENSE                            # MIT License
│
├── frontend/                          # React Web App
│   ├── src/
│   ├── public/
│   ├── package.json
│   ├── .env.example
│   └── README.md
│
├── backend/                           # .NET Core API
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── Data/
│   ├── Program.cs
│   └── README.md
│
├── ai-service/                        # Python AI Service
│   ├── app.py
│   ├── requirements.txt
│   └── README.md
│
└── mobile/                            # React Native App
    ├── src/
    ├── android/
    ├── ios/
    ├── package.json
    └── README.md
```

---

## 4️⃣ README.md Güncellemeleri

Ana `README.md` dosyasını şu bilgilerle güncelleyin:

### Demo Linkleri Bölümü
```markdown
## 🌐 Live Demo

- **Web Application:** https://sentiment-chat.vercel.app
- **Backend API:** https://sentiment-chat-api.onrender.com
- **API Documentation:** https://sentiment-chat-api.onrender.com/swagger
- **AI Service:** https://huggingface.co/spaces/[username]/sentiment-chat-ai
- **Mobile APK:** [Google Drive Link veya GitHub Release]

> **Not:** Demo linkleri deployment sonrası güncellenecektir.
```

### Badges Ekleyin
```markdown
# Sentiment Chat

![React](https://img.shields.io/badge/React-19-blue)
![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![Python](https://img.shields.io/badge/Python-3.9-green)
![License](https://img.shields.io/badge/License-MIT-yellow)

Gerçek zamanlı duygu analizi ile çalışan chat uygulaması
```

---

## 5️⃣ LICENSE Dosyası

`LICENSE` dosyası oluşturun:

```
MIT License

Copyright (c) 2025 [Your Name]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

---

## 6️⃣ GitHub Actions (Opsiyonel)

`.github/workflows/ci.yml` dosyası oluşturun:

```yaml
name: CI

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  frontend-test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup Node.js
        uses: actions/setup-node@v3
        with:
          node-version: '18'
      - name: Install dependencies
        run: cd frontend && npm install
      - name: Run tests
        run: cd frontend && npm test

  backend-test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '8.0.x'
      - name: Restore dependencies
        run: cd backend && dotnet restore
      - name: Build
        run: cd backend && dotnet build
      - name: Test
        run: cd backend.Tests && dotnet test
```

---

## 7️⃣ GitHub Releases

### APK Release Oluşturma

1. Mobile APK'yı build edin:
```bash
cd mobile/android
./gradlew assembleRelease
```

2. GitHub'da Release oluşturun:
   - Releases → "Create a new release"
   - Tag: `v1.0.0`
   - Title: `Sentiment Chat v1.0.0`
   - Description: Release notes
   - APK dosyasını upload edin

---

## 8️⃣ Repository Topics

GitHub repository'nizde şu topic'leri ekleyin:

```
react
react-native
dotnet
csharp
python
ai
machine-learning
sentiment-analysis
chat-application
gradio
huggingface
bert
real-time
websocket
```

---

## 9️⃣ Repository Settings

### Branch Protection
1. Settings → Branches
2. "Add rule" butonuna tıklayın
3. Branch name pattern: `main`
4. Şunları aktif edin:
   - Require pull request reviews before merging
   - Require status checks to pass before merging

### Secrets (Deployment için)
1. Settings → Secrets and variables → Actions
2. Şu secret'ları ekleyin:
   - `VERCEL_TOKEN`
   - `RENDER_API_KEY`
   - `HF_TOKEN`

---

## 🔟 Commit Message Convention

Commit mesajlarınızı şu formatta yazın:

```
feat: Add room management system
fix: Fix polling mechanism in ChatWindow
docs: Update README with deployment guide
style: Improve MessageItem CSS
refactor: Optimize API service layer
test: Add unit tests for MessagesController
chore: Update dependencies
```

---

## 1️⃣1️⃣ Pull Request Template

`.github/PULL_REQUEST_TEMPLATE.md` dosyası oluşturun:

```markdown
## Description
<!-- Describe your changes -->

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Testing
- [ ] Tested locally
- [ ] Added unit tests
- [ ] Tested on mobile

## Screenshots
<!-- If applicable -->

## Checklist
- [ ] Code follows project style guidelines
- [ ] Self-review completed
- [ ] Comments added for complex code
- [ ] Documentation updated
- [ ] No new warnings generated
```

---

## 1️⃣2️⃣ Issue Templates

`.github/ISSUE_TEMPLATE/bug_report.md`:

```markdown
---
name: Bug Report
about: Create a report to help us improve
title: '[BUG] '
labels: bug
assignees: ''
---

**Describe the bug**
A clear description of the bug.

**To Reproduce**
Steps to reproduce:
1. Go to '...'
2. Click on '...'
3. See error

**Expected behavior**
What you expected to happen.

**Screenshots**
If applicable, add screenshots.

**Environment:**
 - OS: [e.g. Windows, macOS]
 - Browser: [e.g. Chrome, Safari]
 - Version: [e.g. 1.0.0]
```

---

## 1️⃣3️⃣ Repository Checklist

Teslim öncesi kontrol listesi:

- [ ] README.md tamamlandı
- [ ] CODE_OWNERSHIP_PROOF.md eklendi
- [ ] DEPLOYMENT_GUIDE_FINAL.md eklendi
- [ ] LICENSE dosyası eklendi
- [ ] .gitignore yapılandırıldı
- [ ] Demo linkleri güncellendi
- [ ] Tüm dosyalar commit edildi
- [ ] Repository public yapıldı
- [ ] Topics eklendi
- [ ] Description güncellendi
- [ ] APK release oluşturuldu
- [ ] GitHub Actions çalışıyor (opsiyonel)

---

## 1️⃣4️⃣ Final Push

```bash
# Tüm değişiklikleri commit edin
git add .
git commit -m "docs: Complete documentation for submission"
git push origin main

# Tag oluşturun
git tag -a v1.0.0 -m "Version 1.0.0 - Initial Release"
git push origin v1.0.0
```

---

## 1️⃣5️⃣ Repository URL

Teslim için repository URL'iniz:

```
https://github.com/[username]/sentiment-chat-app
```

Bu URL'i teslim formuna ekleyin.

---

## ✅ Teslim Hazır!

Repository'niz artık teslim için hazır. Şu özelliklere sahip:

1. ✅ Temiz ve organize klasör yapısı
2. ✅ Kapsamlı README.md
3. ✅ Kod hakimiyeti kanıtı
4. ✅ Deployment rehberi
5. ✅ Demo linkleri
6. ✅ License
7. ✅ .gitignore
8. ✅ Commit history
9. ✅ Release (APK)
10. ✅ Topics ve description

**Başarılar! 🎉**
