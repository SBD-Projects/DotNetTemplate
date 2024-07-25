using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetTemplate.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;


namespace DotNetTemplate.Extensions;

public static class JwtExtensions
{
    public static string GenerateJwtToken(this User user)
    {
        var key = Encoding.ASCII.GetBytes(GenerateKey());
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    private static string GenerateKey(int size = 32)
    {
        using var rng = new RNGCryptoServiceProvider();
        var keyBytes = new byte[size];
        rng.GetBytes(keyBytes);
        return Convert.ToBase64String(keyBytes);
    }
}