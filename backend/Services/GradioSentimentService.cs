using backend.Models;
using System.Diagnostics;
using System.Text.Json;

namespace backend.Services;

public class GradioSentimentService : ISentimentService
{
    private readonly string _pythonScriptPath;
    private readonly ILogger<GradioSentimentService> _logger;

    public GradioSentimentService(IConfiguration configuration, ILogger<GradioSentimentService> logger)
    {
        _logger = logger;
        
        // Python script path
        var aiServicePath = configuration["AiService:PythonScriptPath"] 
            ?? Path.Combine(Directory.GetCurrentDirectory(), "..", "ai-service", "sentiment_client.py");
        
        _pythonScriptPath = aiServicePath;
        
        if (!File.Exists(_pythonScriptPath))
        {
            _logger.LogWarning("Python script not found at: {Path}", _pythonScriptPath);
        }
    }

    public async Task<SentimentResult?> AnalyzeAsync(string text)
    {
        try
        {
            // Call Python script with Gradio Client
            var startInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"\"{_pythonScriptPath}\" \"{text.Replace("\"", "\\\"")}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = startInfo };
            process.Start();

            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                _logger.LogError("Python script failed: {Error}", error);
                return null;
            }

            // Parse JSON output
            var result = JsonSerializer.Deserialize<SentimentResult>(output, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to analyze sentiment");
            return null;
        }
    }
}
