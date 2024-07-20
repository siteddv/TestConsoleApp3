namespace SlaveryMarket.Web.Responses;

public class Response<T>
{
    public Response(T data, string message, bool success)
    {
        Data = data;
        Message = message;
        Success = success;
    }

    public T? Data { get; set; }
    public string? Message { get; set; }
    public bool Success { get; set; }
}