namespace Poc.Domain.Entities.JobHistory.Events;
public class JobHistoryCreatedEvent : JobHistoryBaseEvent
{
    public JobHistoryCreatedEvent(decimal employeeId, DateTime startDate, DateTime endDate, string jobId, decimal departmentId) 
        : base(employeeId, startDate, endDate, jobId, departmentId)
    {
    }
}
