using Poc.Domain.Entities.Departament;

namespace Poc.Contract.Query.Employee.ViewModels;
public class EmployeeQueryModel
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public DateTime HireDate { get; private set; }
    public int JobId { get; private set; }
    public int? ManagerId { get; private set; }
    public DepartmentEntity Department { get; private set; }

    public EmployeeQueryModel()
    {
    }

    public EmployeeQueryModel(int id, string name, string email, string phoneNumber, DateTime hireDate, int jobId, int managerID,DepartmentEntity departament)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        HireDate = hireDate;
        JobId = jobId;
        ManagerId = managerID;
        Department = departament;
    }
}
