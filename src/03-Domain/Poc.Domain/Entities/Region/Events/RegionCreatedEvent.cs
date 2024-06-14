namespace Poc.Domain.Entities.Region.Events;

public class RegionCreatedEvent : RegionBaseEvent
{
    public RegionCreatedEvent(decimal regionId, string regionName) : base(regionId, regionName)
    {
    }
}
