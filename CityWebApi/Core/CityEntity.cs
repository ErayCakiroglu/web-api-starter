using System;

namespace CityWebApi.Core;

public class CityEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "İstanbul";
    public int Population { get; set; } = 20000000;
    public RgbCity rgbCity {get; set;} = RgbCity.Marmara;
}
