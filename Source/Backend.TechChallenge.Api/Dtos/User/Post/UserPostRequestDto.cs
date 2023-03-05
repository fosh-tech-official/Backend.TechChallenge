using System.ComponentModel.DataAnnotations;
using Backend.TechChallenge.Api.Attributes;
using Backend.TechChallenge.Domain.Entities.Enums;

namespace Backend.TechChallenge.Api.Dtos.User.Post;

public class UserPostRequestDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public string Phone { get; set; }
    
    [Required]
    [EnumDataType(typeof(UserType))]
    public string UserType { get; set; }
    
    [Required]
    [Money]
    public string Money { get; set; }
}