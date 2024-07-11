namespace Poc.Contract.Query.Employee.ViewModels;
public class EmployeeQueryModel
{
    public decimal EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime HireDate { get; set; }
    public string JobId { get; set; }
    public decimal Salary { get; set; }
    public decimal CommissionPct { get; set; }
    public int ManagerId { get; set; }
    public int DepartmentId { get; set; }

    public EmployeeQueryModel(decimal employeeId, string firstName, string lastName, string email, string phone, DateTime hireDate, string jobId, decimal salary, decimal commissionPct, int managerId, int departmentId)
    {
        EmployeeId = employeeId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        HireDate = hireDate;
        JobId = jobId;
        Salary = salary;
        CommissionPct = commissionPct;
        ManagerId = managerId;
        DepartmentId = departmentId;
    }
}
