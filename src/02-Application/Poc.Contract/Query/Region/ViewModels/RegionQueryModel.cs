namespace Poc.Contract.Query.Region.ViewModels;
public class RegionQueryModel
{
    public decimal RegionId { get; set; }
    public string RegionName { get; set; }
    public string CountryName { get; set; }

    // Construtor privado sem parâmetros para o Dapper
    public RegionQueryModel() { }

    // Este construtor pode ser usado para criar instâncias na aplicação
    public RegionQueryModel(decimal regionId, string regionName, string countryName)
    {
        RegionId = regionId;
        RegionName = regionName;
        CountryName = countryName;
    }
}

