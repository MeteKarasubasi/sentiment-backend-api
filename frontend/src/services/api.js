import axios from 'axios';

// API base URL - defaults to localhost for development
const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000';

// Create axios instance with default config
const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 10000, // 10 second timeout
});

// API service functions
export const api = {
  /**
   * Register a new user with a rumuz (username)
   * @param {string} rumuz - The username to register
   * @returns {Promise} Response with user data
   */
  registerUser: (rumuz) => {
    return apiClient.post('/api/users', { rumuz });
  },

  /**
   * Create a new room
   * @param {string} name - Room name
   * @param {string} password - Room password
   * @param {string} createdBy - Username creating the room
   * @returns {Promise} Response with room data
   */
  createRoom: (name, password, createdBy) => {
    return apiClient.post('/api/rooms', { name, password, createdBy });
  },

  /**
   * Join an existing room
   * @param {string} name - Room name
   * @param {string} password - Room password
   * @param {string} rumuz - Username joining the room
   * @returns {Promise} Response with room data
   */
  joinRoom: (name, password, rumuz) => {
    return apiClient.post('/api/rooms/join', { name, password, rumuz });
  },

  /**
   * Get all available rooms
   * @returns {Promise} Response with array of rooms
   */
  getRooms: () => {
    return apiClient.get('/api/rooms');
  },

  /**
   * Send a new message
   * @param {string} rumuz - The username sending the message
   * @param {string} text - The message text
   * @param {number} roomId - The room ID
   * @returns {Promise} Response with message data including sentiment
   */
  sendMessage: (rumuz, text, roomId) => {
    return apiClient.post('/api/messages', { rumuz, text, roomId });
  },

  /**
   * Get messages from a specific room with optional pagination
   * @param {number} roomId - Room ID
   * @param {number} page - Page number (default: 1)
   * @param {number} pageSize - Number of messages per page (default: 50)
   * @returns {Promise} Response with array of messages
   */
  getMessages: (roomId, page = 1, pageSize = 50) => {
    return apiClient.get('/api/messages', {
      params: { roomId, page, pageSize },
    });
  },

  /**
   * Get a specific user by ID
   * @param {number} id - User ID
   * @returns {Promise} Response with user data
   */
  getUserById: (id) => {
    return apiClient.get(`/api/users/${id}`);
  },

  /**
   * Get all users
   * @returns {Promise} Response with array of users
   */
  getAllUsers: () => {
    return apiClient.get('/api/users');
  },

  /**
   * Health check endpoint
   * @returns {Promise} Response with health status
   */
  healthCheck: () => {
    return apiClient.get('/api/health');
  },
};

export default api;
