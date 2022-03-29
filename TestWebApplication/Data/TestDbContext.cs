using Microsoft.EntityFrameworkCore;
using TestWebApplication.Models;

namespace TestWebApplication.Data;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Person> Persons { get; set; } = null!;
}
