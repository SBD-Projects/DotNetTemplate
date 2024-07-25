using DotNetTemplate.Entities;
using DotNetTemplate.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetTemplate.Controllers;

public static class UserControllers
{
    public static void RegisterUserRoutes(this RouteGroupBuilder routes)
    {
        routes.MapGet("/", GetAllUsers).WithName("GetAllUsers").WithOpenApi();
        routes.MapPost("/", CreateUser).WithName("CreateUser").WithOpenApi();
    }
    
    private static async Task<IResult> GetAllUsers(TemplateDbContext db) =>
        TypedResults.Ok(await db.Users.Select(w => new UserDto(w)).ToArrayAsync());
    
    private static async Task<IResult> CreateUser(TemplateDbContext db, UserCreateRequest request)
    {
        db.Add(new User(request.Name, request.Email));
        await db.SaveChangesAsync();
        return TypedResults.Created();
    }
}