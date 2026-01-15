
using Microsoft.AspNetCore.Http;

public class UploadHandler
{
    public string Upload(IFormFile file)
    {
        // 1. ตรวจสอบ Extension (นามสกุลไฟล์)
        List<string> validExtensions = new List<string>() { ".jpg", ".png", ".gif" };
        string extension = Path.GetExtension(file.FileName).ToLower();

        if (!validExtensions.Contains(extension))
        {
            return $"Extension is not valid ({string.Join(',', validExtensions)})";
        }

        // 2. ตรวจสอบขนาดไฟล์ (จำกัดที่ 5MB)
        long size = file.Length;
        if (size > (5 * 1024 * 1024)) // 5 * 1024 (KB) * 1024 (Bytes)
        {
            return "Maximum size can be 5MB";
        }

        // 3. เปลี่ยนชื่อไฟล์เป็น GUID เพื่อไม่ให้ชื่อซ้ำกัน
        string fileName = Guid.NewGuid().ToString() + extension;

        // 4. กำหนด Path ที่จะเก็บไฟล์ (โฟลเดอร์ Uploads ใน Project)
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        // 5. บันทึกไฟล์ลงในโฟลเดอร์
        string fileNameWithPath = Path.Combine(path, fileName);

        using var stream = new FileStream(fileNameWithPath, FileMode.Create);
        file.CopyTo(stream);

        return fileName; // ส่งชื่อไฟล์ที่ถูกสร้างใหม่กลับไป
    }
}
