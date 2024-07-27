using Ardalis.Result;
using MediatR;

namespace Poc.Contract.Command.Region.Request;
public class DeleteRegionCommand : IRequest<Result>
{
    public DeleteRegionCommand(decimal regionId, string userIdDelete )
    {
        RegionId = regionId;
        UserIdDelete = userIdDelete;
    }

    public decimal RegionId { get; private set; }
    public string UserIdDelete { get; private set; }
}