using System.Net;

namespace Infrastructure.Response;

public class ApiResponse<T>
{
    public int StatucCode { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; }

    public ApiResponse(T data)
    {
        Data = data;
        StatucCode = 200;
        Message = "Success";
    }

    public ApiResponse(HttpStatusCode statusCode, string message)
    {
        Data = default;
        StatucCode = (int)statusCode;
        Message = message;
    }
}