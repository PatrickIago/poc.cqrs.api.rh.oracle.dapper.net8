using poc.core.api.net8.Events;
namespace Poc.Domain.Entities.Job.Events;
public class JobBaseEvent : Event
{
    public JobBaseEvent(string jobId, string jobTitle, decimal minSalary, decimal maxSalary)
    {
        JobId = jobId;
        JobTitle = jobTitle;
        MinSalary = minSalary;
        MaxSalary = maxSalary;
    }

    public string JobId { get; private set; }
    public string JobTitle { get; private set; }
    public decimal MinSalary { get; private set; }
    public decimal MaxSalary { get; private set; }

}
