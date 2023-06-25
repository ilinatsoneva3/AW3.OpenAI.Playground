using AW3.GR.OpenAI.Domain.Authors;

namespace AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;

public interface IAuthorRepository
{
    Task<Author?> GetAuthorByFirstAndLastNameAsync(string firstName, string lastName);

    Task<Author?> GetAuthorByLastNameAsync(string lastName);

    Task AddAuthorAsync(Author author);
}
