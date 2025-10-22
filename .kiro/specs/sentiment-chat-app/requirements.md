# Requirements Document

## Introduction

Bu proje, kullanıcıların mesajlaşabildiği ve her mesajın yapay zeka tarafından duygu analizi yapıldığı (pozitif/nötr/negatif) tam kapsamlı bir chat uygulamasıdır. Sistem üç ana bileşenden oluşur: Hugging Face üzerinde çalışan AI duygu analiz servisi, .NET Core backend API ve React/React Native frontend uygulamaları. Proje, modern cloud deployment araçları (Render, Vercel, Hugging Face Spaces) kullanılarak deploy edilecektir.

## Glossary

- **AI Service**: Hugging Face Spaces üzerinde deploy edilen, metin girişi alıp duygu analizi sonucu döndüren Python/Gradio tabanlı servis
- **Backend API**: .NET Core ile geliştirilmiş, SQLite veritabanı kullanan ve Render üzerinde host edilen RESTful API servisi
- **Web Frontend**: React ile geliştirilmiş, Vercel üzerinde deploy edilen web arayüzü
- **Mobile App**: React Native ile geliştirilmiş mobil uygulama
- **Sentiment Analysis**: Bir metnin duygusal tonunu (pozitif, nötr, negatif) belirleme işlemi
- **User Rumuz**: Kullanıcının sistemde tanımlandığı benzersiz takma ad

## Requirements

### Requirement 1: AI Duygu Analiz Servisi

**User Story:** As a system, I want to analyze the sentiment of text messages, so that users can see emotional context in conversations

#### Acceptance Criteria

1. THE AI Service SHALL accept text input via HTTP POST request
2. WHEN a text input is received, THE AI Service SHALL return sentiment analysis result in JSON format containing label (pozitif/nötr/negatif) and confidence score
3. THE AI Service SHALL use a pre-trained Hugging Face model (such as cardiffnlp/twitter-roberta-base-sentiment)
4. THE AI Service SHALL be deployed on Hugging Face Spaces with a publicly accessible endpoint
5. THE AI Service SHALL respond within 5 seconds for text inputs up to 500 characters

### Requirement 2: Kullanıcı Yönetimi

**User Story:** As a user, I want to register with a username (rumuz), so that I can participate in chat conversations

#### Acceptance Criteria

1. THE Backend API SHALL provide a POST endpoint for user registration that accepts a rumuz parameter
2. WHEN a valid rumuz is provided, THE Backend API SHALL store the rumuz in SQLite database
3. THE Backend API SHALL validate that rumuz is not empty and contains between 3 and 20 characters
4. IF a rumuz already exists in database, THEN THE Backend API SHALL return an appropriate error message
5. THE Backend API SHALL return a success response with user identifier upon successful registration

### Requirement 3: Mesaj Gönderme ve Kaydetme

**User Story:** As a user, I want to send messages that are automatically analyzed for sentiment, so that emotional context is preserved

#### Acceptance Criteria

1. THE Backend API SHALL provide a POST endpoint that accepts user rumuz and message text
2. WHEN a message is received, THE Backend API SHALL call the AI Service to obtain sentiment analysis
3. THE Backend API SHALL store the message text, user rumuz, sentiment label, and sentiment score in SQLite database
4. THE Backend API SHALL return the saved message with sentiment data to the client
5. IF the AI Service is unavailable, THEN THE Backend API SHALL store the message with null sentiment data and return an error indicator

### Requirement 4: Mesaj Listeleme

**User Story:** As a user, I want to view all chat messages with their sentiment scores, so that I can see the conversation history

#### Acceptance Criteria

1. THE Backend API SHALL provide a GET endpoint that returns all messages from the database
2. THE Backend API SHALL return messages in chronological order (oldest to newest)
3. WHEN messages are retrieved, THE Backend API SHALL include user rumuz, message text, sentiment label, sentiment score, and timestamp for each message
4. THE Backend API SHALL support pagination with configurable page size
5. THE Backend API SHALL return an empty array when no messages exist

### Requirement 5: Web Arayüzü

**User Story:** As a user, I want to use a web interface to chat and see sentiment analysis, so that I can access the application from any browser

#### Acceptance Criteria

1. THE Web Frontend SHALL display a chat interface with message list area and input field
2. WHEN a user enters a rumuz, THE Web Frontend SHALL store it for subsequent message submissions
3. THE Web Frontend SHALL send messages to the Backend API and display them in the chat list
4. THE Web Frontend SHALL display sentiment indicators (pozitif/nötr/negatif) next to each message
5. THE Web Frontend SHALL be deployed on Vercel with a publicly accessible URL

### Requirement 6: Mobil Uygulama

**User Story:** As a user, I want to use a mobile app to chat and see sentiment analysis, so that I can access the application from my mobile device

#### Acceptance Criteria

1. THE Mobile App SHALL provide the same chat functionality as the Web Frontend
2. THE Mobile App SHALL connect to the same Backend API endpoints
3. THE Mobile App SHALL display messages with sentiment indicators in a mobile-optimized layout
4. THE Mobile App SHALL be buildable as an Android APK
5. THE Mobile App SHALL run successfully on Android emulator or physical device

### Requirement 7: Backend Deployment

**User Story:** As a developer, I want the backend API deployed on a cloud platform, so that it is accessible to frontend applications

#### Acceptance Criteria

1. THE Backend API SHALL be deployed on Render using the Free Web Service plan
2. THE Backend API SHALL use SQLite database for data persistence
3. THE Backend API SHALL expose CORS-enabled endpoints accessible from web and mobile clients
4. THE Backend API SHALL provide a health check endpoint for monitoring
5. THE Backend API SHALL maintain database state across deployments

### Requirement 8: Proje Dokümantasyonu

**User Story:** As a developer or evaluator, I want comprehensive documentation, so that I can understand, run, and evaluate the project

#### Acceptance Criteria

1. THE project SHALL include a README.md file in the root directory
2. THE README.md SHALL document local setup instructions for all three components (AI service, backend, frontend)
3. THE README.md SHALL list all AI tools used (Hugging Face model, ChatGPT, Copilot, etc.)
4. THE README.md SHALL provide working demo links for Vercel, Render, and Hugging Face Spaces deployments
5. THE README.md SHALL explain key code sections and identify which parts were AI-generated versus manually written
