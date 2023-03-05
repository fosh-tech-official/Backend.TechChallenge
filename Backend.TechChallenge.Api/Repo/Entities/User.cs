using Backend.TechChallenge.Api.Common.Models;

namespace Backend.TechChallenge.Api.Repo.Entities
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public decimal Money { get; set; }
    }
}
