namespace SharedKarnel.Exceptions;

public class InvalidRequestException : ApplicationException
{
    public InvalidRequestException(string errorMessage)
        : base("Invalid request format.")
    {
        Error = errorMessage;
    }

    public string Error { get; set; }
}
