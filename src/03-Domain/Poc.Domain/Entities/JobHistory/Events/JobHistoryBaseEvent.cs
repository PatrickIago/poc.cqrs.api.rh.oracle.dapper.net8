using poc.core.api.net8.Events;

namespace Poc.Domain.Entities.JobHistory.Events;
public class JobHistoryBaseEvent : Event
{
    public JobHistoryBaseEvent(decimal employeeId, DateTime startDate, DateTime endDate, string jobId, decimal departmentId)
    {
        EmployeeId = employeeId;
        StartDate = startDate;
        EndDate = endDate;
        JobId = jobId;
        DepartmentId = departmentId;
    }
    public decimal EmployeeId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string JobId { get; private set; }
    public decimal DepartmentId { get; private set; }
}
