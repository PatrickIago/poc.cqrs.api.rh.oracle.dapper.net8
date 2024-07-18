namespace Poc.Domain.Entities.Job.Events;
public class JobDeletedEvent : JobBaseEvent
{
    public JobDeletedEvent(string jobId, string jobTitle, decimal minSalary, decimal maxSalary)
        : base(jobId, jobTitle, minSalary, maxSalary)
    {
    }
}
