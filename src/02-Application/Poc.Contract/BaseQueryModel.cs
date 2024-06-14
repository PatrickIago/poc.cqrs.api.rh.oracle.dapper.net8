using poc.core.api.net8.Abstractions;

namespace Poc.Contract;

public abstract class BaseQueryModel : IQueryModel<Guid>
{
    public Guid Id { get; private init; } = Guid.NewGuid();
}