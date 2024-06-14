using Ardalis.Result;
using MediatR;
using Poc.Contract.Command.Region.Response;
using System.ComponentModel.DataAnnotations;

namespace Poc.Contract.Command.Region.Request;

public class CreateRegionCommand : IRequest<Result<CreateRegionResponse>>
{
    [Required]
    [MaxLength(50)]
    [DataType(DataType.Text)]
    public string RegionName { get; set; }
}
