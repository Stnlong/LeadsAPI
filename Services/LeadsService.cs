using LeadsAPI.Models;

namespace LeadsAPI.Services
{
    public class LeadsService
    {
        private static readonly List<Leads> leadsList = new List<Leads>
        {
            new Leads { Id = 1, Name = "John Doe", Phone = "555-1234", Zip = "12345", CanContact = true, Email = "john.doe@example.com", Details = "Interested in product A" },
            new Leads { Id = 2, Name = "Jane Smith", Phone = "555-5678", Zip = "67890", CanContact = false, Email = "jane.smith@example.com", Details = "Requested a demo for product B" },
            new Leads { Id = 3, Name = "Bob Johnson", Phone = "555-8765", Zip = "54321", CanContact = true, Email = "", Details = "Looking for more information on product C" },
            new Leads { Id = 4, Name = "Alice Brown", Phone = "555-4321", Zip = "98765", CanContact = true, Email = "alice.brown@example.com", Details = "Interested in bulk purchase of product D" },
            new Leads { Id = 5, Name = "Charlie Davis", Phone = "555-6789", Zip = "11223", CanContact = false, Email = "charlie.davis@example.com", Details = "Needs a quote for product E" }
        };

        private readonly ILogger<LeadsService> _logger;

        public LeadsService(ILogger<LeadsService> logger)
        {
            _logger = logger;
        }

        public List<Leads> GetLeads()
        {
            return leadsList;
        }

        public async Task AddLeadsAsync(List<Leads> newLeads)
        {
            if (newLeads == null || newLeads.Count == 0)
            {
                _logger.LogWarning("Attempted to add an empty or null list of leads.");
                return;
            }

            try
            {
                await Task.Run(() =>
                {
                    lock (leadsList)
                    {
                        leadsList.AddRange(newLeads);
                    }
                });
                _logger.LogInformation("Successfully added new leads.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding new leads.");
                throw;
            }
        }
    }
}
