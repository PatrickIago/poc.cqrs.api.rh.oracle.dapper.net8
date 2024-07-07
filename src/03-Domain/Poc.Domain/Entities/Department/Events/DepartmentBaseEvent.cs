using poc.core.api.net8.Events;
namespace Poc.Domain.Entities.Department.Events;
public abstract class DepartmentBaseEvent : Event
{
    protected DepartmentBaseEvent(decimal id, string departmentName, decimal managerId, decimal location)
    {
        Id = id;
        DepartmentName = departmentName;
        ManagerId = managerId;
        Location = location;
    }

    public decimal Id { get; private set; }
    public string DepartmentName { get; private set; }
    public decimal ManagerId { get; private set; }
    public decimal Location { get; private set; }
}