using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Contract;

namespace Service
{
    public class FileService : IFileService
    {
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

            return Path.Combine(subFolder, uniqueName).Replace("\\", "/");// คืนค่า path สำหรับเก็บใน DB
        }
    }
}
