using poc.core.api.net8;
using poc.core.api.net8.api.net8.api.net8.Abstractions;
using Poc.Domain.Entities.Department.Events;

public class DepartmentEntity : BaseEOraclentity, IAggregateRoot
{
    public decimal Id { get; set; }
    public string Name { get; set; }
    public decimal ManagerId { get; set; }
    public decimal Location { get; set; }
    public DepartmentEntity() { }

    public DepartmentEntity(string name, decimal managerId, decimal location)
    {
        Name = name;
        ManagerId = managerId;
        Location = location;

        // Adicionando um evento de criação de departamento ao criar uma instância.
        AddDomainEvent(new DepartmentCreatedEvent(Id, Name, ManagerId, Location));
    }

    public DepartmentEntity(decimal id, string name, decimal managerId, decimal location)
    {
        Id = id;
        Name = name;
        ManagerId = managerId;
        Location = location;

        // Adicionando um evento de atualização de departamento ao atualizar os detalhes.
        AddDomainEvent(new DepartmentUpdatedEvent(Id, Name, ManagerId, Location));
    }

    public DepartmentEntity(decimal id)
    {
        // Adicionando um evento de exclusão de departamento ao excluir o departamento.
        Id = id;
        AddDomainEvent(new DepartmentDeletedEvent(Id, Name, ManagerId, Location));
    }

    public void SetDepartamentId(decimal departamentId)
    {
        if (Id == default)
        {
            Id = departamentId;
        }
        else
        {
            throw new InvalidOperationException("O ID só pode ser definido uma vez.");
        }
    }
}