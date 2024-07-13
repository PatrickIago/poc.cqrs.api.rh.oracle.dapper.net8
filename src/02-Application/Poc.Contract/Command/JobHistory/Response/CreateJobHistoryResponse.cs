namespace Poc.Contract.Command.JobHistory.Response;
public class CreateJobHistoryResponse
{
    public CreateJobHistoryResponse(decimal id) => EmployeeId = id;
    public decimal EmployeeId { get; set; }
}
