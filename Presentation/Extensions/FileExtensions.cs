using Microsoft.AspNetCore.Http;

namespace Presentation.Extensions
{
    public static class FileExtensions
    {
        private static readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        public static bool IsValidImageExtension(this IFormFile? file)
        {
            // ถ้าเป็น null ให้ผ่าน (ถือว่าไม่ได้แนบไฟล์มา ก็ไม่ผิดกฎนามสกุล)
            if (file == null)
                return true;

            var ext = Path.GetExtension(file.FileName).ToLower();
            return _allowedExtensions.Contains(ext);
        }
    }
}
