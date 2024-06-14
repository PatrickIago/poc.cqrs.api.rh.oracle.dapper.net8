namespace Poc.Domain.Entities.Region.Events;
public class RegionUpdateEvent : RegionBaseEvent
{
    public RegionUpdateEvent(decimal regionId, string regionName) : base(regionId, regionName)
    {
    }
}
