using AdminService.Application.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace AdminService.Api.Shared.Extensions;

public static class ResultMapper
{
    // ta ett Result och gör om det till ett http-svar
    public static IActionResult MapToActionResult(Result result)
        => MapError(result.Error!);

    public static IActionResult MapToActionResult<T>(Result<T> result)
        => MapError(result.Error!);

    public static IActionResult MapError(ResultError error)
    {
        return error.Type switch
        {
            ErrorTypes.NotFound => new NotFoundObjectResult(error.Message),
            ErrorTypes.BadRequest => new BadRequestObjectResult(error.Message),
            ErrorTypes.Conflict => new ConflictObjectResult(error.Message),
            _ => new ObjectResult(error.Message) { StatusCode = 500 }
        };
    }

}
