using Poc.Domain.Entities.Departament;
namespace Poc.Oracle.DTO;
public class DepartmentSqlDTO
{
    public string Name { get; set; }
    public int Id { get; set; }
    public int? ManagerId { get; private set; }
    public string Location { get; set; }

    public DepartamentEntity MapToResult()
    {
        return new DepartamentEntity(Id,Name,ManagerId,Location);
    }
}