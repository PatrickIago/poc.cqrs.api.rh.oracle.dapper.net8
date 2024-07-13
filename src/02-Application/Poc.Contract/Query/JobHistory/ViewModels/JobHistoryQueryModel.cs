namespace Poc.Contract.Query.JobHistory.ViewModels;
public class JobHistoryQueryModel
{
    public decimal EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string JobId { get; set; }
    public decimal DepartmentId { get; set; }

    public JobHistoryQueryModel(decimal employeeId, DateTime startDate, DateTime endDate, string jobId, decimal departmentId)
    {
        EmployeeId = employeeId;
        StartDate = startDate;
        EndDate = endDate;
        JobId = jobId;
        DepartmentId = departmentId;
    }
}
