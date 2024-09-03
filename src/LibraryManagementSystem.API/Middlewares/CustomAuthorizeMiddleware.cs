using LibraryManagementSystem.Business.DTOs;
using System.Text.Json;

namespace LibraryManagementSystem.API.Middlewares
{
    public class CustomAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.Response.ContentType = "application/json";
                var response = new GenericResponseDto(401, "Unauthorized access. Please log in.");
                
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            else if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                context.Response.ContentType = "application/json";
                var response = new GenericResponseDto(403, "Forbidden. You do not have permission to access this resource.");
                
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
