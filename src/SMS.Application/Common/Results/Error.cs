namespace SMS.Application.Common.Results;

public sealed record Error (string Code, string Message)
{
    public static Error Validation ( string message ) => new Error( "VALIDATION_ERROR", message );
    public static Error NotFound ( string message ) => new Error( "NOT_FOUND_ERROR", message );
    public static Error Internal ( string message ) => new Error( "INTERNAL_ERROR", message );
    public static Error Exception ( string message ) => new Error( "EXCEPTION_ERROR", message );
}