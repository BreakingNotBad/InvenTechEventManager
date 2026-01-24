using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public sealed class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        // เช็คว่าใช่ Error จาก FluentValidation หรือไม่?
        if (exception is not ValidationException validationException)
        {
            return false; // ถ้าไม่ใช่ ให้ส่งไม้ต่อให้ GlobalExceptionHandler
        }

        // สร้าง ProblemDetails สำหรับ Error 400
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation Failed",
            Detail = "ข้อมูลนำเข้าไม่ถูกต้อง โปรดตรวจสอบความผิดพลาดด้านล่าง",
        };

        // ดึง Error รายฟิลด์ออกมา (เช่น "EventName": ["ห้ามว่าง"])
        var errors = validationException
            .Errors.GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

        // ใส่เข้าไปใน Property เสริมชื่อ "errors" (ตามมาตรฐาน RFC)
        problemDetails.Extensions["errors"] = errors;

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        // ส่ง JSON กลับไป
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
