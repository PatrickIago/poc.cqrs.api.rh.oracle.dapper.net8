using Poc.Domain.Entities.Department.Events;
public class DepartmentUpdatedEvent : DepartmentBaseEvent
{
    public DepartmentUpdatedEvent(int id, string departmentName, int? managerId, string location)
        : base(id, departmentName, managerId, location)
    {
    }
}