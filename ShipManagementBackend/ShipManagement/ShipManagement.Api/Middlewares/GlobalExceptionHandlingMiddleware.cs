namespace ShipManagement.Infrastructure.Midllewares;

using System.Net;
using System.Text.Json;
using ShipManagement.Infrastructure.Exceptions;

public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message;

            switch (exception)
            {
                case BadRequestException:
                    message = exception.Message;
                    status = HttpStatusCode.BadRequest;
                    break;

                case NotFoundException:
                    message = exception.Message;
                    status = HttpStatusCode.NotFound;
                    break;

                case InvalidOperationException:
                    message = exception.Message;
                    status = HttpStatusCode.Conflict;
                    break;

                case NotImplementedException:
                    message = exception.Message;
                    status = HttpStatusCode.NotImplemented;
                    break;

                case Exceptions.KeyNotFoundException:
                    message = exception.Message;
                    status = HttpStatusCode.NotFound;
                    break;

                default:
                    // Provide a generic error message when other exceptions may happen
                    status = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred. Please try again later.";
                    break;
            }

            var exceptionResult = JsonSerializer.Serialize(new
            {
                error = message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(exceptionResult);
        }
    }
