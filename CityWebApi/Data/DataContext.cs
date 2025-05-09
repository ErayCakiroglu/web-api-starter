using System;
using CityWebApi.Core;
using Microsoft.EntityFrameworkCore;

namespace CityWebApi.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<CityEntity> CityEntities { get; set; }
    public DbSet<UserEntity> UserEntities { get; set; }
}
