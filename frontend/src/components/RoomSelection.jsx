import { useState, useEffect } from 'react';
import { api } from '../services/api';
import './RoomSelection.css';

function RoomSelection({ rumuz, onRoomJoined, onLogout }) {
  const [rooms, setRooms] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [showCreateForm, setShowCreateForm] = useState(false);
  const [showJoinForm, setShowJoinForm] = useState(false);
  const [selectedRoom, setSelectedRoom] = useState(null);
  
  // Form states
  const [newRoomName, setNewRoomName] = useState('');
  const [newRoomPassword, setNewRoomPassword] = useState('');
  const [joinPassword, setJoinPassword] = useState('');

  useEffect(() => {
    fetchRooms();
  }, []);

  const fetchRooms = async () => {
    try {
      setLoading(true);
      const response = await api.getRooms();
      // Backend response might be wrapped or direct array
      const roomsData = Array.isArray(response.data) 
        ? response.data 
        : (response.data?.rooms || response.data?.data || []);
      setRooms(roomsData);
    } catch (err) {
      console.error('Failed to fetch rooms:', err);
      setError('Odalar yüklenirken bir hata oluştu');
      setRooms([]); // Set empty array on error
    } finally {
      setLoading(false);
    }
  };

  const handleCreateRoom = async (e) => {
    e.preventDefault();
    setError('');

    if (!newRoomName.trim()) {
      setError('Oda adı boş olamaz');
      return;
    }

    if (newRoomName.length < 3 || newRoomName.length > 50) {
      setError('Oda adı 3-50 karakter arasında olmalıdır');
      return;
    }

    if (!newRoomPassword || newRoomPassword.length < 4) {
      setError('Şifre en az 4 karakter olmalıdır');
      return;
    }

    try {
      const response = await api.createRoom(newRoomName.trim(), newRoomPassword, rumuz);
      onRoomJoined(response.data);
    } catch (err) {
      if (err.response?.status === 409) {
        setError('Bu oda adı zaten kullanılıyor');
      } else {
        setError(err.response?.data?.message || 'Oda oluşturulamadı');
      }
    }
  };

  const handleJoinRoom = async (e) => {
    e.preventDefault();
    setError('');

    if (!selectedRoom) {
      setError('Lütfen bir oda seçin');
      return;
    }

    if (!joinPassword) {
      setError('Şifre boş olamaz');
      return;
    }

    try {
      const response = await api.joinRoom(selectedRoom.name, joinPassword, rumuz);
      onRoomJoined(response.data);
    } catch (err) {
      if (err.response?.status === 401) {
        setError('Yanlış şifre');
      } else {
        setError(err.response?.data?.message || 'Odaya katılınamadı');
      }
    }
  };

  const openJoinForm = (room) => {
    setSelectedRoom(room);
    setShowJoinForm(true);
    setShowCreateForm(false);
    setError('');
    setJoinPassword('');
  };

  return (
    <div className="room-selection-container">
      <div className="room-selection-card">
        <div className="room-header">
          <div>
            <h1>Oda Seçimi</h1>
            <p className="room-subtitle">Hoş geldin, {rumuz}!</p>
          </div>
          <button onClick={onLogout} className="logout-button-small">
            Çıkış
          </button>
        </div>

        {error && (
          <div className="error-message">
            {error}
          </div>
        )}

        {!showCreateForm && !showJoinForm && (
          <>
            <div className="action-buttons">
              <button 
                onClick={() => {
                  setShowCreateForm(true);
                  setShowJoinForm(false);
                  setError('');
                }}
                className="create-room-button"
              >
                + Yeni Oda Oluştur
              </button>
            </div>

            <div className="rooms-list">
              <h2>Mevcut Odalar</h2>
              {loading ? (
                <div className="loading-state">Odalar yükleniyor...</div>
              ) : rooms.length === 0 ? (
                <div className="empty-state">
                  Henüz oda yok. İlk odayı sen oluştur!
                </div>
              ) : (
                <div className="rooms-grid">
                  {rooms.map((room) => (
                    <div key={room.id} className="room-card">
                      <h3>{room.name}</h3>
                      <p className="room-creator">Oluşturan: {room.createdBy}</p>
                      <button 
                        onClick={() => openJoinForm(room)}
                        className="join-button"
                      >
                        Katıl
                      </button>
                    </div>
                  ))}
                </div>
              )}
            </div>
          </>
        )}

        {showCreateForm && (
          <div className="room-form">
            <h2>Yeni Oda Oluştur</h2>
            <form onSubmit={handleCreateRoom}>
              <div className="form-group">
                <label>Oda Adı</label>
                <input
                  type="text"
                  value={newRoomName}
                  onChange={(e) => setNewRoomName(e.target.value)}
                  placeholder="Oda adını girin"
                  maxLength={50}
                  autoFocus
                />
              </div>
              <div className="form-group">
                <label>Şifre</label>
                <input
                  type="password"
                  value={newRoomPassword}
                  onChange={(e) => setNewRoomPassword(e.target.value)}
                  placeholder="Oda şifresini girin"
                />
                <small>En az 4 karakter</small>
              </div>
              <div className="form-buttons">
                <button type="submit" className="submit-button">
                  Oluştur
                </button>
                <button 
                  type="button" 
                  onClick={() => {
                    setShowCreateForm(false);
                    setNewRoomName('');
                    setNewRoomPassword('');
                    setError('');
                  }}
                  className="cancel-button"
                >
                  İptal
                </button>
              </div>
            </form>
          </div>
        )}

        {showJoinForm && selectedRoom && (
          <div className="room-form">
            <h2>{selectedRoom.name} Odasına Katıl</h2>
            <p className="room-info">Oluşturan: {selectedRoom.createdBy}</p>
            <form onSubmit={handleJoinRoom}>
              <div className="form-group">
                <label>Şifre</label>
                <input
                  type="password"
                  value={joinPassword}
                  onChange={(e) => setJoinPassword(e.target.value)}
                  placeholder="Oda şifresini girin"
                  autoFocus
                />
              </div>
              <div className="form-buttons">
                <button type="submit" className="submit-button">
                  Katıl
                </button>
                <button 
                  type="button" 
                  onClick={() => {
                    setShowJoinForm(false);
                    setSelectedRoom(null);
                    setJoinPassword('');
                    setError('');
                  }}
                  className="cancel-button"
                >
                  İptal
                </button>
              </div>
            </form>
          </div>
        )}
      </div>
    </div>
  );
}

export default RoomSelection;
