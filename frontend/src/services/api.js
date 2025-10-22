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
   * Send a new message
   * @param {string} rumuz - The username sending the message
   * @param {string} text - The message text
   * @returns {Promise} Response with message data including sentiment
   */
  sendMessage: (rumuz, text) => {
    return apiClient.post('/api/messages', { rumuz, text });
  },

  /**
   * Get all messages with optional pagination
   * @param {number} page - Page number (default: 1)
   * @param {number} pageSize - Number of messages per page (default: 50)
   * @returns {Promise} Response with array of messages
   */
  getMessages: (page = 1, pageSize = 50) => {
    return apiClient.get('/api/messages', {
      params: { page, pageSize },
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
