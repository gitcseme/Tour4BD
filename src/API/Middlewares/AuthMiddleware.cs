using Application;

namespace API.Middlewires;

public class AuthMiddleware : IMiddleware
{
    private const string AuthHeaderName = "Authorization";

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Headers.TryGetValue(AuthHeaderName, out var token))
        {
            string tokenString = ((string)token!).Substring("Bearer ".Length);

            context.Items[AppConstants.TokenItem] = tokenString;
        }

        await next(context);
    }
}
