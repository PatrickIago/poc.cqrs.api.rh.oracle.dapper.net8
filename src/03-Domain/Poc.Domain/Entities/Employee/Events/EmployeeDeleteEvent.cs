﻿using Poc.Domain.Entities.Departament;

namespace Poc.Domain.Entities.Employee.Events;
public class EmployeeDeletedEvent : EmployeeBaseEvent
{
    public EmployeeDeletedEvent(int id, string name, string email, string phoneNumber, DateTime hireDate, int jobId, decimal salary, int? managerId, DepartmentEntity department)
        : base(id, name, email, phoneNumber, hireDate, jobId, salary, managerId, department)
    {
    }
}
