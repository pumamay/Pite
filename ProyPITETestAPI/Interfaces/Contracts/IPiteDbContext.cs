using Microsoft.EntityFrameworkCore;
using Proyecto.PiteApi.Models;
using System.Data;

namespace Proyecto.PiteApi.Interfaces.Contracts;

public interface IPiteDbContext : IUnitOfWork
{
    public DbSet<User> Users { get; }
    public IDbConnection Connection { get; }
}
