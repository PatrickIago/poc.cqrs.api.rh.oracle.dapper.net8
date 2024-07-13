namespace Poc.Domain.Entities.JobHistory.Events;
public class JobHistoryDeletedEvent : JobHistoryBaseEvent
{
    public JobHistoryDeletedEvent(decimal employeeId, DateTime startDate, DateTime endDate, string jobId, decimal departmentId) : base(employeeId, startDate, endDate, jobId, departmentId)
    {
    }
}
