using dotnetWebApiCoreCBA.Controllers;
using dotnetWebApiCoreCBA.Models.DTOs.Todo;
using dotnetWebApiCoreCBA.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using dotnetWebApiCoreCBA.Common;

namespace dotnetWebApiCoreCBA.Tests;

public class TodoControllerTests
{
    private readonly Mock<ITodoService> _mockTodoService;
    private readonly TodoController _controller;

    public TodoControllerTests()
    {
        _mockTodoService = new Mock<ITodoService>();
        _controller = new TodoController(_mockTodoService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfTodos()
    {
        // Arrange
        var mockTodos = new List<TodoResponse>
        {
            new TodoResponse { Id = 1, Title = "Test Todo 1", IsCompleted = false },
            new TodoResponse { Id = 2, Title = "Test Todo 2", IsCompleted = true }
        };
        _mockTodoService.Setup(service => service.GetAllAsync())
            .ReturnsAsync(mockTodos);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse<IEnumerable<TodoResponse>>>(okResult.Value);
        Assert.True(apiResponse.Success);
        Assert.Equal(2, apiResponse.Data?.Count());
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenIdDoesNotExist()
    {
        // Arrange
        int invalidId = 999;
        _mockTodoService.Setup(service => service.GetByIdAsync(invalidId))
            .ReturnsAsync((TodoResponse?)null);

        // Act
        var result = await _controller.GetById(invalidId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse<TodoResponse>>(notFoundResult.Value);
        Assert.False(apiResponse.Success);
        Assert.Equal("Todo not found", apiResponse.Message);
        Assert.Equal("NOT_FOUND", apiResponse.ErrorCode);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenIdExists()
    {
        // Arrange
        int validId = 1;
        var mockTodo = new TodoResponse { Id = validId, Title = "Valid", IsCompleted = false };
        _mockTodoService.Setup(service => service.GetByIdAsync(validId))
            .ReturnsAsync(mockTodo);

        // Act
        var result = await _controller.GetById(validId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse<TodoResponse>>(okResult.Value);
        Assert.True(apiResponse.Success);
        Assert.Equal(validId, apiResponse.Data?.Id);
    }
}
