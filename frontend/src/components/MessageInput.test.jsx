import { describe, it, expect, vi } from 'vitest';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import MessageInput from './MessageInput';

describe('MessageInput', () => {
  it('renders textarea and send button', () => {
    const mockOnSendMessage = vi.fn();
    render(<MessageInput onSendMessage={mockOnSendMessage} />);
    
    expect(screen.getByPlaceholderText('Mesajınızı yazın...')).toBeInTheDocument();
    expect(screen.getByRole('button', { type: 'submit' })).toBeInTheDocument();
  });

  it('updates textarea value when user types', async () => {
    const mockOnSendMessage = vi.fn();
    const user = userEvent.setup();
    render(<MessageInput onSendMessage={mockOnSendMessage} />);
    
    const textarea = screen.getByPlaceholderText('Mesajınızı yazın...');
    await user.type(textarea, 'Hello world');
    
    expect(textarea).toHaveValue('Hello world');
  });

  it('calls onSendMessage when send button is clicked', async () => {
    const mockOnSendMessage = vi.fn().mockResolvedValue();
    const user = userEvent.setup();
    render(<MessageInput onSendMessage={mockOnSendMessage} />);
    
    const textarea = screen.getByPlaceholderText('Mesajınızı yazın...');
    const sendButton = screen.getByRole('button', { type: 'submit' });
    
    await user.type(textarea, 'Test message');
    await user.click(sendButton);
    
    await waitFor(() => {
      expect(mockOnSendMessage).toHaveBeenCalledWith('Test message');
    });
  });

  it('clears textarea after successful message send', async () => {
    const mockOnSendMessage = vi.fn().mockResolvedValue();
    const user = userEvent.setup();
    render(<MessageInput onSendMessage={mockOnSendMessage} />);
    
    const textarea = screen.getByPlaceholderText('Mesajınızı yazın...');
    
    await user.type(textarea, 'Test message');
    await user.click(screen.getByRole('button', { type: 'submit' }));
    
    await waitFor(() => {
      expect(textarea).toHaveValue('');
    });
  });

  it('disables send button when textarea is empty', () => {
    const mockOnSendMessage = vi.fn();
    render(<MessageInput onSendMessage={mockOnSendMessage} />);
    
    const sendButton = screen.getByRole('button', { type: 'submit' });
    expect(sendButton).toBeDisabled();
  });

  it('disables send button when textarea contains only whitespace', async () => {
    const mockOnSendMessage = vi.fn();
    const user = userEvent.setup();
    render(<MessageInput onSendMessage={mockOnSendMessage} />);
    
    const textarea = screen.getByPlaceholderText('Mesajınızı yazın...');
    await user.type(textarea, '   ');
    
    const sendButton = screen.getByRole('button', { type: 'submit' });
    expect(sendButton).toBeDisabled();
  });

  it('disables textarea and button when disabled prop is true', () => {
    const mockOnSendMessage = vi.fn();
    render(<MessageInput onSendMessage={mockOnSendMessage} disabled={true} />);
    
    const textarea = screen.getByPlaceholderText('Mesajınızı yazın...');
    const sendButton = screen.getByRole('button', { type: 'submit' });
    
    expect(textarea).toBeDisabled();
    expect(sendButton).toBeDisabled();
  });

  it('shows loading state while sending message', async () => {
    const mockOnSendMessage = vi.fn(() => new Promise(resolve => setTimeout(resolve, 100)));
    const user = userEvent.setup();
    render(<MessageInput onSendMessage={mockOnSendMessage} />);
    
    const textarea = screen.getByPlaceholderText('Mesajınızı yazın...');
    await user.type(textarea, 'Test message');
    
    const sendButton = screen.getByRole('button', { type: 'submit' });
    await user.click(sendButton);
    
    // Check for loading emoji
    expect(sendButton).toHaveTextContent('⏳');
    expect(sendButton).toBeDisabled();
  });

  it('submits form when Enter key is pressed', async () => {
    const mockOnSendMessage = vi.fn().mockResolvedValue();
    const user = userEvent.setup();
    render(<MessageInput onSendMessage={mockOnSendMessage} />);
    
    const textarea = screen.getByPlaceholderText('Mesajınızı yazın...');
    await user.type(textarea, 'Test message{Enter}');
    
    await waitFor(() => {
      expect(mockOnSendMessage).toHaveBeenCalledWith('Test message');
    });
  });
});
