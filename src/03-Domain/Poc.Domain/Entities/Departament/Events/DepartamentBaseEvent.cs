using poc.core.api.net8.Events;
namespace Poc.Domain.Entities.Department.Events;
public abstract class DepartmentBaseEvent : Event
{
    protected DepartmentBaseEvent(int id, string departmentName, int? managerId, string location)
    {
        Id = id;
        DepartmentName = departmentName;
        ManagerId = managerId;
        Location = location;
    }

    public int Id { get; private set; }
    public string DepartmentName { get; private set; }
    public int? ManagerId { get; private set; }
    public string Location { get; private set; }
}