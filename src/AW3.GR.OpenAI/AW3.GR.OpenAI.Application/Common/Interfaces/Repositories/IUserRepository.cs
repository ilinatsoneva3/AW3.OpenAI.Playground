using AW3.GR.OpenAI.Domain.Users;

namespace AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);

    Task AddUser(User user);
}
