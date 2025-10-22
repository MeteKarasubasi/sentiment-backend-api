import { useEffect, useRef } from 'react';
import MessageItem from './MessageItem';
import './MessageList.css';

function MessageList({ messages }) {
  const messagesEndRef = useRef(null);

  // Auto-scroll to bottom when messages change
  useEffect(() => {
    scrollToBottom();
  }, [messages]);

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' });
  };

  if (messages.length === 0) {
    return (
      <div className="message-list">
        <div className="empty-state">
          <p>Henüz mesaj yok. İlk mesajı sen gönder! 💬</p>
        </div>
      </div>
    );
  }

  return (
    <div className="message-list">
      {messages.map((message) => (
        <MessageItem key={message.id} message={message} />
      ))}
      <div ref={messagesEndRef} />
    </div>
  );
}

export default MessageList;
