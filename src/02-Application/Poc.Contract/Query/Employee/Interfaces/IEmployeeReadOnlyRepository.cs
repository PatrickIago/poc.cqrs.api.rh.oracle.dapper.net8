using Poc.Contract.Query.Employee.ViewModels;
namespace Poc.Contract.Query.Employee.Interfaces;
public interface IEmployeeReadOnlyRepository
{
    Task<EmployeeQueryModel> Get(decimal id);
    Task<List<EmployeeQueryModel>> Get();
}
