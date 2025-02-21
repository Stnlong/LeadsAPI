

namespace LeadsAPI.Models
{
    public class Leads
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Zip { get; set; }
        public bool CanContact { get; set; }
        public string? Email { get; set; }
        public required string Details { get; set; }
    }

}