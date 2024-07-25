using DotNetTemplate.Extensions;

namespace DotNetTemplate.Entities;

public class User(string firstName, string lastName, string email, string password, string phoneNumber)
{
    public int Id { get; init; }
    public string FirstName { get; private set; } = string.IsNullOrWhiteSpace(firstName) && firstName.Length > 50 
        ? throw new ArgumentException("Warehouse name cannot be empty", nameof(firstName)) 
        : firstName;
    
    public string LastName { get; private set; } = string.IsNullOrWhiteSpace(lastName) && lastName.Length > 50 
        ? throw new ArgumentException("Warehouse name cannot be empty", nameof(lastName)) 
        : lastName;
    
    public string Email { get; private set; } = string.IsNullOrWhiteSpace(email) && !email.IsValidEmail() && email.Length > 100
        ? throw new ArgumentException("Warehouse name cannot be empty", nameof(email)) 
        : email;
    
    public string Password { get; init; } = string.IsNullOrWhiteSpace(password) && !password.IsValidPassword()
        ? throw new ArgumentException("Warehouse name cannot be empty", nameof(password)) 
        : password;
    
    public string PhoneNumber { get; private set; } = string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Length > 15
        ? throw new ArgumentException("Warehouse name cannot be empty", nameof(phoneNumber)) 
        : phoneNumber;

    public void Update(string firstName, string lastName, string email, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}