using Poc.Contract.Query.JobHistory.ViewModels;
using Poc.Domain.Entities.JobHistory;
namespace Poc.Contract.Command.JobHistory.Interfaces;
public interface IJobHistoryWriteOnlyRepository
{
    Task<JobHistoryQueryModel> Create(JobHistoryEntity jobHistory);
    Task<bool> Update(JobHistoryEntity jobHistory);
    Task<bool> Delete(decimal employeeId);
    Task<JobHistoryEntity> Get(decimal id);
    Task<List<JobHistoryQueryModel>> GetByEmployeeId(decimal employeeId);
}
