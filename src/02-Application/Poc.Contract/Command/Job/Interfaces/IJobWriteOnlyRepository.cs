using Poc.Contract.Query.Job.ViewModels;
using Poc.Domain.Entities.Job;

namespace Poc.Contract.Command.Job.Interfaces;
public interface IJobWriteOnlyRepository
{
    Task<JobQueryModel> Create(JobEntity job);
    Task<bool> Update(JobEntity job);
    Task<bool> Delete(string id);
    Task<JobEntity> Get(string id);
}
