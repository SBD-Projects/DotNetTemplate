using DotNetTemplate.Entities;
using DotNetTemplate.Extensions;
using DotNetTemplate.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetTemplate.Controllers;

public static class UserControllers
{
    public static void RegisterUserRoutes(this RouteGroupBuilder routes)
    {
        routes.MapGet("/", GetAllUsers).WithName("GetAllUsers").WithOpenApi();
        routes.MapGet("/{id}", GetUserById).WithName("GetUserById").WithOpenApi();
        routes.MapPost("/", CreateUser).WithName("CreateUser").WithOpenApi();
        routes.MapPost("/login", Login).WithName("UserLogin").WithOpenApi();
        routes.MapDelete("/{id}", DeleteUser).WithName("DeleteUser").WithOpenApi();
        routes.MapPut("/update", UpdateUser).WithName("UpdateUser").WithOpenApi();
    }

    private static async Task<IResult> GetAllUsers(TemplateDbContext db) =>
        TypedResults.Ok(await db.Users.Select(w => new UserDto(w)).ToArrayAsync());

    private static async Task<IResult> GetUserById(TemplateDbContext db, int id){
        var user = await db.Users.FindAsync(id);
        if (user == null) return TypedResults.NotFound();
        var userDto = new UserDto(user);
        return TypedResults.Ok(userDto);
    }

private static async Task<IResult> CreateUser(TemplateDbContext db, UserCreateRequest request)
    {
        db.Add(new User(request.FirstName, request.LastName, request.Email, request.Password.HashPassword(), request.PhoneNumber));
        await db.SaveChangesAsync();
        return TypedResults.Created();
    }

    private static async Task<IResult> Login(TemplateDbContext db, UserLoginRequest request)
    {
        var user = await db.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || user.Password != request.Password.HashPassword()) return TypedResults.Unauthorized();
        var token = user.GenerateJwtToken();
        return TypedResults.Ok(new { Token = token });
    }
    
    private static async Task<IResult> DeleteUser(TemplateDbContext db, int id)
    {
        var user = await db.Users.FindAsync(id);
        if (user == null) return TypedResults.NotFound();
        db.Remove(user);
        await db.SaveChangesAsync();
        return TypedResults.Ok();
    }

    private static async Task<IResult> UpdateUser(TemplateDbContext db, UpdateUserRequest request)
    {
        var user = await db.Users.FindAsync(request.Id);
        if (user == null) return TypedResults.NotFound();
        user.Update(request.FirstName, request.LastName, request.Email, request.PhoneNumber);
        await db.SaveChangesAsync();
        return TypedResults.Ok();
    }
}