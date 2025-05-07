using System;
using System.Text.Json.Serialization;

namespace CityWebApi.Core;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RgbCity
{
    Marmara = 1,
    Anadolu = 2,
    Ege = 3,
    Karadeniz = 4
}
