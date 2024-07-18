namespace Poc.Domain.Entities.Job.Events;
public class JobUpdatedEvent : JobBaseEvent
{
    public JobUpdatedEvent(string jobId, string jobTitle, decimal minSalary, decimal maxSalary)
        : base(jobId, jobTitle, minSalary, maxSalary)
    {
    }
}
