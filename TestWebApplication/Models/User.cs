namespace TestWebApplication.Models;

public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }

    public User(Guid id, string? name, string? email, string? address)
    {
        Id = id;
        Name = name;
        Email = email;
        Address = address;
    }

    public User()
    {
        Id = Guid.NewGuid();
    }
}
