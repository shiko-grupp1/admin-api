using AdminService.Application.Shared;
using Microsoft.EntityFrameworkCore;

namespace AdminService.Infrastructure.Persistence.Contexts;

public class PersistenceContext(DbContextOptions<PersistenceContext> options) : DbContext(options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceContext).Assembly);
    }

 
}
