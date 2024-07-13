using poc.core.api.net8;
using poc.core.api.net8.api.net8.api.net8.Abstractions;
using Poc.Domain.Entities.JobHistory.Events;

namespace Poc.Domain.Entities.JobHistory;
public class JobHistoryEntity : BaseEOraclentity, IAggregateRoot
{
    public decimal EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string JobId { get; set; }
    public decimal DepartmentId { get; set; }

    public JobHistoryEntity()
    {
    }

    public JobHistoryEntity(decimal employeeId, DateTime startDate, DateTime endDate, string jobId, decimal departmentId)
    {
        EmployeeId = employeeId;
        StartDate = startDate;
        EndDate = endDate;
        JobId = jobId;
        DepartmentId = departmentId;
        AddDomainEvent(new JobHistoryCreatedEvent(EmployeeId, StartDate, EndDate, JobId, DepartmentId));
    }

    public JobHistoryEntity(decimal employeeId, DateTime startDate, string jobId, decimal departmentId)
    {
        EmployeeId = employeeId;
        StartDate = startDate;
        JobId = jobId;
        DepartmentId = departmentId;
        AddDomainEvent(new JobHistoryUpdatedEvent(EmployeeId, StartDate, EndDate, JobId, DepartmentId));
    }

    public JobHistoryEntity(decimal employeeId)
    {
        EmployeeId = employeeId;
        AddDomainEvent(new JobHistoryDeletedEvent(EmployeeId, StartDate, EndDate, JobId, DepartmentId));
    }

    public void SetEmployeeId(decimal employeeId)
    {
        if (EmployeeId == default)
        {
            EmployeeId = employeeId;
        }
        else
        {
            throw new InvalidOperationException("O ID só pode ser definido uma vez.");
        }
    }
}