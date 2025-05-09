using System;

namespace CityWebApi.Core.Dtos.User;

public class UserRegister
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
