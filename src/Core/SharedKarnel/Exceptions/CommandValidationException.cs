using FluentValidation.Results;

namespace SharedKarnel.Exceptions;

public class CommandValidationException : ApplicationException
{
    public CommandValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public CommandValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(
                grp => char.ToLower(grp.Key[0]) + grp.Key.Substring(1),
                grp => grp.ToArray()
            );
    }

    public IDictionary<string, string[]> Errors { get; }
}
