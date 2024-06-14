namespace Poc.Domain.Entities.Department.Events;
public class DepartmentCreatedEvent : DepartmentBaseEvent
{
    public DepartmentCreatedEvent(int id, string departmentName, int? managerId, string location)
        : base(id, departmentName, managerId, location)
    {
    }
}