namespace Poc.Contract.Command.Region.Response;

public class CreateRegionResponse
{
    public CreateRegionResponse(decimal id) => RegionId = id;

    public decimal RegionId { get; }
}
