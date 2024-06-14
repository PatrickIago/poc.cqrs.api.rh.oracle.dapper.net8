namespace Poc.Domain.Entities.Region.Events;

public class RegionDeletedEvent : RegionBaseEvent
{
    public RegionDeletedEvent(decimal regionId, string regionName) : base(regionId, regionName)
    {
    }
}
