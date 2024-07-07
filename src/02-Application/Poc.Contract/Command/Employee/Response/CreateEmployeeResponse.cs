namespace Poc.Contract.Command.Employee.Response;
public class CreateEmployeeResponse // Resposta de um comando de criação.
{
    public CreateEmployeeResponse(int id) => Id = id;
    public int Id { get; }
}