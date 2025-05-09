using System;
using CityWebApi.Core;

namespace CityWebApi.Data;

public interface IAuthRepository
{
    Task<ServiceResponse<int>> Register(UserEntity user, string password);
    Task<ServiceResponse<string>> Login(string UserName, string password);
    Task<bool> UserExist(string UserName);
}
