using Ardalis.Result;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Poc.Contract.Command.Region.Request;

public class UpdateRegionCommand : IRequest<Result>
{
    [Required]
    public decimal RegionId { get; set; }

    [Required]
    [MaxLength(50)]
    [DataType(DataType.Text)]
    public string RegionName { get; set; }
}
