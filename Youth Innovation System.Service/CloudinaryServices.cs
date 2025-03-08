using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Youth_Innovation_System.Core.IServices;

namespace Youth_Innovation_System.Service
{
    public class CloudinaryServices : ICloudinaryServices
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryServices(IConfiguration configuration)
        {
            var account = new Account(
                configuration["CloudinarySettings:CloudName"],
                configuration["CloudinarySettings:ApiKey"],
                configuration["CloudinarySettings:ApiSecret"]
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<bool> DeleteFileAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return false;

            var publicId = GetPublicIdfromUrl(imageUrl);
            if (string.IsNullOrEmpty(publicId)) return false;

            var deleteParams = new DeletionParams(publicId);

            var imageDestroyResult = await _cloudinary.DestroyAsync(deleteParams);
            return imageDestroyResult.Result == "ok";

        }
        private string GetPublicIdfromUrl(string imageUrl)
        {
            Uri uri = new Uri(imageUrl);
            string filename = Path.GetFileNameWithoutExtension(uri.AbsolutePath);
            return filename;
        }
        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var uploadResult = new ImageUploadResult();


            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult;

        }
    }
}