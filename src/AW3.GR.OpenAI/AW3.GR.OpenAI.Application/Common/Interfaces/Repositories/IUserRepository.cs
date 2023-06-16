using AW3.GR.OpenAI.Domain.UserAggregate;

namespace AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    User? GetUserByEmail(string email);

    void AddUser(User user);
}
