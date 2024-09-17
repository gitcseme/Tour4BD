namespace Application.DTOs;

public class ApiResponse<T>
{
    public string Message { get; set; }
    public T Data { get; set; }
    public bool IsSuccess { get; set; }

    public static ApiResponse<T> Success(T data, string message = default) => new() { Data = data, Message = message, IsSuccess = true };
    
    public static ApiResponse<T> Failure(string errorMessage, T data = default) => new() { Data = data, IsSuccess = false, Message = errorMessage };
}
