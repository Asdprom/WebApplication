using TestWebApplication.Models;

namespace TestWebApplication.Services;

public class UsersService
{
    private static readonly List<User> Users = new()
                                         {
                                             new User(Guid.NewGuid(), "Kolya", "kolya@mail.ru", "Moscow"),
                                             new User(Guid.NewGuid(), "Petya", "petya@mail.ru", "Moscow"),
                                             new User(Guid.NewGuid(), "Anya", "anya@mail.ru", "Moscow"),
                                         };
    public IEnumerable<User> GetUsers()
    {
        return Users;
    }

    public void AddUser(User user)
    {
        if (Users.Any(x => x.Name == user.Name))
            throw new ArgumentException("User with such name already exists.");
        Users.Add(user);
    }

    public void UpdateUser(User user)
    {
        var existing = Users.FirstOrDefault(x => x.Id == user.Id);
        if (existing == null)
            return;
        existing.Address = user.Address;
        existing.Email = user.Email;
        existing.Name = user.Name;
    }

    public void DeleteUser(Guid id)
    {
        var existing = Users.FirstOrDefault(x => x.Id == id);
        if (existing == null)
            return;
        Users.Remove(existing);
    }
}
