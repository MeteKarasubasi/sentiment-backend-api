using backend.Models;
using System.Text.Json;
using System.Text;

namespace backend.Services;

public class SentimentService : ISentimentService
{
    private readonly HttpClient _httpClient;
    private readonly string _aiServiceUrl;
    private readonly ILogger<SentimentService> _logger;

    public SentimentService(HttpClient httpClient, IConfiguration configuration, ILogger<SentimentService> logger)
    {
        _httpClient = httpClient;
        _aiServiceUrl = configuration["AiService:Url"] ?? throw new ArgumentNullException("AiService:Url configuration is missing");
        _logger = logger;
        
        // Set timeout to 5 seconds
        _httpClient.Timeout = TimeSpan.FromSeconds(5);
    }

    public async Task<SentimentResult?> AnalyzeAsync(string text)
    {
        try
        {
            // Prepare request payload for Gradio API
            var requestPayload = new
            {
                data = new[] { text }
            };

            var jsonContent = JsonSerializer.Serialize(requestPayload);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _logger.LogInformation("Sending sentiment analysis request to AI Service: {Url}", _aiServiceUrl);

            // Send POST request to AI Service
            var response = await _httpClient.PostAsync(_aiServiceUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("AI Service returned non-success status code: {StatusCode}", response.StatusCode);
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Received response from AI Service");

            // Parse Gradio response format
            var gradioResponse = JsonSerializer.Deserialize<GradioResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (gradioResponse?.Data == null || gradioResponse.Data.Length == 0)
            {
                _logger.LogWarning("AI Service returned empty or invalid response");
                return null;
            }

            var sentimentData = gradioResponse.Data[0];
            
            return new SentimentResult
            {
                Label = sentimentData.Label,
                Score = sentimentData.Score
            };
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "AI Service request timed out after 5 seconds");
            return null;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request to AI Service failed");
            return null;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Failed to parse AI Service response");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during sentiment analysis");
            return null;
        }
    }

    // Internal classes for deserializing Gradio response
    private class GradioResponse
    {
        public SentimentData[] Data { get; set; } = Array.Empty<SentimentData>();
    }

    private class SentimentData
    {
        public string Label { get; set; } = string.Empty;
        public double Score { get; set; }
    }
}
