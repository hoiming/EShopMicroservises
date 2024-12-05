namespace BuildingBlocks.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }

        public BadRequestException(string message, string details) : base(details)
        {
            Detail = details;
        }

        public string? Detail { get; }
    }
}
