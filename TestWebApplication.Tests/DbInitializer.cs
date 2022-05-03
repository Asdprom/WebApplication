using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TestWebApplication.Data;

namespace TestWebApplication.Tests;

public static class DbInitializer
{
    public static void Initialize(DbContextOptions<TestDbContext> options)
    {
        try
        {
            var dbContext = new TestDbContext(options);
            dbContext.Database.EnsureCreated();
            dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            TestContext.Out.WriteLine(e.Message);
        }
    }
}
