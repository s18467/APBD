using MediDoc.Jwt.Middlewares;

namespace MediDoc.Jwt.Extensions
{
    public static class FileMediLoggerExtensions
    {
        public static IApplicationBuilder UseFileMediLogger(this IApplicationBuilder builder, string path)
        {
            return builder.UseMiddleware<FileMediLoggerMiddleware>(path);
        }
    }
}
