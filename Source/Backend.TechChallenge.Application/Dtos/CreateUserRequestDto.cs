namespace Backend.TechChallenge.Application.Dtos;

public class CreateUserRequestDto
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public UserTypeDto? UserType { get; set; }

    public decimal? Money { get; set; }
}