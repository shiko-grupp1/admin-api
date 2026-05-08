namespace AdminService.Infrastructure.Auth;

public static class AuthApiClientErrors
{
    public static string UserCreationFailed = "Failed to create user.";

    public static string InvalidUserId = "Auth service returned an invalid or missing user ID.";

    public static string UserDeletionFailed = "Failed to delete user.";

    public static string EmailIsRequired = "Email must be provided.";

    public static string RoleIsRequired = "Role must be provided.";
}
