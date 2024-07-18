using poc.core.api.net8;
using poc.core.api.net8.api.net8.api.net8.Abstractions;
using Poc.Domain.Entities.Job.Events;

namespace Poc.Domain.Entities.Job;

public class JobEntity : BaseEOraclentity, IAggregateRoot
{
    public string JobId { get; set; }
    public string JobTitle { get; set; }
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }

    public JobEntity()
    {

    }
    public JobEntity(string jobTitle, decimal minSalary, decimal maxSalary)
    {
        JobTitle = jobTitle;
        MinSalary = minSalary;
        MaxSalary = maxSalary;
        AddDomainEvent(new JobCreatedEvent(JobId, JobTitle, MinSalary, MaxSalary));
    }

    public JobEntity(string jobId, string jobTitle, decimal minSalary, decimal maxSalary)
    {
        JobId = jobId;
        JobTitle = jobTitle;
        MinSalary = minSalary;
        MaxSalary = maxSalary;
        AddDomainEvent(new JobUpdatedEvent(JobId, JobTitle, MinSalary, MaxSalary));
    }

    public JobEntity(string jobId)
    {
        JobId = jobId;
        AddDomainEvent(new JobDeletedEvent(JobId, JobTitle, MinSalary, MaxSalary));
    }
}
