using hmcts.Data;
using hmcts.Models;
using hmcts.Pages;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace hmctstests;

public class IndexTests
{
    [Fact]
    public async Task OnGetAsync_PopulatesCaseList()
    {
        // Arrange  
        var mockCases = new List<Case>
        {
            new() { Id = 1, CaseNumber = "C123", Title = "Case 1", Description = "Description 1", Status = "Open", CreatedDate = DateTime.Now },
            new() { Id = 2, CaseNumber = "C124", Title = "Case 2", Description = "Description 2", Status = "Closed", CreatedDate = DateTime.Now }
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Case>>();
        mockSet.As<IQueryable<Case>>().Setup(m => m.Provider).Returns(mockCases.Provider);
        mockSet.As<IQueryable<Case>>().Setup(m => m.Expression).Returns(mockCases.Expression);
        mockSet.As<IQueryable<Case>>().Setup(m => m.ElementType).Returns(mockCases.ElementType);
        mockSet.As<IQueryable<Case>>().Setup(m => m.GetEnumerator()).Returns(mockCases.GetEnumerator());

        var mockOptions = new DbContextOptionsBuilder<HmctsContext>().UseInMemoryDatabase("hmctsContext").Options;

        // Create a real DbContext instance using the in-memory database
        var mockContext = new HmctsContext(mockOptions);

        // Seed the in-memory database with mock data
        mockContext.Case.AddRange(mockCases);
        mockContext.SaveChanges();

        // Pass the real DbContext instance to the IndexModel
        var pageModel = new IndexModel(mockContext);

        // Act  
        await pageModel.OnGetAsync();

        // Assert  
        Assert.NotNull(pageModel.Case);
        Assert.Equal(2, pageModel.Case.Count);
        Assert.Equal("C123", pageModel.Case[0].CaseNumber);
        Assert.Equal("C124", pageModel.Case[1].CaseNumber);

    }
}
