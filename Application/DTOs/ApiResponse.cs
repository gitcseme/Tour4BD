namespace Application.DTOs;

public class ApiResponse<T>
{
    public string Message { get; set; }
    public T Data { get; set; }
    public bool IsSuccess { get; set; }

    public static ApiResponse<T> Success(T Data) => new() { Data = Data, IsSuccess = true };
    public static ApiResponse<T> Failure(string errorMessage, T Data = default) => new() { Data = Data, IsSuccess = false, Message = errorMessage };
}
