using backend.Models;
using System.Text.Json;
using System.Text;

namespace backend.Services;

public class CustomSpaceSentimentService : ISentimentService
{
    private readonly HttpClient _httpClient;
    private readonly string _spaceUrl;
    private readonly ILogger<CustomSpaceSentimentService> _logger;

    public CustomSpaceSentimentService(
        HttpClient httpClient, 
        IConfiguration configuration, 
        ILogger<CustomSpaceSentimentService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        
        // Custom Hugging Face Space URL
        _spaceUrl = configuration["AiService:SpaceUrl"] 
            ?? "https://mete1923-emotion.hf.space";
        
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<SentimentResult?> AnalyzeAsync(string text)
    {
        try
        {
            var apiUrl = $"{_spaceUrl}/api/sentiment";
            
            var requestPayload = new { text = text };
            var jsonContent = JsonSerializer.Serialize(requestPayload);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _logger.LogInformation("Sending request to Custom Space: {Url}", apiUrl);

            var response = await _httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogWarning("Custom Space API error: {StatusCode}, {Error}", 
                    response.StatusCode, errorContent);
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Received response from Custom Space");

            // Parse response: {"label": "pozitif", "score": 0.99}
            var result = JsonSerializer.Deserialize<SentimentResult>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result == null)
            {
                _logger.LogWarning("Failed to parse Custom Space response");
                return null;
            }

            return result;
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "Custom Space API request timed out");
            return null;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request to Custom Space failed");
            return null;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Failed to parse Custom Space response");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during sentiment analysis");
            return null;
        }
    }
}
