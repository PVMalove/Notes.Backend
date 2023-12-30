namespace Notes.WebApi.Middleware;

public static class CustomExceptionHandlerMiddlewareExtensioms
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}