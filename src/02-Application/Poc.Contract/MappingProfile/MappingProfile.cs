using AutoMapper;
using Poc.Contract.Query.Region.ViewModels;
using Poc.Domain.Entities.Region;

namespace Poc.Contract.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Oracle
        CreateMap<RegionEntity, RegionQueryModel>();
        CreateMap<RegionQueryModel, RegionEntity>();
        #endregion
    }
}
