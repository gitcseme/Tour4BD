namespace SharedKarnel.Contracts;

public class Result<T>
{
    public string Message { get; set; }
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
    public dynamic Errors { get; set; }

    public static Result<T> Success(T data, string message = default) => new()
    {
        Data = data,
        Message = message,
        IsSuccess = true
    };

    public static Result<T> Failure(dynamic errors = default, string message = default) => new()
    {
        Errors = errors,
        Message = message,
        IsSuccess = false
    };
}
