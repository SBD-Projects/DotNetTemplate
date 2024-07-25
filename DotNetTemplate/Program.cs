using DotNetTemplate.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DotNetTemplate;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<TemplateDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("TemplateDb"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        var userRoutes = app.MapGroup("/users");
        userRoutes.RegisterUserRoutes();
        
        //app.UseAuthorization();
        app.Run();
    }
}