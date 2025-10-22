using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using backend.Controllers;
using backend.Data;
using backend.Models;
using backend.Services;

namespace backend.Tests;

public class MessagesControllerTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateMessage_WithValidData_ReturnsCreatedResult()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var user = new User { Rumuz = "testuser", CreatedAt = DateTime.UtcNow };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var mockSentimentService = new Mock<ISentimentService>();
        mockSentimentService
            .Setup(s => s.AnalyzeAsync(It.IsAny<string>()))
            .ReturnsAsync(new SentimentResult { Label = "pozitif", Score = 0.95 });

        var logger = new Mock<ILogger<MessagesController>>();
        var controller = new MessagesController(context, mockSentimentService.Object, logger.Object);

        var request = new CreateMessageRequest
        {
            Rumuz = "testuser",
            Text = "This is a great day!"
        };

        // Act
        var result = await controller.CreateMessage(request);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var response = Assert.IsType<MessageResponse>(createdResult.Value);
        Assert.Equal("testuser", response.Rumuz);
        Assert.Equal("This is a great day!", response.Text);
        Assert.Equal("pozitif", response.SentimentLabel);
        Assert.Equal(0.95, response.SentimentScore);
    }

    [Fact]
    public async Task CreateMessage_WithEmptyRumuz_ReturnsBadRequest()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var mockSentimentService = new Mock<ISentimentService>();
        var logger = new Mock<ILogger<MessagesController>>();
        var controller = new MessagesController(context, mockSentimentService.Object, logger.Object);

        var request = new CreateMessageRequest
        {
            Rumuz = "",
            Text = "Test message"
        };

        // Act
        var result = await controller.CreateMessage(request);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateMessage_WithEmptyText_ReturnsBadRequest()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var mockSentimentService = new Mock<ISentimentService>();
        var logger = new Mock<ILogger<MessagesController>>();
        var controller = new MessagesController(context, mockSentimentService.Object, logger.Object);

        var request = new CreateMessageRequest
        {
            Rumuz = "testuser",
            Text = ""
        };

        // Act
        var result = await controller.CreateMessage(request);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateMessage_WithNonExistentUser_ReturnsBadRequest()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var mockSentimentService = new Mock<ISentimentService>();
        var logger = new Mock<ILogger<MessagesController>>();
        var controller = new MessagesController(context, mockSentimentService.Object, logger.Object);

        var request = new CreateMessageRequest
        {
            Rumuz = "nonexistentuser",
            Text = "Test message"
        };

        // Act
        var result = await controller.CreateMessage(request);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateMessage_WhenAIServiceFails_Returns207WithNullSentiment()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var user = new User { Rumuz = "testuser", CreatedAt = DateTime.UtcNow };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var mockSentimentService = new Mock<ISentimentService>();
        mockSentimentService
            .Setup(s => s.AnalyzeAsync(It.IsAny<string>()))
            .ReturnsAsync((SentimentResult?)null);

        var logger = new Mock<ILogger<MessagesController>>();
        var controller = new MessagesController(context, mockSentimentService.Object, logger.Object);

        var request = new CreateMessageRequest
        {
            Rumuz = "testuser",
            Text = "Test message"
        };

        // Act
        var result = await controller.CreateMessage(request);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(207, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task CreateMessage_WhenAIServiceThrowsException_Returns207WithNullSentiment()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var user = new User { Rumuz = "testuser", CreatedAt = DateTime.UtcNow };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var mockSentimentService = new Mock<ISentimentService>();
        mockSentimentService
            .Setup(s => s.AnalyzeAsync(It.IsAny<string>()))
            .ThrowsAsync(new Exception("AI Service error"));

        var logger = new Mock<ILogger<MessagesController>>();
        var controller = new MessagesController(context, mockSentimentService.Object, logger.Object);

        var request = new CreateMessageRequest
        {
            Rumuz = "testuser",
            Text = "Test message"
        };

        // Act
        var result = await controller.CreateMessage(request);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(207, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task GetMessages_ReturnsAllMessagesInOrder()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var user = new User { Rumuz = "testuser", CreatedAt = DateTime.UtcNow };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Add messages with different timestamps
        context.Messages.AddRange(
            new Message
            {
                Rumuz = "testuser",
                Text = "First message",
                SentimentLabel = "pozitif",
                SentimentScore = 0.9,
                CreatedAt = DateTime.UtcNow
            },
            new Message
            {
                Rumuz = "testuser",
                Text = "Second message",
                SentimentLabel = "nötr",
                SentimentScore = 0.5,
                CreatedAt = DateTime.UtcNow.AddMinutes(1)
            }
        );
        await context.SaveChangesAsync();

        var mockSentimentService = new Mock<ISentimentService>();
        var logger = new Mock<ILogger<MessagesController>>();
        var controller = new MessagesController(context, mockSentimentService.Object, logger.Object);

        // Act
        var result = await controller.GetMessages();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<PagedMessagesResponse>(okResult.Value);
        Assert.Equal(2, response.Messages.Count);
        Assert.Equal("First message", response.Messages[0].Text);
        Assert.Equal("Second message", response.Messages[1].Text);
    }

    [Fact]
    public async Task GetMessages_WithPagination_ReturnsCorrectPage()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var user = new User { Rumuz = "testuser", CreatedAt = DateTime.UtcNow };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Add 5 messages
        for (int i = 1; i <= 5; i++)
        {
            context.Messages.Add(new Message
            {
                Rumuz = "testuser",
                Text = $"Message {i}",
                SentimentLabel = "nötr",
                SentimentScore = 0.5,
                CreatedAt = DateTime.UtcNow.AddMinutes(i)
            });
        }
        await context.SaveChangesAsync();

        var mockSentimentService = new Mock<ISentimentService>();
        var logger = new Mock<ILogger<MessagesController>>();
        var controller = new MessagesController(context, mockSentimentService.Object, logger.Object);

        // Act
        var result = await controller.GetMessages(page: 2, pageSize: 2);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<PagedMessagesResponse>(okResult.Value);
        Assert.Equal(2, response.Messages.Count);
        Assert.Equal(2, response.Page);
        Assert.Equal(5, response.TotalCount);
        Assert.Equal(3, response.TotalPages);
    }

    [Fact]
    public async Task GetMessage_WithValidId_ReturnsMessage()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var user = new User { Rumuz = "testuser", CreatedAt = DateTime.UtcNow };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var message = new Message
        {
            Rumuz = "testuser",
            Text = "Test message",
            SentimentLabel = "pozitif",
            SentimentScore = 0.9,
            CreatedAt = DateTime.UtcNow
        };
        context.Messages.Add(message);
        await context.SaveChangesAsync();

        var mockSentimentService = new Mock<ISentimentService>();
        var logger = new Mock<ILogger<MessagesController>>();
        var controller = new MessagesController(context, mockSentimentService.Object, logger.Object);

        // Act
        var result = await controller.GetMessage(message.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<MessageResponse>(okResult.Value);
        Assert.Equal("Test message", response.Text);
        Assert.Equal("pozitif", response.SentimentLabel);
    }

    [Fact]
    public async Task GetMessage_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var mockSentimentService = new Mock<ISentimentService>();
        var logger = new Mock<ILogger<MessagesController>>();
        var controller = new MessagesController(context, mockSentimentService.Object, logger.Object);

        // Act
        var result = await controller.GetMessage(999);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }
}
