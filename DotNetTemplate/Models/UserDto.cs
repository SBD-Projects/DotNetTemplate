using DotNetTemplate.Entities;

namespace DotNetTemplate.Models;

public record UserDto(int Id, string Name, string Email)
{
    public UserDto(User user) : this(user.Id, user.Name, user.Email)
    {
    }
}

public record UserCreateRequest(string Email, string Name);