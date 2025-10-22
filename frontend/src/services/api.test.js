import { describe, it, expect, vi, beforeEach } from 'vitest';
import axios from 'axios';

// Mock axios module
vi.mock('axios', () => {
  const mockAxiosInstance = {
    post: vi.fn(),
    get: vi.fn(),
  };
  
  return {
    default: {
      create: vi.fn(() => mockAxiosInstance),
    },
  };
});

describe('API Service', () => {
  let api;
  let mockAxiosInstance;

  beforeEach(async () => {
    // Clear all mocks
    vi.clearAllMocks();
    
    // Get the mocked axios instance
    mockAxiosInstance = axios.create();
    
    // Re-import the api module to get fresh instance
    const apiModule = await import('./api.js?t=' + Date.now());
    api = apiModule.api;
  });

  describe('registerUser', () => {
    it('calls POST /api/users with rumuz', async () => {
      const mockResponse = { data: { id: 1, rumuz: 'testuser' } };
      mockAxiosInstance.post.mockResolvedValue(mockResponse);

      const result = await api.registerUser('testuser');

      expect(mockAxiosInstance.post).toHaveBeenCalledWith('/api/users', { rumuz: 'testuser' });
      expect(result).toEqual(mockResponse);
    });
  });

  describe('sendMessage', () => {
    it('calls POST /api/messages with rumuz and text', async () => {
      const mockResponse = {
        data: {
          id: 1,
          rumuz: 'testuser',
          text: 'Hello world',
          sentimentLabel: 'pozitif',
          sentimentScore: 0.95,
        },
      };
      mockAxiosInstance.post.mockResolvedValue(mockResponse);

      const result = await api.sendMessage('testuser', 'Hello world');

      expect(mockAxiosInstance.post).toHaveBeenCalledWith('/api/messages', {
        rumuz: 'testuser',
        text: 'Hello world',
      });
      expect(result).toEqual(mockResponse);
    });
  });

  describe('getMessages', () => {
    it('calls GET /api/messages with default pagination', async () => {
      const mockResponse = { data: [] };
      mockAxiosInstance.get.mockResolvedValue(mockResponse);

      const result = await api.getMessages();

      expect(mockAxiosInstance.get).toHaveBeenCalledWith('/api/messages', {
        params: { page: 1, pageSize: 50 },
      });
      expect(result).toEqual(mockResponse);
    });

    it('calls GET /api/messages with custom pagination', async () => {
      const mockResponse = { data: [] };
      mockAxiosInstance.get.mockResolvedValue(mockResponse);

      const result = await api.getMessages(2, 20);

      expect(mockAxiosInstance.get).toHaveBeenCalledWith('/api/messages', {
        params: { page: 2, pageSize: 20 },
      });
      expect(result).toEqual(mockResponse);
    });
  });

  describe('getUserById', () => {
    it('calls GET /api/users/:id', async () => {
      const mockResponse = { data: { id: 1, rumuz: 'testuser' } };
      mockAxiosInstance.get.mockResolvedValue(mockResponse);

      const result = await api.getUserById(1);

      expect(mockAxiosInstance.get).toHaveBeenCalledWith('/api/users/1');
      expect(result).toEqual(mockResponse);
    });
  });

  describe('getAllUsers', () => {
    it('calls GET /api/users', async () => {
      const mockResponse = { data: [] };
      mockAxiosInstance.get.mockResolvedValue(mockResponse);

      const result = await api.getAllUsers();

      expect(mockAxiosInstance.get).toHaveBeenCalledWith('/api/users');
      expect(result).toEqual(mockResponse);
    });
  });

  describe('healthCheck', () => {
    it('calls GET /api/health', async () => {
      const mockResponse = { data: { status: 'healthy' } };
      mockAxiosInstance.get.mockResolvedValue(mockResponse);

      const result = await api.healthCheck();

      expect(mockAxiosInstance.get).toHaveBeenCalledWith('/api/health');
      expect(result).toEqual(mockResponse);
    });
  });
});
