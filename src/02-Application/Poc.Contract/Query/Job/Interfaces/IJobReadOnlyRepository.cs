using Poc.Contract.Query.Job.ViewModels;

namespace Poc.Contract.Query.Job.Interfaces;
public interface IJobReadOnlyRepository
{
    Task<List<JobQueryModel>> Get();
    Task<JobQueryModel> Get(string id);
}
