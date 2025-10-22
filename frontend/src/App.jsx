import { useState, useEffect } from 'react'
import UserLogin from './components/UserLogin'
import ChatWindow from './components/ChatWindow'
import './App.css'

function App() {
  const [rumuz, setRumuz] = useState(null)
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

  const handleLogout = () => {
    localStorage.removeItem('rumuz')
    setRumuz(null)
    setIsLoggedIn(false)
  }

  // If not logged in, show login screen
  if (!isLoggedIn) {
    return <UserLogin onLoginSuccess={handleLoginSuccess} />
  }

  // If logged in, show chat screen
  return (
    <div className="app-container">
      <ChatWindow rumuz={rumuz} onLogout={handleLogout} />
    </div>
  )
}

export default App
