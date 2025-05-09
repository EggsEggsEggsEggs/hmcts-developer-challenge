using hmcts.Data;
using hmcts.Models;
using hmcts.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace hmctstests;

public class EditModelTests
{
    [Fact]
    public async Task OnGetAsync_ReturnsPage_WhenCaseExists()
    {
        // Arrange
        var mockCases = new List<Case>
        {
            new() { Id = 2, CaseNumber = "C123", Title = "Case 1", Description = "Description 1", Status = "Open", CreatedDate = DateTime.Now }
        }.AsQueryable();

        var mockOptions = new DbContextOptionsBuilder<HmctsContext>().UseInMemoryDatabase("hmctsContext").Options;
        var mockContext = new HmctsContext(mockOptions);
        mockContext.Case.AddRange(mockCases);
        mockContext.SaveChanges();

        var pageModel = new EditModel(mockContext);

        // Act
        var result = await pageModel.OnGetAsync(2);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PageResult>(result);
        Assert.NotNull(pageModel.Case);
        Assert.Equal(2, pageModel.Case.Id);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenCaseDoesNotExist()
    {
        // Arrange
        var mockOptions = new DbContextOptionsBuilder<HmctsContext>().UseInMemoryDatabase("hmctsContext").Options;
        var mockContext = new HmctsContext(mockOptions);

        var pageModel = new EditModel(mockContext);

        // Act
        var result = await pageModel.OnGetAsync(99);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenIdIsNull()
    {
        // Arrange
        var mockOptions = new DbContextOptionsBuilder<HmctsContext>().UseInMemoryDatabase("hmctsContext").Options;
        var mockContext = new HmctsContext(mockOptions);

        var pageModel = new EditModel(mockContext);

        // Act
        var result = await pageModel.OnGetAsync(null);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnPostAsync_RedirectsToIndex_WhenUpdateIsSuccessful()
    {
        // Arrange
        var mockCases = new List<Case>
        {
            new() { Id = 1, CaseNumber = "C123", Title = "Case 1", Description = "Description 1", Status = "Open", CreatedDate = DateTime.Now }
        }.AsQueryable();

        var mockOptions = new DbContextOptionsBuilder<HmctsContext>().UseInMemoryDatabase("hmctsContext").Options;
        var mockContext = new HmctsContext(mockOptions);
        mockContext.Case.AddRange(mockCases);
        mockContext.SaveChanges();

        var pageModel = new EditModel(mockContext)
        {
            Case = new() { Id = 1, CaseNumber = "C123", Title = "Case 1", Description = "Description 1", Status = "Open", CreatedDate = DateTime.Now }
        };

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("./Index", ((RedirectToPageResult)result).PageName);
    }

    [Fact]
    public async Task OnPostAsync_ReturnsPage_WhenModelStateIsInvalid()
    {
        // Arrange
        var mockOptions = new DbContextOptionsBuilder<HmctsContext>().UseInMemoryDatabase("hmctsContext").Options;
        var mockContext = new HmctsContext(mockOptions);

        var pageModel = new EditModel(mockContext);
        pageModel.ModelState.AddModelError("Case.Title", "The Title field is required.");

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PageResult>(result);
    }

    [Fact]
    public async Task OnPostAsync_ReturnsNotFound_WhenCaseDoesNotExist()
    {
        // Arrange
        var mockOptions = new DbContextOptionsBuilder<HmctsContext>().UseInMemoryDatabase("hmctsContext").Options;
        var mockContext = new HmctsContext(mockOptions);

        var pageModel = new EditModel(mockContext)
        {
            Case = new Case { Id = 99, CaseNumber = "C999", Title = "Nonexistent Case", Description = "Nonexistent Description", Status = "Closed", CreatedDate = DateTime.Now }
        };

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }
}
