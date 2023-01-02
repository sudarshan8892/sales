using SalesAssignment.Models;

namespace WebApplication1.Models
{
    public class SalesResponse
    {
        public List<Sale> Sales { get; set; } = new List<Sale>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
