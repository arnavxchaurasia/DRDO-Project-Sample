using Microsoft.AspNetCore.Http;

namespace MyBackendAPI.Services
{
    public static class FileUploadHelper
    {
        public static async Task<string?> SaveFileAsync(IFormFile? file, string folderName = "uploads")
        {
            if (file == null)
                return null;

            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadPath, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/{folderName}/{uniqueFileName}";
        }
    }
}
