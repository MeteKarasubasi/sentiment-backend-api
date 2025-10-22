using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using backend.Models;
using backend.Services;

namespace backend.Tests;

public class SentimentServiceTests
{
    private Mock<HttpMessageHandler> CreateMockHttpMessageHandler(HttpStatusCode statusCode, string responseContent)
    {
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(responseContent)
            });

        return mockHandler;
    }

    [Fact]
    public async Task AnalyzeAsync_WithSuccessfulResponse_ReturnsSentimentResult()
    {
        // Arrange
        var responseContent = JsonSerializer.Serialize(new
        {
            data = new[]
            {
                new { label = "pozitif", score = 0.95 }
            }
        });

        var mockHandler = CreateMockHttpMessageHandler(HttpStatusCode.OK, responseContent);
        var httpClient = new HttpClient(mockHandler.Object);

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "AiService:Url", "http://test-ai-service.com" }
            }!)
            .Build();

        var logger = new Mock<ILogger<SentimentService>>();
        var service = new SentimentService(httpClient, configuration, logger.Object);

        // Act
        var result = await service.AnalyzeAsync("This is a great day!");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("pozitif", result.Label);
        Assert.Equal(0.95, result.Score);
    }

    [Fact]
    public async Task AnalyzeAsync_WithNonSuccessStatusCode_ReturnsNull()
    {
        // Arrange
        var mockHandler = CreateMockHttpMessageHandler(HttpStatusCode.InternalServerError, "");
        var httpClient = new HttpClient(mockHandler.Object);

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "AiService:Url", "http://test-ai-service.com" }
            }!)
            .Build();

        var logger = new Mock<ILogger<SentimentService>>();
        var service = new SentimentService(httpClient, configuration, logger.Object);

        // Act
        var result = await service.AnalyzeAsync("Test message");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AnalyzeAsync_WithEmptyResponse_ReturnsNull()
    {
        // Arrange
        var responseContent = JsonSerializer.Serialize(new { data = Array.Empty<object>() });
        var mockHandler = CreateMockHttpMessageHandler(HttpStatusCode.OK, responseContent);
        var httpClient = new HttpClient(mockHandler.Object);

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "AiService:Url", "http://test-ai-service.com" }
            }!)
            .Build();

        var logger = new Mock<ILogger<SentimentService>>();
        var service = new SentimentService(httpClient, configuration, logger.Object);

        // Act
        var result = await service.AnalyzeAsync("Test message");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AnalyzeAsync_WithInvalidJson_ReturnsNull()
    {
        // Arrange
        var mockHandler = CreateMockHttpMessageHandler(HttpStatusCode.OK, "invalid json");
        var httpClient = new HttpClient(mockHandler.Object);

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "AiService:Url", "http://test-ai-service.com" }
            }!)
            .Build();

        var logger = new Mock<ILogger<SentimentService>>();
        var service = new SentimentService(httpClient, configuration, logger.Object);

        // Act
        var result = await service.AnalyzeAsync("Test message");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AnalyzeAsync_WithTimeout_ReturnsNull()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ThrowsAsync(new TaskCanceledException());

        var httpClient = new HttpClient(mockHandler.Object);

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "AiService:Url", "http://test-ai-service.com" }
            }!)
            .Build();

        var logger = new Mock<ILogger<SentimentService>>();
        var service = new SentimentService(httpClient, configuration, logger.Object);

        // Act
        var result = await service.AnalyzeAsync("Test message");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AnalyzeAsync_WithHttpRequestException_ReturnsNull()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ThrowsAsync(new HttpRequestException());

        var httpClient = new HttpClient(mockHandler.Object);

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "AiService:Url", "http://test-ai-service.com" }
            }!)
            .Build();

        var logger = new Mock<ILogger<SentimentService>>();
        var service = new SentimentService(httpClient, configuration, logger.Object);

        // Act
        var result = await service.AnalyzeAsync("Test message");

        // Assert
        Assert.Null(result);
    }
}
