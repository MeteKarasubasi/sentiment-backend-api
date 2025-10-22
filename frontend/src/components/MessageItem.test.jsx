import { describe, it, expect } from 'vitest';
import { render, screen } from '@testing-library/react';
import MessageItem from './MessageItem';

describe('MessageItem', () => {
  const mockMessage = {
    id: 1,
    rumuz: 'testuser',
    text: 'This is a test message',
    sentimentLabel: 'pozitif',
    sentimentScore: 0.95,
    createdAt: '2024-01-15T10:30:00Z',
  };

  it('renders message text and rumuz', () => {
    render(<MessageItem message={mockMessage} />);
    
    expect(screen.getByText('testuser')).toBeInTheDocument();
    expect(screen.getByText('This is a test message')).toBeInTheDocument();
  });

  it('displays sentiment label when provided', () => {
    render(<MessageItem message={mockMessage} />);
    
    expect(screen.getByText('pozitif')).toBeInTheDocument();
  });

  it('displays sentiment score as percentage', () => {
    render(<MessageItem message={mockMessage} />);
    
    expect(screen.getByText('95%')).toBeInTheDocument();
  });

  it('applies correct CSS class for positive sentiment', () => {
    render(<MessageItem message={mockMessage} />);
    
    const badge = screen.getByText('pozitif').closest('.sentiment-badge');
    expect(badge).toHaveClass('positive');
  });

  it('applies correct CSS class for negative sentiment', () => {
    const negativeMessage = {
      ...mockMessage,
      sentimentLabel: 'negatif',
    };
    
    render(<MessageItem message={negativeMessage} />);
    
    const badge = screen.getByText('negatif').closest('.sentiment-badge');
    expect(badge).toHaveClass('negative');
  });

  it('applies correct CSS class for neutral sentiment', () => {
    const neutralMessage = {
      ...mockMessage,
      sentimentLabel: 'nötr',
    };
    
    render(<MessageItem message={neutralMessage} />);
    
    const badge = screen.getByText('nötr').closest('.sentiment-badge');
    expect(badge).toHaveClass('neutral');
  });

  it('does not display sentiment badge when sentimentLabel is null', () => {
    const messageWithoutSentiment = {
      ...mockMessage,
      sentimentLabel: null,
      sentimentScore: null,
    };
    
    render(<MessageItem message={messageWithoutSentiment} />);
    
    expect(screen.queryByText('pozitif')).not.toBeInTheDocument();
    expect(screen.queryByText('95%')).not.toBeInTheDocument();
  });
});
