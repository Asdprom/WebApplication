using System.Security.Cryptography;
using System.Text;
using TestWebApplication.Data;
using TestWebApplication.Models;

namespace TestWebApplication.Services;

public class AuthenticationService
{
    private readonly TestDbContext _dbContext;

    public AuthenticationService(IUsersService service, TestDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Guid Register(string userName, string password)
    {
        var person = _dbContext.Persons.FirstOrDefault(x => x.UserName == userName);
        if (person != null)
            return person.Id;

        var salt = RandomString(10);
        var newPerson = new Person()
                        {
                            Id = Guid.NewGuid(),
                            UserName = userName,
                            PasswordHash = Hash(password + salt),
                            Salt = salt,
                        };
        _dbContext.Persons.Add(newPerson);
        _dbContext.SaveChanges();
        return newPerson.Id;
    }

    public bool Login(string userName, string password)
    {
        var person = _dbContext.Persons.FirstOrDefault(x => x.UserName == userName);
        if (person == null)
            return false;
        return person.PasswordHash == Hash(password + person.Salt);
    }

    private string Hash(string password)
    {
        var algorithm = HashAlgorithm.Create("SHA256");
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        return Convert.ToBase64String(algorithm!.ComputeHash(passwordBytes));
    }

    public static string RandomString(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
                                    .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
