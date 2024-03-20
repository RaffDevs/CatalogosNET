using APICatalogo.App.Controllers;
using APICatalogo.App.Domain.Category.Models.DTO;
using APICatalogoTest.Setup;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogoTest.Category.Tests.Controller;

public class GetCategoryTest : IClassFixture<CategoryControllerSetup>
{
    private readonly CategoryController _controller;

    public GetCategoryTest(CategoryControllerSetup controller)
    {
        _controller = new CategoryController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task GetCategoryById_Return_OkResult()
    {
        //Arrange
        var categoryId = 2;
        
        //Act
        var data = await _controller.GetCategory(categoryId);

        //Assert
        // (xunit)
        // var okResult = Assert.IsType<Ok>(data.Result);
        // Assert.Equal(200, okResult.StatusCode);

        data.Result.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetCategoryById_Return_NotFound()
    {
        //Arrange
        var categoryId = 1478500003;
        
        //Act
        var data = await _controller.GetCategory(categoryId);
        
        //Assert
        data.Result.Should().BeOfType<NotFoundObjectResult>()
            .Which.StatusCode.Should().Be(404);

    }

    [Fact]
    public async Task GetCategoryId_Return_BadRequest()
    {
        //Arrange
        var categoryId = -1;
        
        //Act
        var data = await _controller.GetCategory(categoryId);
        
        //Assert
        data.Result.Should().BeOfType<BadRequestObjectResult>()
            .Which.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task GetAllCategory_Return_ListOfCategoryDTO()
    {
        //Act
        var data = await _controller.GetCategories();
        
        //Assert
        data.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<IEnumerable<CategoryDTO>>()
            .And.NotBeNull();

    }
}