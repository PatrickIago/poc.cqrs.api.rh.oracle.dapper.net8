namespace Poc.Contract.Query.Job.ViewModels;
public class JobQueryModel
{
    public string JobId { get; set; }
    public string JobTitle { get; set; }
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }

    public JobQueryModel(string jobId, string jobTitle, decimal minSalary, decimal maxSalary)
    {
        JobId = jobId;
        JobTitle = jobTitle;
        MinSalary = minSalary;
        MaxSalary = maxSalary;
    }
}
