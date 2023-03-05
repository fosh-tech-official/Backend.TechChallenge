using System.Collections.Generic;
using Backend.TechChallenge.Api.Common.Models;

namespace Backend.TechChallenge.Api.Controllers.Entities
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType Type { get; set; }
        public decimal Money { get; set; }
    }

    public class CreateUserResult
    {
        public bool IsSuccess { get; set; }
    }
}
