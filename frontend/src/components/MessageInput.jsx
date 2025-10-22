import { useState } from 'react';
import './MessageInput.css';

function MessageInput({ onSendMessage, disabled }) {
  const [text, setText] = useState('');
  const [isSending, setIsSending] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    const trimmedText = text.trim();
    if (!trimmedText || isSending) return;

    setIsSending(true);
    
    try {
      await onSendMessage(trimmedText);
      setText(''); // Clear input after successful send
    } catch (error) {
      // Error handling is done in parent component
      console.error('Failed to send message:', error);
    } finally {
      setIsSending(false);
    }
  };

  const handleKeyPress = (e) => {
    // Send on Enter, but allow Shift+Enter for new line
    if (e.key === 'Enter' && !e.shiftKey) {
      e.preventDefault();
      handleSubmit(e);
    }
  };

  return (
    <div className="message-input-container">
      <form onSubmit={handleSubmit} className="message-input-form">
        <textarea
          value={text}
          onChange={(e) => setText(e.target.value)}
          onKeyPress={handleKeyPress}
          placeholder="Mesajınızı yazın..."
          disabled={disabled || isSending}
          rows={1}
          maxLength={500}
          className="message-textarea"
        />
        <button
          type="submit"
          disabled={!text.trim() || disabled || isSending}
          className="send-button"
        >
          {isSending ? '⏳' : '📤'}
        </button>
      </form>
    </div>
  );
}

export default MessageInput;
