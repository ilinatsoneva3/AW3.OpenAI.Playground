using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Domain.Users;

namespace AW3.GR.OpenAI.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly GROpenAIDbContext _dbContext;

    public UserRepository(GROpenAIDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddUser(User user)
    {
        _dbContext.Add(user);
        _dbContext.SaveChanges();
    }

    public User? GetUserByEmail(string email)
        => _dbContext.Users.SingleOrDefault(u => u.Email == email);
}
