import { useState, useEffect } from 'react'
import UserLogin from './components/UserLogin'
import RoomSelection from './components/RoomSelection'
import ChatWindow from './components/ChatWindow'
import './App.css'

function App() {
  const [rumuz, setRumuz] = useState(null)
  const [room, setRoom] = useState(null)
  const [isLoggedIn, setIsLoggedIn] = useState(false)

  // Check if user is already logged in (rumuz in localStorage)
  useEffect(() => {
    const storedRumuz = localStorage.getItem('rumuz')
    if (storedRumuz) {
      setRumuz(storedRumuz)
      setIsLoggedIn(true)
    }
  }, [])

  const handleLoginSuccess = (userRumuz) => {
    setRumuz(userRumuz)
    setIsLoggedIn(true)
  }

  const handleRoomJoined = (roomData) => {
    // Ensure roomData has required properties
    if (roomData && roomData.id && roomData.name) {
      setRoom(roomData)
      // Store room info in localStorage
      localStorage.setItem('currentRoom', JSON.stringify(roomData))
    } else {
      console.error('Invalid room data:', roomData)
      alert('Oda bilgisi alınamadı. Lütfen tekrar deneyin.')
    }
  }

  const handleLeaveRoom = () => {
    setRoom(null)
    localStorage.removeItem('currentRoom')
  }

  const handleLogout = () => {
    localStorage.removeItem('rumuz')
    localStorage.removeItem('currentRoom')
    setRumuz(null)
    setRoom(null)
    setIsLoggedIn(false)
  }

  // If not logged in, show login screen
  if (!isLoggedIn) {
    return <UserLogin onLoginSuccess={handleLoginSuccess} />
  }

  // If logged in but no room selected, show room selection
  if (!room) {
    return (
      <RoomSelection 
        rumuz={rumuz} 
        onRoomJoined={handleRoomJoined}
        onLogout={handleLogout}
      />
    )
  }

  // If logged in and room selected, show chat screen
  return (
    <div className="app-container">
      <ChatWindow 
        rumuz={rumuz} 
        room={room}
        onLogout={handleLogout}
        onLeaveRoom={handleLeaveRoom}
      />
    </div>
  )
}

export default App
