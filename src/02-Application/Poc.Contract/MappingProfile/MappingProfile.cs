using AutoMapper;
using Poc.Contract.Query.Departament.ViewModels;
using Poc.Contract.Query.Employee.ViewModels;
using Poc.Contract.Query.JobHistory.ViewModels;
using Poc.Contract.Query.Region.ViewModels;
using Poc.Domain.Entities.Employee;
using Poc.Domain.Entities.JobHistory;
using Poc.Domain.Entities.Region;

namespace Poc.Contract.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Oracle
        CreateMap<RegionEntity, RegionQueryModel>();
        CreateMap<RegionQueryModel, RegionEntity>();
        CreateMap<EmployeeEntity, EmployeeQueryModel>();
        CreateMap<EmployeeQueryModel, EmployeeEntity>();
        CreateMap<DepartmentEntity, DepartmentQueryModel>();
        CreateMap<DepartmentQueryModel, DepartmentEntity>();
        CreateMap<JobHistoryQueryModel,JobHistoryEntity>();
        CreateMap<JobHistoryEntity, JobHistoryQueryModel>();


        #endregion
    }
}
