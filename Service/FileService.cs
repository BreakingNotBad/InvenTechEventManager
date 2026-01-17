using Service.Contracts;

namespace Service
{
    public class FileService : IFileService
    {
        public Task DeleteFileAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return Task.CompletedTask;

            // filePath ที่มาจาก DB เช่น: Staff/abc123.png
            var fullPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                filePath.Replace("/", Path.DirectorySeparatorChar.ToString())
            );

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            return Task.CompletedTask;
        }

        public async Task<string> SaveFileAsync(
            Stream fileStream,
            string fileName,
            string subFolder
        )
        {
            var extension = Path.GetExtension(fileName);
            var uniqueName = $"{Guid.NewGuid()}{extension}";

            // กำหนดที่อยู่ไฟล์ (ไปที่ Presentation/wwwroot/uploads/...)
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                subFolder
            );

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var filePath = Path.Combine(path, uniqueName);

            // บันทึกไฟล์จาก Stream
            using (var targetStream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.CopyToAsync(targetStream);
            }

            return Path.Combine(subFolder, uniqueName).Replace("\\", "/"); // คืนค่า path สำหรับเก็บใน DB
        }
    }
}
