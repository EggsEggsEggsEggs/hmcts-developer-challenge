using hmcts.Data;
using hmcts.Models;
using hmcts.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace hmctstests;

public class CreateModelTests
{
    [Fact]
    public void OnGet_ReturnsPage()
    {
        // Arrange
        var mockOptions = new DbContextOptionsBuilder<HmctsContext>().UseInMemoryDatabase("hmctsContext").Options;
        var mockContext = new HmctsContext(mockOptions);
        var pageModel = new CreateModel(mockContext);

        // Act
        var result = pageModel.OnGet();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PageResult>(result);
    }

    [Fact]
    public async Task OnPostAsync_RedirectsToIndex_WhenModelStateIsValid()
    {
        // Arrange
        var mockOptions = new DbContextOptionsBuilder<HmctsContext>().UseInMemoryDatabase("hmctsContext").Options;
        var mockContext = new HmctsContext(mockOptions);
        var pageModel = new CreateModel(mockContext)
        {
            Case = new Case
            {
                Id = 1,
                CaseNumber = "C123",
                Title = "Case 1",
                Description = "Description 1",
                Status = "Open",
                CreatedDate = DateTime.Now
            }
        };

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<RedirectToPageResult>(result);
        var redirectResult = result as RedirectToPageResult;
        Assert.Equal("./Index", redirectResult?.PageName);
        Assert.Single(mockContext.Case);
    }

    [Fact]
    public async Task OnPostAsync_ReturnsPage_WhenModelStateIsInvalid()
    {
        // Arrange
        var mockOptions = new DbContextOptionsBuilder<HmctsContext>().UseInMemoryDatabase("hmctsContext").Options;
        var mockContext = new HmctsContext(mockOptions);
        var pageModel = new CreateModel(mockContext);
        pageModel.ModelState.AddModelError("Case.Title", "The Title field is required.");

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PageResult>(result);
    }
}
