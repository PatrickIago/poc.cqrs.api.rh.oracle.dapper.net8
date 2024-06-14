using poc.core.api.net8; 
using poc.core.api.net8.api.net8.api.net8.Abstractions; 
using Poc.Domain.Entities.Department.Events; 

namespace Poc.Domain.Entities.Departament;

/// <summary>
/// Entidade que representa um departamento na aplicação.
/// </summary>
public class DepartamentEntity : BaseEOraclentity, IAggregateRoot
{
    /// <summary>
    /// ID do departamento.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Nome do departamento.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// ID do gerente do departamento (pode ser nulo).
    /// </summary>
    public int? ManagerId { get; private set; }

    /// <summary>
    /// Localização do departamento.
    /// </summary>
    public string Location { get; private set; }

    /// <summary>
    /// Construtor padrão da entidade DepartamentEntity.
    /// </summary>
    public DepartamentEntity() { }

    /// <summary>
    /// Construtor para criar uma instância de DepartamentEntity com os detalhes do departamento.
    /// </summary>
    /// <param name="name">Nome do departamento.</param>
    /// <param name="managerId">ID do gerente do departamento (opcional).</param>
    /// <param name="location">Localização do departamento.</param>
    public DepartamentEntity(string name, int? managerId, string location)
    {
        Name = name;
        ManagerId = managerId;
        Location = location;

        // Adicionando um evento de criação de departamento ao criar uma instância.
        AddDomainEvent(new DepartmentCreatedEvent(Id, Name, ManagerId, Location));
    }

    /// <summary>
    /// Atualiza os detalhes do departamento.
    /// </summary>
    /// <param name="name">Novo nome do departamento.</param>
    /// <param name="managerId">Novo ID do gerente do departamento (opcional).</param>
    /// <param name="location">Nova localização do departamento.</param>
    public DepartamentEntity(int id,string name, int? managerId, string location)
    {
        Id = id;
        Name = name;
        ManagerId = managerId;
        Location = location;

        // Adicionando um evento de atualização de departamento ao atualizar os detalhes.
        AddDomainEvent(new DepartmentUpdatedEvent(Id, Name, ManagerId, Location));
    }

    /// <summary>
    /// Exclui o departamento.
    /// </summary>
    public DepartamentEntity(int id)
    {
        // Adicionando um evento de exclusão de departamento ao excluir o departamento.
        AddDomainEvent(new DepartmentDeletedEvent(Id, Name, ManagerId, Location));
    }
}