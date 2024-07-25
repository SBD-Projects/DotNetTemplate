using DotNetTemplate.Extensions;

namespace DotNetTemplate.Entities;

public class User(string name, string email)
{
    public int Id { get; init; }
    public string Name { get; private set; } = string.IsNullOrWhiteSpace(name) && name.Length > 50 
        ? throw new ArgumentException("Warehouse name cannot be empty", nameof(name)) 
        : name;
    
    public string Email { get; private set; } = string.IsNullOrWhiteSpace(email) && !email.IsValidEmail() && email.Length > 100
        ? throw new ArgumentException("Warehouse name cannot be empty", nameof(email)) 
        : email;
}