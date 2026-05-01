namespace AdminService.Application.Shared.Results;

public sealed record ResultError(ErrorTypes Type, string Message);
