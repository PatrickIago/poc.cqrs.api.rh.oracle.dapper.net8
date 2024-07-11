namespace Poc.Contract.Command.Departament.Response;
public class CreateDepartmentResponse
{
    public CreateDepartmentResponse(decimal id) => Id = id;
    public decimal Id { get; }
}
