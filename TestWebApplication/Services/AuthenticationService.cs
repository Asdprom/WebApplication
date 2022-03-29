using TestWebApplication.Data;
using TestWebApplication.Models;

namespace TestWebApplication.Services;

public class AuthenticationService
{
    private readonly TestDbContext _dbContext;

    public AuthenticationService(TestDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Guid Register(string userName, string password)
    {
        var person = _dbContext.Persons.FirstOrDefault(x => x.UserName == userName);
        if (person != null)
            return person.Id;

        var newPerson = new Person() { Id = Guid.NewGuid(), UserName = userName, Password = password };
        _dbContext.Persons.Add(newPerson);
        _dbContext.SaveChanges();
        return newPerson.Id;
    }

    public bool Login(string userName, string password)
    {
        var person = _dbContext.Persons.FirstOrDefault(x => x.UserName == userName);
        if (person == null)
            return false;
        return person.Password == password;
    }
}
