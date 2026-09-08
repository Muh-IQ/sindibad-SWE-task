namespace Sindibad.Api.Contracts;

public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string>? Errors { get; set; }

    public static ApiResponse Fail(string message, List<string>? errors = null)
        => new ApiResponse
        {
            Success = false,
            Message = message,
            Errors = errors
        };

    public static ApiResponse Ok(string message = "Operation Successed")
        => new ApiResponse
        {
            Success = true,
            Message = message
        };
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }

    public static ApiResponse<T> Ok(T data, string message = "Operation Succeeded")
        => new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
}
