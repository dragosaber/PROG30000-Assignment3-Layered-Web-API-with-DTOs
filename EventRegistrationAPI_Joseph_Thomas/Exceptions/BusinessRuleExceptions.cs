namespace EventRegistrationAPI_Joseph_Thomas.Exceptions
{
    // Custom exceptions for business rule violations.

    // Custom exception for not found resources, such as when an event or registration doesn't exist.
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    // Custom exception for bad requests, such as invalid input or business rule violations.
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }

    // Custom exception for conflicts, such as duplicate entries or resource state conflicts.
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }
}
