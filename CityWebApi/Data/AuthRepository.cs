using System;
using CityWebApi.Core;
using Microsoft.EntityFrameworkCore;

namespace CityWebApi.Data;

public class AuthRepository : IAuthRepository
{
    private readonly DataContext _dataContext;

    public AuthRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<ServiceResponse<string>> Login(string UserName, string password)
    {
        var serviceResponse = new ServiceResponse<string>();
        var user = await _dataContext.UserEntities.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(UserName.ToLower()));

        if(user == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User is not available.";
        }
        else if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Wrong Password";
        }
        else
        {
            serviceResponse.Data = user.Id.ToString();
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<int>> Register(UserEntity user, string password)
    {
        ServiceResponse<int> serviceResponse = new ServiceResponse<int>();
        if(await UserExist(user.UserName))
        {
            serviceResponse.Success = true;
            serviceResponse.Message = "User is available.";
            return serviceResponse;
        }
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _dataContext.UserEntities.Add(user);
        await _dataContext.SaveChangesAsync();
        serviceResponse.Data = user.Id;
        return serviceResponse;
    }

    public async Task<bool> UserExist(string UserName)
    {
        if(await _dataContext.UserEntities.AnyAsync(x => x.UserName.ToLower() == UserName.ToLower()))
        {
            return true;
        }
        return false;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using(var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        {
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }
    }
}
