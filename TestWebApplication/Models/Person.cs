namespace TestWebApplication.Models;

public class Person
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string PasswordHash { get; set; }

    public string Salt { get; set; }
}
