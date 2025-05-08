using System.Text.Json;
using Shared.ErrorModels;
namespace E_Commerce.Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;
        public CustomExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            this._next = Next;
            this._logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message
                };

                await httpContext.Response.WriteAsJsonAsync(Response);

            }

        }

    }

}