using AW3.GR.OpenAI.Application.Common.Persistence.Interfaces;
using AW3.GR.OpenAI.Domain.Entities;

namespace AW3.GR.OpenAI.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private List<User> _users = new();
    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
        => _users.SingleOrDefault(u => u.Email == email);
}
