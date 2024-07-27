using Poc.Contract.Query.Region.ViewModels;
using Poc.Domain.Entities.Region;
namespace Poc.Contract.Command.Region.Interfaces;

public interface IRegionWriteOnlyRepository
{
    Task<RegionQueryModel> Create(RegionEntity region);
    Task<bool> Update(RegionEntity region);
    Task<bool> Delete(decimal regionId, string user_id_delete); 
    Task<RegionEntity> Get(decimal id);
}