namespace Backend.TechChallenge.Infrastructure.File.Models;

public class UserFile
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string UserType { get; set; } = string.Empty;

    public decimal Money { get; set; }
}