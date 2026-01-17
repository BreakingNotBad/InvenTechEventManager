namespace Service.Contracts
{
    public interface IFileService
    {
        // รับ Stream และชื่อไฟล์ดั้งเดิม (เพื่อเอานามสกุล)
        Task<string> SaveFileAsync(Stream fileStream, string fileName, string subFolder);
        Task DeleteFileAsync(string filePath);
    }
}
