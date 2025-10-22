import { useState } from 'react';
import { api } from '../services/api';
import './UserLogin.css';

function UserLogin({ onLoginSuccess }) {
  const [rumuz, setRumuz] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    // Validation
    if (!rumuz.trim()) {
      setError('Lütfen bir rumuz girin');
      return;
    }

    if (rumuz.length < 3 || rumuz.length > 20) {
      setError('Rumuz 3-20 karakter arasında olmalıdır');
      return;
    }

    setLoading(true);

    try {
      const response = await api.registerUser(rumuz.trim());
      
      // Store rumuz in localStorage
      localStorage.setItem('rumuz', rumuz.trim());
      
      // Call success callback to navigate to chat
      if (onLoginSuccess) {
        onLoginSuccess(rumuz.trim());
      }
    } catch (err) {
      if (err.response) {
        // API returned an error
        if (err.response.status === 409) {
          setError('Bu rumuz zaten kullanılıyor. Lütfen başka bir rumuz seçin.');
        } else if (err.response.data?.message) {
          setError(err.response.data.message);
        } else {
          setError('Kayıt sırasında bir hata oluştu');
        }
      } else if (err.request) {
        // Network error
        setError('Bağlantı hatası. Lütfen internet bağlantınızı kontrol edin.');
      } else {
        setError('Bir hata oluştu. Lütfen tekrar deneyin.');
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login-container">
      <div className="login-card">
        <h1>Sentiment Chat</h1>
        <p className="login-subtitle">Duygularınızı paylaşın, AI analiz etsin</p>
        
        <form onSubmit={handleSubmit} className="login-form">
          <div className="form-group">
            <label htmlFor="rumuz">Rumuz</label>
            <input
              type="text"
              id="rumuz"
              value={rumuz}
              onChange={(e) => setRumuz(e.target.value)}
              placeholder="Rumuzunuzu girin"
              disabled={loading}
              maxLength={20}
              autoFocus
            />
            <small className="form-hint">3-20 karakter arası</small>
          </div>

          {error && (
            <div className="error-message">
              {error}
            </div>
          )}

          <button 
            type="submit" 
            className="login-button"
            disabled={loading}
          >
            {loading ? 'Kayıt yapılıyor...' : 'Sohbete Başla'}
          </button>
        </form>
      </div>
    </div>
  );
}

export default UserLogin;
