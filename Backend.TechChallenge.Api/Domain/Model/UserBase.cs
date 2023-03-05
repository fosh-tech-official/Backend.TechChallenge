using Backend.TechChallenge.Api.Common.Models;

namespace Backend.TechChallenge.Api.Domain.Model
{
    public abstract class UserBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public abstract UserType Type { get; }
        public decimal Money { get; set; }
    }

    public class NormalUser : UserBase
    {
        public override UserType Type => UserType.Normal;
    }

    public class SuperUser : UserBase
    {
        public override UserType Type => UserType.SuperUser;
    }

    public class PremiumUser : UserBase
    {
        public override UserType Type => UserType.Premium;
    }
}
