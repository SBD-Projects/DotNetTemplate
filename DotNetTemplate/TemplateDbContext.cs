using DotNetTemplate.Configurations;
using DotNetTemplate.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetTemplate;

public class TemplateDbContext : DbContext
{
    public TemplateDbContext(DbContextOptions<TemplateDbContext> options)
        : base(options) { }
    
    public DbSet<User> Users => Set<User>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfig());
    }
}