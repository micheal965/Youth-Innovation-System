using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
namespace Youth_Innovation_System.Core.IServices
{
    public interface ICloudinaryServices
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
        Task<bool> DeleteFileAsync(string imageUrl);
    }
}
