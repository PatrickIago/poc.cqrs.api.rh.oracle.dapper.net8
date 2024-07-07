using Ardalis.Result;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Poc.Contract.Command.Departament.Request;
public class UpdateDepartmentCommand : IRequest<Result>
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    [Required]
    public int ManagerId { get; set; }
    [Required]
    public int Location { get; set; }
}