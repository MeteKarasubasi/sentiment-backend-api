import './MessageItem.css';

function MessageItem({ message, currentUserRumuz }) {
  const { rumuz, text, sentimentLabel, sentimentScore, createdAt } = message;
  const isOwnMessage = rumuz === currentUserRumuz;

  // Get sentiment badge class based on label
  const getSentimentClass = () => {
    if (!sentimentLabel) return 'sentiment-badge neutral';
    
    const label = sentimentLabel.toLowerCase();
    if (label === 'pozitif' || label === 'positive') return 'sentiment-badge positive';
    if (label === 'negatif' || label === 'negative') return 'sentiment-badge negative';
    return 'sentiment-badge neutral';
  };

  // Format timestamp
  const formatTime = (timestamp) => {
    const date = new Date(timestamp);
    return date.toLocaleTimeString('tr-TR', { 
      hour: '2-digit', 
      minute: '2-digit' 
    });
  };

  return (
    <div className={`message-item ${isOwnMessage ? 'own-message' : 'other-message'}`}>
      <div className="message-header">
        <span className="message-rumuz">{rumuz}</span>
        <span className="message-time">{formatTime(createdAt)}</span>
      </div>
      <div className="message-content">
        <p className="message-text">{text}</p>
        {sentimentLabel && (
          <div className={getSentimentClass()}>
            {sentimentLabel}
            {sentimentScore && (
              <span className="sentiment-score">
                {(sentimentScore * 100).toFixed(0)}%
              </span>
            )}
          </div>
        )}
      </div>
    </div>
  );
}

export default MessageItem;
