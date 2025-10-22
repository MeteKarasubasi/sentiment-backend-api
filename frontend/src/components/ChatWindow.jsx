import { useState, useEffect } from 'react';
import { api } from '../services/api';
import MessageList from './MessageList';
import MessageInput from './MessageInput';
import './ChatWindow.css';

function ChatWindow({ rumuz, room, onLogout, onLeaveRoom }) {
  const [messages, setMessages] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  // Fetch messages on component mount and set up polling
  useEffect(() => {
    fetchMessages();
    
    // Poll for new messages every 3 seconds
    const pollInterval = setInterval(() => {
      fetchMessages(true); // Pass true to indicate this is a background fetch
    }, 3000);
    
    // Cleanup interval on unmount
    return () => clearInterval(pollInterval);
  }, [room.id]);

  const fetchMessages = async (isBackgroundFetch = false) => {
    try {
      // Only show loading spinner on initial fetch, not on background polling
      if (!isBackgroundFetch) {
        setLoading(true);
      }
      setError('');
      const response = await api.getMessages(room.id);
      // Backend response might be wrapped or direct array
      const messagesData = Array.isArray(response.data) 
        ? response.data 
        : (response.data?.messages || response.data?.data || []);
      setMessages(messagesData);
    } catch (err) {
      console.error('Failed to fetch messages:', err);
      // Only show error on initial fetch, not on background polling
      if (!isBackgroundFetch) {
        setError('Mesajlar yüklenirken bir hata oluştu');
        setMessages([]); // Set empty array on error
      }
    } finally {
      if (!isBackgroundFetch) {
        setLoading(false);
      }
    }
  };

  const handleSendMessage = async (text) => {
    try {
      setError('');
      const response = await api.sendMessage(rumuz, text, room.id);
      
      // Handle 207 Multi-Status response (message saved but sentiment failed)
      const messageData = response.status === 207 ? response.data.message : response.data;
      
      // Add new message to the list immediately for better UX
      setMessages((prevMessages) => [...prevMessages, messageData]);
      
      // Fetch all messages to ensure we have the latest from other users
      // This will be done by the polling mechanism, but we can trigger it immediately
      setTimeout(() => fetchMessages(true), 500);
      
      // Check if sentiment analysis failed (207 Multi-Status)
      if (response.status === 207) {
        setError('Mesaj gönderildi ancak duygu analizi yapılamadı');
        // Clear error after 5 seconds
        setTimeout(() => setError(''), 5000);
      }
    } catch (err) {
      console.error('Failed to send message:', err);
      
      if (err.response) {
        setError(err.response.data?.message || 'Mesaj gönderilemedi');
      } else if (err.request) {
        setError('Bağlantı hatası. Lütfen internet bağlantınızı kontrol edin.');
      } else {
        setError('Mesaj gönderilemedi. Lütfen tekrar deneyin.');
      }
      
      throw err; // Re-throw to let MessageInput handle it
    }
  };

  return (
    <div className="chat-window">
      <div className="chat-header">
        <div className="chat-header-left">
          <h1 className="chat-title">{room.name}</h1>
          <p className="chat-subtitle">{rumuz} • Oda: {room.name}</p>
        </div>
        <div className="header-buttons">
          <button onClick={onLeaveRoom} className="leave-room-button">
            Odadan Ayrıl
          </button>
          <button onClick={onLogout} className="logout-button">
            Çıkış Yap
          </button>
        </div>
      </div>

      {error && (
        <div className="error-banner">
          <p>{error}</p>
        </div>
      )}

      {loading ? (
        <div className="loading-state">
          <div className="loading-spinner"></div>
          Mesajlar yükleniyor...
        </div>
      ) : (
        <>
          <MessageList messages={messages} currentUserRumuz={rumuz} />
          <MessageInput onSendMessage={handleSendMessage} disabled={false} />
        </>
      )}
    </div>
  );
}

export default ChatWindow;
