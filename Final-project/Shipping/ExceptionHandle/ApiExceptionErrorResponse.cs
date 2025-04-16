namespace Shipping.ExceptionHandle
{
    public class ApiExceptionErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

        public ApiExceptionErrorResponse(int statusCode, string message = null, string details = null)
        {
            StatusCode = statusCode;
            Message = message ?? "An internal server error occurred.";
            Details = details;
        }
    }
}
