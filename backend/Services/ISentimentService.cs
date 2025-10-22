using backend.Models;

namespace backend.Services;

public interface ISentimentService
{
    Task<SentimentResult?> AnalyzeAsync(string text);
}
