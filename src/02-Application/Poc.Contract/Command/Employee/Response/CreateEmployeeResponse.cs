namespace Poc.Contract.Command.Employee.Response;
public class CreateEmployeeResponse
{
    public CreateEmployeeResponse(decimal id) => EmployeeId = id;
    public decimal EmployeeId { get; }
}
