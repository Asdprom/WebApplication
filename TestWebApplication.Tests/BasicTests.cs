using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TestWebApplication.Data;
using TestWebApplication.Models;
using TestWebApplication.Services;

namespace TestWebApplication.Tests;

[TestFixture]
public class BasicTests
{
    const string ConnectionString = "Server=localhost;Port=5432;Database=TEST_testDatabase;User Id=postgres;Password=password;";
    private readonly DbContextOptions<TestDbContext> _options;

    public BasicTests()
    {
        _options = new DbContextOptionsBuilder<TestDbContext>()
                      .UseNpgsql(ConnectionString)
                      .Options;
    }

    [OneTimeSetUp]
    public void Initialize()
    {
        DbInitializer.Initialize(_options);
    }

    [TearDown]
    public void TearDown()
    {
        using var dbContext = new TestDbContext(_options);
        dbContext.RemoveRange(dbContext.Users.AsQueryable());
        dbContext.SaveChanges();
    }

    [Test]
    public void SimpleGetUsersTest()
    {
        using var dbContext = new TestDbContext(_options);
        var sut = new UsersService(dbContext);
        var expectedUser = new User(Guid.NewGuid(), "test1", "test2", "test3");

        sut.AddUser(expectedUser);

        var users = sut.GetUsers();
        var user = users.Single();
        Assert.AreEqual(expectedUser.Id, user.Id);
        Assert.AreEqual(expectedUser.Name, user.Name);
        Assert.AreEqual(expectedUser.Email, user.Email);
        Assert.AreEqual(expectedUser.Address, user.Address);
    }

    [Test]
    public void UserWithNullsGetUsersTest()
    {
        using var dbContext = new TestDbContext(_options);
        var sut = new UsersService(dbContext);
        var expectedUser = new User(Guid.NewGuid(), null, null, null);

        sut.AddUser(expectedUser);

        var users = sut.GetUsers();
        var user = users.Single();
        Assert.AreEqual(expectedUser.Id, user.Id);
        Assert.AreEqual(expectedUser.Name, user.Name);
        Assert.AreEqual(expectedUser.Email, user.Email);
        Assert.AreEqual(expectedUser.Address, user.Address);
    }
}
