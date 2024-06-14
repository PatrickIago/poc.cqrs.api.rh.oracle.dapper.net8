using poc.core.api.net8;
using poc.core.api.net8.api.net8.api.net8.Abstractions;
using Poc.Domain.Entities.Departament;
using Poc.Domain.Entities.Employee.Events;

namespace Poc.Domain.Entities.Employee;

/// <summary>
/// Representa a entidade de um funcionário.
/// </summary>
public class EmployeeEntity : BaseEOraclentity, IAggregateRoot
{
    /// <summary>
    /// Obtém ou define o ID do funcionário.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Obtém ou define o nome do funcionário.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Obtém ou define o e-mail do funcionário.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Obtém ou define o número de telefone do funcionário.
    /// </summary>
    public string PhoneNumber { get; private set; }

    /// <summary>
    /// Obtém ou define a data de contratação do funcionário.
    /// </summary>
    public DateTime HireDate { get; private set; }

    /// <summary>
    /// Obtém ou define o ID do cargo do funcionário.
    /// </summary>
    public int JobId { get; private set; }

    /// <summary>
    /// Obtém ou define o salário do funcionário.
    /// </summary>
    public decimal Salary { get; private set; }

    /// <summary>
    /// Obtém ou define o ID do gerente do funcionário.
    /// </summary>
    public int? ManagerId { get; private set; }

    /// <summary>
    /// Obtém ou define o departamento ao qual o funcionário está associado.
    /// </summary>
    public DepartamentEntity Department { get; private set; }

    /// <summary>
    /// Construtor padrão da classe EmployeeEntity.
    /// </summary>
    public EmployeeEntity()
    {
    }

    /// <summary>
    /// Construtor para criar um novo funcionário.
    /// </summary>
    /// <param name="name">Nome do funcionário.</param>
    /// <param name="email">E-mail do funcionário.</param>
    /// <param name="phoneNumber">Número de telefone do funcionário.</param>
    /// <param name="hireDate">Data de contratação do funcionário.</param>
    /// <param name="jobId">ID do cargo do funcionário.</param>
    /// <param name="salary">Salário do funcionário.</param>
    /// <param name="managerId">ID do gerente do funcionário.</param>
    /// <param name="department">Departamento ao qual o funcionário está associado.</param>
    public EmployeeEntity(string name, string email, string phoneNumber, DateTime hireDate, int jobId, decimal salary, int? managerId, DepartamentEntity department)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        HireDate = hireDate;
        JobId = jobId;
        Salary = salary;
        ManagerId = managerId;
        Department = department;

        AddDomainEvent(new EmployeeCreatedEvent(Id,Name, Email, PhoneNumber, HireDate, JobId, Salary, ManagerId, Department));
    }

    /// <summary>
    /// Atualiza os detalhes do funcionário.
    /// </summary>
    /// <param name="name">Nome do funcionário.</param>
    /// <param name="email">E-mail do funcionário.</param>
    /// <param name="phoneNumber">Número de telefone do funcionário.</param>
    /// <param name="hireDate">Data de contratação do funcionário.</param>
    /// <param name="jobId">ID do cargo do funcionário.</param>
    /// <param name="salary">Salário do funcionário.</param>
    /// <param name="managerId">ID do gerente do funcionário.</param>
    /// <param name="department">Departamento ao qual o funcionário está associado.</param>
    public EmployeeEntity(int id, string name, string email, string phoneNumber, DateTime hireDate, int jobId, decimal salary, int? managerId, DepartamentEntity department)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        HireDate = hireDate;
        JobId = jobId;
        Salary = salary;
        ManagerId = managerId;
        Department = department;

        AddDomainEvent(new EmployeeUpdatedEvent(Id, Name, Email, PhoneNumber, HireDate, JobId, Salary, ManagerId, Department));
    }

    /// <summary>
    /// Exclui o funcionário.
    /// </summary>
    public EmployeeEntity(int id)
    {
        AddDomainEvent(new EmployeeDeletedEvent(Id, Name, Email, PhoneNumber, HireDate, JobId, Salary, ManagerId, Department));
    }
}