using Poc.Contract.Query.Departament.ViewModels;
using Poc.Domain.Entities.Departament;

namespace Poc.Contract.Command.Departament.Interfaces;
public interface IDepartmentWriteOnlyRepository
{
    Task<DepartmentQueryModel> Create(DepartmentEntity departament);
    Task<bool> Update(DepartmentEntity departament);
    Task<bool> Delete(decimal id);
    Task<DepartmentEntity> Get(decimal id);
}
