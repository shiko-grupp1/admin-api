namespace AdminService.Application.Shared.Results;

public sealed record Result
{
    // eftersom bara get -> defaultvärden sätts från .NET om inget annat anges
    public bool IsSuccess { get; }
    // lagrar inget värde utan räknas ut varje gång den används (beräknad property). Finns inte i objektets state, bara en vy ovanpå IsSuccess.
    public bool IsFailure => !IsSuccess;
    public ResultError? Error { get; }

    // tvingar skapande objekt via create-metoderna
    private Result(bool isSuccess, ResultError? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true);
    public static Result Failure(ResultError error) => new(false, error);
}

public sealed record Result<T> 
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public ResultError? Error { get; }
    public T? Value { get; }

    // om jag inte skickar in något värde så kommer defaultvärdet för T att sättas
    private Result(bool isSuccess, ResultError? error = null, T? value = default) 
    {
        IsSuccess = isSuccess;
        Error = error; 
        Value = value;
    }

    public static Result<T> Success(T value) => new(true, null, value);
    public static Result<T> Failure(ResultError error) => new(false, error, default);
}