using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace InventechEventManager.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            // Log Error ไว้ดูภายหลัง
            _logger.LogError(exception, "เกิดข้อผิดพลาดที่ไม่คาดคิด: {Message}", exception.Message);

            // สร้าง ProblemDetails สำหรับ Error 500
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error",
                Detail = "เกิดข้อผิดพลาดภายในระบบ โปรดติดต่อผู้ดูแลระบบ",
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            // ส่ง JSON กลับไป
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
