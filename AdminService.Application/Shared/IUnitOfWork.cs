namespace AdminService.Application.Shared;
// int = number of rows that where changed in the database
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
