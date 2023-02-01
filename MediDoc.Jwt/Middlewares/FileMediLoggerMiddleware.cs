namespace MediDoc.Jwt.Middlewares;

public class FileMediLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _path;

    public FileMediLoggerMiddleware(RequestDelegate next, string path)
    {
        _next = next;
        _path = path;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        var response = context.Response;

        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            var log = $"[{DateTime.Now}] {request.Method} {request.Path} {response.StatusCode} {e.Message}";
            await File.AppendAllTextAsync(_path, log + Environment.NewLine);
            throw;
        }
    }
}