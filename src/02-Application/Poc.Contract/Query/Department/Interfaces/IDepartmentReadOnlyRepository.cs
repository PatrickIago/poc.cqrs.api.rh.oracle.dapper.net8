using Poc.Contract.Query.Departament.ViewModels;
namespace Poc.Contract.Query.Departament.Interfaces;
public interface IDepartmentReadOnlyRepository
{
    Task<List<DepartmentQueryModel>> Get();
    Task<DepartmentQueryModel> Get(decimal id);
}
