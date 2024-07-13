using Poc.Contract.Query.JobHistory.ViewModels;
namespace Poc.Contract.Query.JobHistory.Interfaces;
public interface IJobHistoryReadOnlyRepository
{
    Task<List<JobHistoryQueryModel>> Get();
    Task<JobHistoryQueryModel> Get(decimal id);
}

