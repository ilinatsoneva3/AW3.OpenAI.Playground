using System.Linq.Expressions;
using AW3.GR.OpenAI.Domain.Authors;

namespace AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;

public interface IAuthorRepository
{
    Task<Author?> FirstOrDefaultAsync(Expression<Func<Author, bool>>? predicate = null, CancellationToken cancellationToken = default);

    Task AddAuthorAsync(Author author, CancellationToken cancellationToken = default);

    Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
