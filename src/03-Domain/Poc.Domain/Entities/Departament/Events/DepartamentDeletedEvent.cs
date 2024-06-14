using Poc.Domain.Entities.Department.Events;
public class DepartmentDeletedEvent : DepartmentBaseEvent
{
    public DepartmentDeletedEvent(int id, string departmentName, int? managerId, string location)
        : base(id, departmentName, managerId, location)
    {
    }
}