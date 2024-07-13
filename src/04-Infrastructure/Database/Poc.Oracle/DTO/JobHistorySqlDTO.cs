using Poc.Domain.Entities.JobHistory;
namespace Poc.Oracle.DTO;
public class JobHistorySqlDTO
{
    public decimal EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string JobId { get; set; }
    public decimal DepartmentId { get; set; }

    public JobHistoryEntity MapToResult()
    {
        return new JobHistoryEntity(EmployeeId,StartDate,EndDate,JobId,DepartmentId);
    }
}
