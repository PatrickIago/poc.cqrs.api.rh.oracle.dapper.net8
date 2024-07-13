namespace Poc.Domain.Entities.JobHistory.Events;
public class JobHistoryUpdatedEvent : JobHistoryBaseEvent
{
    public JobHistoryUpdatedEvent(decimal employeeId, DateTime startDate, DateTime endDate, string jobId, decimal departmentId) : base(employeeId, startDate, endDate, jobId, departmentId)
    {
    }
}
