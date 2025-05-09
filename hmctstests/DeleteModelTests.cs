using hmcts.Data;
using hmcts.Models;
using hmcts.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace hmctstests;

public class DeleteModelTests
{
    [Fact]
    public async Task OnGetAsync_ReturnsPage_WhenCaseExists()
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

        var pageModel = new DeleteModel(mockContext);

        // Act  
        var result = await pageModel.OnGetAsync(1);

        // Assert  
        Assert.NotNull(result);
        Assert.IsType<PageResult>(result);
        Assert.NotNull(pageModel.Case);
        Assert.Equal(1, pageModel.Case.Id);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsNotFound_WhenCaseDoesNotExist()
    {
        // Arrange  
        var mockOptions = new DbContextOptionsBuilder<HmctsContext>().UseInMemoryDatabase("hmctsContext").Options;
        var mockContext = new HmctsContext(mockOptions);

        var pageModel = new DeleteModel(mockContext);

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

        var pageModel = new DeleteModel(mockContext);

        // Act  
        var result = await pageModel.OnGetAsync(null);

        // Assert  
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnPostAsync_RedirectsToIndex_WhenCaseIsDeleted()
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

        var pageModel = new DeleteModel(mockContext);

        // Act
        var result = await pageModel.OnPostAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<RedirectToPageResult>(result);
        var redirectResult = result as RedirectToPageResult;
        Assert.Equal("./Index", redirectResult?.PageName);
        Assert.Empty(mockContext.Case);
    }

    [Fact]
    public async Task OnPostAsync_ReturnsRedirectToPage_WhenCaseDoesNotExist()
    {
        // Arrange
        var mockOptions = new DbContextOptionsBuilder<HmctsContext>().UseInMemoryDatabase("hmctsContext").Options;
        var mockContext = new HmctsContext(mockOptions);

        var pageModel = new DeleteModel(mockContext);

        // Act
        var result = await pageModel.OnPostAsync(99);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<RedirectToPageResult>(result);
    }

    [Fact]
    public async Task OnPostAsync_ReturnsNotFound_WhenIdIsNull()
    {
        // Arrange
        var mockOptions = new DbContextOptionsBuilder<HmctsContext>().UseInMemoryDatabase("hmctsContext").Options;
        var mockContext = new HmctsContext(mockOptions);

        var pageModel = new DeleteModel(mockContext);

        // Act
        var result = await pageModel.OnPostAsync(null);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }
}
