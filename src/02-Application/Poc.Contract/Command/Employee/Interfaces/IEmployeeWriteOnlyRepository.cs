using Poc.Contract.Query.Employee.ViewModels;
using Poc.Domain.Entities.Employee;
namespace Poc.Contract.Command.Employee.Interfaces;
public interface IEmployeeWriteOnlyRepository
{
    Task<EmployeeQueryModel> Create(EmployeeEntity employee);
    Task<bool> Update(EmployeeEntity employee);
    Task<bool> Delete(decimal employeeId);
    Task<EmployeeEntity> Get(decimal id);
}
