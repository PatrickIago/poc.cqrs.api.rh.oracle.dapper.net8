namespace Poc.Domain.Entities.Job.Events;
public class JobCreatedEvent : JobBaseEvent
{
    public JobCreatedEvent(string jobId, string jobTitle, decimal minSalary, decimal maxSalary)
        : base(jobId, jobTitle, minSalary, maxSalary)
    {
    }
}
