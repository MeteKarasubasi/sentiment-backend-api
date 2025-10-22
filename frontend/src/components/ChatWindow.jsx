import { useState, useEffect } from 'react';
import { api } from '../services/api';
import MessageList from './MessageList';
import MessageInput from './MessageInput';
import './ChatWindow.css';

function ChatWindow({ rumuz, onLogout }) {
  const [messages, setMessages] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  // Fetch messages on component mount
  useEffect(() => {
    fetchMessages();
  }, []);

  const fetchMessages = async () => {
    try {
      setLoading(true);
      setError('');
      const response = await api.getMessages();
      setMessages(response.data || []);
    } catch (err) {
      console.error('Failed to fetch messages:', err);
      setError('Mesajlar yüklenirken bir hata oluştu');
    } finally {
      setLoading(false);
    }
  };

  const handleSendMessage = async (text) => {
    try {
      setError('');
      const response = await api.sendMessage(rumuz, text);
      
      // Add new message to the list
      setMessages((prevMessages) => [...prevMessages, response.data]);
      
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
          <h1 className="chat-title">Sentiment Chat</h1>
          <p className="chat-subtitle">Hoş geldin, {rumuz}!</p>
        </div>
        <button onClick={onLogout} className="logout-button">
          Çıkış Yap
        </button>
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
          <MessageList messages={messages} />
          <MessageInput onSendMessage={handleSendMessage} disabled={false} />
        </>
      )}
    </div>
  );
}

export default ChatWindow;
