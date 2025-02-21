using LeadsAPI.Models;
using LeadsAPI.Services;
using Moq;

public class LeadsServiceTests
{
    private readonly LeadsService _leadsService;
    private readonly Mock<ILogger<LeadsService>> _mockLogger;

    public LeadsServiceTests()
    {
        _mockLogger = new Mock<ILogger<LeadsService>>();
        _leadsService = new LeadsService(_mockLogger.Object);
    }

    [Fact]
    public void GetLeads_ShouldReturnAllLeads()
    {
        // Act
        List<Leads>? leads = _leadsService.GetLeads();

        // Assert
        Assert.NotNull(leads);
        Assert.Equal(5, leads.Count); // Assuming the initial list has 5 leads
    }

    [Fact]
    public async Task AddLeadsAsync_ShouldAddNewLeads()
    {
        // Arrange
        List<Leads>? newLeads = new List<Leads>
        {
            new Leads
            {
                Id = 6,
                Name = "New Lead",
                Phone = "555-9999",
                Zip = "99999",
                CanContact = true,
                Email = "new.lead@example.com",
                Details = "Interested in new product"
            }
        };

        // Act
        await _leadsService.AddLeadsAsync(newLeads);

        // Assert
        List<Leads>? leads = _leadsService.GetLeads();
        Assert.Contains(leads, l => l.Id == 6);
    }

    [Fact]
    public async Task AddLeadsAsync_ShouldHandleEmptyList()
    {
        // Arrange
        int initialCount = _leadsService.GetLeads().Count;
        List<Leads>? emptyLeads = new List<Leads>();

        // Act
        await _leadsService.AddLeadsAsync(emptyLeads);
        List<Leads>? leads = _leadsService.GetLeads();

        // Assert
        Assert.Equal(initialCount, leads.Count); // No new leads should be added
    }

    [Fact]
    public async Task AddLeadsAsync_ShouldHandleNullList()
    {
        // Arrange
        int initialCount = _leadsService.GetLeads().Count;
        List<Leads>? nullLeads = null;

        // Act
        await _leadsService.AddLeadsAsync(nullLeads);
        List<Leads>? leads = _leadsService.GetLeads();

        // Assert
        Assert.Equal(initialCount, leads.Count); // No new leads should be added
    }
}
