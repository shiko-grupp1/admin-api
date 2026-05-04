namespace AdminService.Domain.Entities.Students;

public sealed record StudentId(Guid Value)
{
    public static StudentId Create()
    {
        return new StudentId(Guid.NewGuid());
    }
}
