using Microsoft.EntityFrameworkCore;
using SimpleTodoList.Models;

namespace SimpleTodoList.DataAccess;

public class TodoDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public TodoDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Todo> Todos => Set<Todo>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DbConnection"));
    }
}