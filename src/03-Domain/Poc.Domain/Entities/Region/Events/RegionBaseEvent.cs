using poc.core.api.net8.Events;

namespace Poc.Domain.Entities.Region.Events;

public abstract class RegionBaseEvent : Event
{
    protected RegionBaseEvent(decimal regionId, string regionName)
    {
        RegionId = regionId;
        RegionName = regionName;
    }

    public decimal RegionId { get; private set; }
    public string RegionName { get; private set; }
}
