using Poc.Contract.Query.Region.ViewModels;

namespace Poc.Contract.Query.Region.Interfaces;

public interface IRegionsReadOnlyRepository
{
    Task<RegionQueryModel> Get(decimal id);
    Task<List<RegionQueryModel>> Get();
}
