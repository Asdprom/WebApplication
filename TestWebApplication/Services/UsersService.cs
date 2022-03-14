using TestWebApplication.Data;
using TestWebApplication.Models;

namespace TestWebApplication.Services;

public class UsersService : IUsersService
{
    private readonly TestDbContext _dbContext;

    public UsersService(TestDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<User> GetUsers()
    {
        return _dbContext.Users.ToArray();
    }

    public void AddUser(User user)
    {
        if (_dbContext.Users.Any(x => x.Name == user.Name))
            throw new ArgumentException("User with such name already exists.");
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public void UpdateUser(User user)
    {
        var existing = _dbContext.Users.FirstOrDefault(x => x.Id == user.Id);
        if (existing == null)
            return;
        existing.Address = user.Address;
        existing.Email = user.Email;
        existing.Name = user.Name;
        _dbContext.SaveChanges();
    }

    public void DeleteUser(Guid id)
    {
        var existing = _dbContext.Users.FirstOrDefault(x => x.Id == id);
        if (existing == null)
            return;
        _dbContext.Users.Remove(existing);
        _dbContext.SaveChanges();
    }
}
