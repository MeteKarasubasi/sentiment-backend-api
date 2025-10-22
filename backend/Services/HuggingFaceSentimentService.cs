using backend.Models;
using System.Text.Json;
using System.Text;

namespace backend.Services;

public class HuggingFaceSentimentService : ISentimentService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiToken;
    private readonly string _modelId;
    private readonly ILogger<HuggingFaceSentimentService> _logger;

    public HuggingFaceSentimentService(
        HttpClient httpClient, 
        IConfiguration configuration, 
        ILogger<HuggingFaceSentimentService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        
        // Hugging Face API Token (optional for public models)
        _apiToken = configuration["HuggingFace:ApiToken"] ?? "";
        
        // Model: Multilingual sentiment (supports Turkish)
        _modelId = configuration["HuggingFace:ModelId"] 
            ?? "cardiffnlp/twitter-xlm-roberta-base-sentiment";
        
        // Set headers
        if (!string.IsNullOrEmpty(_apiToken))
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiToken}");
        }
        
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
    }

    public async Task<SentimentResult?> AnalyzeAsync(string text)
    {
        try
        {
            var apiUrl = $"https://api-inference.huggingface.co/models/{_modelId}";
            
            var requestPayload = new { inputs = text };
            var jsonContent = JsonSerializer.Serialize(requestPayload);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _logger.LogInformation("Sending request to Hugging Face API: {Model}", _modelId);

            var response = await _httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogWarning("Hugging Face API error: {StatusCode}, {Error}", 
                    response.StatusCode, errorContent);
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Received response from Hugging Face API");

            // Parse response: [[{"label": "positive", "score": 0.99}]]
            var results = JsonSerializer.Deserialize<List<List<HfSentimentResult>>>(responseContent);
            
            if (results == null || results.Count == 0 || results[0].Count == 0)
            {
                _logger.LogWarning("Empty response from Hugging Face API");
                return null;
            }

            var topResult = results[0][0];
            
            // Map labels to Turkish
            var labelMap = new Dictionary<string, string>
            {
                { "positive", "pozitif" },
                { "negative", "negatif" },
                { "neutral", "nötr" },
                { "POSITIVE", "pozitif" },
                { "NEGATIVE", "negatif" },
                { "NEUTRAL", "nötr" }
            };

            var label = labelMap.ContainsKey(topResult.Label) 
                ? labelMap[topResult.Label] 
                : topResult.Label.ToLower();

            return new SentimentResult
            {
                Label = label,
                Score = Math.Round(topResult.Score, 4)
            };
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "Hugging Face API request timed out");
            return null;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request to Hugging Face API failed");
            return null;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Failed to parse Hugging Face API response");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during sentiment analysis");
            return null;
        }
    }

    private class HfSentimentResult
    {
        public string Label { get; set; } = string.Empty;
        public double Score { get; set; }
    }
}
