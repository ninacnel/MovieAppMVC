using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApp.Controllers;
using MovieApp.Data.Services;
using MovieApp.Models;
using MovieApp.ViewModels;
using MovieAppData;
using System.Collections.Generic;
using Xunit;

public class HomeControllerTests
{
    [Fact]
    public void Index_ReturnsViewWithMovies()
    {
        // Arrange
        var movieServiceMock = new Mock<MovieService>();
        movieServiceMock.Setup(service => service.GetMovies())
            .Returns(new List<MovieViewModel>());

        var controller = new HomeController(movieServiceMock.Object);

        // Act
        var result = controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsAssignableFrom<List<Movie>>(viewResult.Model);
    }

    [Fact]
    public void Movie_ReturnsViewWithMovie()
    {
        // Arrange
        var movieServiceMock = new Mock<MovieService>();
        movieServiceMock.Setup(service => service.GetMovie(It.IsAny<int>()))
            .Returns(new MovieViewModel());

        var controller = new HomeController(movieServiceMock.Object);

        // Act
        var result = controller.Index(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsAssignableFrom<Movie>(viewResult.Model);
    }

    [Fact]
    public void AddMovie_ValidModelState_RedirectsToIndex()
    {
        // Arrange
        var movieServiceMock = new Mock<MovieService>();
        var controller = new HomeController(movieServiceMock.Object);
        var movieViewModel = new MovieViewModel { /* populate with valid data */ };

        // Act
        var result = controller.AddMovie(movieViewModel) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
    }

    [Fact]
    public void AddMovie_InvalidModelState_ReturnsView()
    {
        // Arrange
        var movieServiceMock = new Mock<MovieService>();
        var controller = new HomeController(movieServiceMock.Object);
        var movieViewModel = new MovieViewModel { /* populate with invalid data */ };
        controller.ModelState.AddModelError("propertyName", "error message");

        // Act
        var result = controller.AddMovie(movieViewModel);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(movieViewModel, viewResult.Model);
    }
}
