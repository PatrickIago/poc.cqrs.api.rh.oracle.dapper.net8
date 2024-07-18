using Poc.Domain.Entities.Job;

namespace Poc.Oracle.DTO;
public class JobSqlDTO
{
    public string JobId { get; set; }
    public string JobTitle { get; set; }
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }

    public JobEntity MapToResult()
    {
        return new JobEntity(JobId, JobTitle, MinSalary, MaxSalary);
    }
}
