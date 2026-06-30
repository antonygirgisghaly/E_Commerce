namespace E_Commerce.Application.Comman
{
    public sealed record Error(string code, string description, ErrorType errorType = ErrorType.Failure)
    {
        public static Error Failure(string code = "General.Failure",string description = "General Failure Error Has Occured")
            => new(code, description);
        public static Error Validation(string code = "General.Validation", string description = "General Validation Error Has Occured")
            => new(code, description,ErrorType.Validation);
        public static Error NotFound(string code = "General.NotFound", string description = "Resorce Has Occured")
          => new(code, description, ErrorType.NotFound);
        public static Error Conflict(string code = "General.Conflict", string description = "General Conflict Error Has Occured")
          => new(code, description, ErrorType.Conflict);
        public static Error Unothorized(string code = "General.Unothorized", string description = "Access Is Denied")
          => new(code, description, ErrorType.Unauthorized);
        public static Error Forbidden(string code = "General.Forbidden", string description = "This O")
          => new(code, description, ErrorType.Forbidden);
        public static Error InvalidCredentials(string code = "General.InvalidCredentials", string description = "Provided Credentials Invalid")
          => new(code, description, ErrorType.InvalidCredentials);
    }

    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Conflict = 3,
        Unauthorized = 4,
        Forbidden = 5,
        InvalidCredentials = 6
    }
}