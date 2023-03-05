namespace Backend.TechChallenge.Application.Dtos.Enums;

public enum CreateUserResponseResult
{
    Created,
    Duplicated,
    NotValid,
    UnhandledError
}