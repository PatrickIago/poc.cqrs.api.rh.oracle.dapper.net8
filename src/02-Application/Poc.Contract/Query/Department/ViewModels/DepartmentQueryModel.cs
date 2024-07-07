namespace Poc.Contract.Query.Departament.ViewModels;
public class DepartmentQueryModel
{
    public decimal Id { get; set; }
    public string Name { get; set; }
    public decimal ManagerId { get; set; }
    public decimal Location { get; set; }

    public DepartmentQueryModel()
    {
    }

    public DepartmentQueryModel(decimal id, string name, decimal managerId, decimal location)
    {
        Id = id;
        Name = name;
        ManagerId = managerId;
        Location = location;
    }
}
