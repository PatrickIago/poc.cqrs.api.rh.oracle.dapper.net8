using Poc.Domain.Entities.Departament;
namespace Poc.Oracle.DTO;
public class DepartmentSqlDTO
{
    public string Name { get; set; }
    public decimal Id { get; set; }
    public decimal ManagerId { get; set; }
    public decimal Location { get; set; }

    public DepartmentEntity MapToResult()
    {
        return new DepartmentEntity(Id,Name,ManagerId,Location);
    }
}