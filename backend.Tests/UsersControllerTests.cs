using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using backend.Controllers;
using backend.Data;
using backend.Models;

namespace backend.Tests;

public class UsersControllerTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateUser_WithValidRumuz_ReturnsCreatedResult()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new Mock<ILogger<UsersController>>();
        var controller = new UsersController(context, logger.Object);
        var request = new CreateUserRequest { Rumuz = "testuser" };

        // Act
        var result = await controller.CreateUser(request);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var response = Assert.IsType<UserResponse>(createdResult.Value);
        Assert.Equal("testuser", response.Rumuz);
        Assert.True(response.Id > 0);
    }

    [Fact]
    public async Task CreateUser_WithEmptyRumuz_ReturnsBadRequest()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new Mock<ILogger<UsersController>>();
        var controller = new UsersController(context, logger.Object);
        var request = new CreateUserRequest { Rumuz = "" };

        // Act
        var result = await controller.CreateUser(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.NotNull(badRequestResult.Value);
    }

    [Fact]
    public async Task CreateUser_WithShortRumuz_ReturnsBadRequest()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new Mock<ILogger<UsersController>>();
        var controller = new UsersController(context, logger.Object);
        var request = new CreateUserRequest { Rumuz = "ab" };

        // Act
        var result = await controller.CreateUser(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.NotNull(badRequestResult.Value);
    }

    [Fact]
    public async Task CreateUser_WithLongRumuz_ReturnsBadRequest()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new Mock<ILogger<UsersController>>();
        var controller = new UsersController(context, logger.Object);
        var request = new CreateUserRequest { Rumuz = "thisrumuziswaytoolongforvalidation" };

        // Act
        var result = await controller.CreateUser(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.NotNull(badRequestResult.Value);
    }

    [Fact]
    public async Task CreateUser_WithDuplicateRumuz_ReturnsConflict()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new Mock<ILogger<UsersController>>();
        var controller = new UsersController(context, logger.Object);
        
        // Create first user
        var user = new User { Rumuz = "existinguser", CreatedAt = DateTime.UtcNow };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var request = new CreateUserRequest { Rumuz = "existinguser" };

        // Act
        var result = await controller.CreateUser(request);

        // Assert
        var conflictResult = Assert.IsType<ConflictObjectResult>(result.Result);
        Assert.NotNull(conflictResult.Value);
    }

    [Fact]
    public async Task GetUsers_ReturnsAllUsers()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new Mock<ILogger<UsersController>>();
        var controller = new UsersController(context, logger.Object);

        // Add test users
        context.Users.AddRange(
            new User { Rumuz = "user1", CreatedAt = DateTime.UtcNow },
            new User { Rumuz = "user2", CreatedAt = DateTime.UtcNow.AddMinutes(1) }
        );
        await context.SaveChangesAsync();

        // Act
        var result = await controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var users = Assert.IsAssignableFrom<IEnumerable<UserResponse>>(okResult.Value);
        Assert.Equal(2, users.Count());
    }

    [Fact]
    public async Task GetUser_WithValidId_ReturnsUser()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new Mock<ILogger<UsersController>>();
        var controller = new UsersController(context, logger.Object);

        var user = new User { Rumuz = "testuser", CreatedAt = DateTime.UtcNow };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        var result = await controller.GetUser(user.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<UserResponse>(okResult.Value);
        Assert.Equal("testuser", response.Rumuz);
    }

    [Fact]
    public async Task GetUser_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new Mock<ILogger<UsersController>>();
        var controller = new UsersController(context, logger.Object);

        // Act
        var result = await controller.GetUser(999);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }
}
