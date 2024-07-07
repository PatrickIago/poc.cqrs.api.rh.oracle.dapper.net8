using Poc.Contract.Query.Employee.ViewModels;
namespace Poc.Contract.Query.Employee.Interfaces;
public interface IEmployeeReadOnlyRepository
{
    Task<List<EmployeeQueryModel>> Get();
    Task<EmployeeQueryModel> Get(int id);
}
