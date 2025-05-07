using System;

namespace CityWebApi.Core.Dtos.City;

public class AddCity
{
    public string Name { get; set; } = "İstanbul";
    public int Population { get; set; } = 20000000;
    public RgbCity rgbCity {get; set;} = RgbCity.Marmara;

}
