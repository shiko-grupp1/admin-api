using Microsoft.EntityFrameworkCore;

namespace AdminService.Infrastructure.Persistence.Contexts;

public class PersistenceContext(DbContextOptions<PersistenceContext> options) : DbContext(options)
{
}
