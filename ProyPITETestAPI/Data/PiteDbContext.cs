using Microsoft.EntityFrameworkCore;
using Proyecto.PiteApi.Interfaces;
using Proyecto.PiteApi.Interfaces.Contracts;
using Proyecto.PiteApi.Models;
using System.Data;
using System.Reflection;

namespace Proyecto.PiteApi.Data;

public class PiteDbContext : DbContext, IPiteDbContext
{
    public DbSet<User> Users { get; set; }

    public IDbConnection Connection => Database.GetDbConnection();

    public PiteDbContext(DbContextOptions<PiteDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var now = DateTime.Now;
        foreach (var entry in this.ChangeTracker.Entries())
        {
            var entity = entry.Entity;
            if (entity is IDatedEntity trackable)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        trackable.CreatedDate = now;
                        break;
                    case EntityState.Modified:
                        trackable.UpdatedDate = now;
                        break;
                    case EntityState.Deleted:
                        if (this.ChangeTracker.Entries<IDeletableEntity>().Any(x => x.Entity == entity))
                        {
                            entry.State = EntityState.Deleted;
                        }
                        else
                        {
                            trackable.IsActive = false;
                            trackable.DeletedDate = now;
                            entry.State = EntityState.Modified;
                        }
                        break;
                }
            }
        }
        this.ChangeTracker.DetectChanges();
        return await base.SaveChangesAsync(cancellationToken);
    }
}
