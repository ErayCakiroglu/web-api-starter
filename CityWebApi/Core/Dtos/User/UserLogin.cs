using System;

namespace CityWebApi.Core.Dtos.User;

public class UserLogin
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
