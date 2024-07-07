using Poc.Domain.Entities.Department.Events;
public class DepartmentDeletedEvent : DepartmentBaseEvent
{
    public DepartmentDeletedEvent(decimal id, string departmentName, decimal managerId, decimal location)
        : base(id, departmentName, managerId, location)
    {
    }
}