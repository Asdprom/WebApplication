using TestWebApplication.Models;

namespace TestWebApplication.Services;

public interface IUsersService
{
    public IEnumerable<User> GetUsers();

    public void AddUser(User user);

    public void UpdateUser(User user);

    public void DeleteUser(Guid id);
}
