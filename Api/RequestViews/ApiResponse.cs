namespace Api.RequestViews;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }

    private ApiResponse(int statusCode, string? message = null, object? data = null)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }
    public static ApiResponse Success(object? data, string? message = "Ok")
    {
        return new ApiResponse(200, message, data);
    }
    public static ApiResponse Ok(string? message = "Ok")
    {
        return new ApiResponse(200, message);
    }
    public static ApiResponse NotFound(string message = "Not Found")
    {
        return new ApiResponse(404, null, message);
    }
    public static ApiResponse BadRequest(string message = "Bad Request")
    {
        return new ApiResponse(400, null, message);
    }
    public static ApiResponse ServerError(string message = "Server Error")
    {
        return new ApiResponse(500, null, message);
    }
    public static ApiResponse Unauthorized(string message = "Unauthorized")
    {
        return new ApiResponse(401, message, null);
    }
}