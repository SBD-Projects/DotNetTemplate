using DotNetTemplate.Entities;

namespace DotNetTemplate.Models;

public record UserDto(int Id, string FirstName, string LastName, string Email, string PhoneNumber)
{
    public UserDto(User user) : this(user.Id, user.FirstName, user.LastName, user.Email, user.PhoneNumber)
    {
    }
}

public record UserCreateRequest(string Email, string FirstName, string LastName, string Password, string PhoneNumber);

public record UserLoginRequest(string Email, string Password);

public record UpdateUserRequest(int Id, string Email, string FirstName, string LastName, string PhoneNumber);