using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
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

        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return false;

            var publicId = GetPublicIdfromUrl(imageUrl);
            if (string.IsNullOrEmpty(publicId)) return false;

            var deleteParams = new DeletionParams(publicId);

            var imageDestroyResult = await _cloudinary.DestroyAsync(deleteParams);
            return imageDestroyResult.Result == "ok";

        }
        private string GetPublicIdfromUrl(string url)
        {
            Uri uri = new Uri(url);
            string publicId = Path.GetFileNameWithoutExtension(uri.AbsolutePath);
            return publicId;
        }
        public async Task<string> GetImageAsync(string ImageUrl)
        {
            try
            {
                var publicId = GetPublicIdfromUrl(ImageUrl);
                var uri = _cloudinary.Api.UrlImgUp.BuildUrl(publicId);
                return uri.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("There is no Image");
            }
        }
        //public async Task<string> GetFileAsync(string url)
        //{
        //    try
        //    {
        //        var publicId = getb(url);

        //        // ✅ Use `.Url` instead of `.UrlImgUp` for all files
        //        var uri = _cloudinary.Api.Url.BuildUrl(publicId);

        //        return uri.ToString();
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("File not found or invalid URL.");
        //    }
        //}
        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;
            //Restrict send only images
            if (!IsValidImageFile(file))
                throw new Exception($"Invalid file: {file.FileName} (Only images are allowed)");

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
        public async Task<List<RawUploadResult>> UploadFilesAsync(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                throw new Exception("No files provided");

            var uploadResults = new List<RawUploadResult>();

            foreach (var file in files)
            {
                if (file.Length == 0) continue; // Skip empty files

                await using var stream = file.OpenReadStream();
                var uploadParams = new RawUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = $"posts/{Guid.NewGuid()}"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                uploadResults.Add(uploadResult);
            }

            return uploadResults;
        }
        /// <summary>
        /// ✅ Validates whether the uploaded file is an image.
        /// </summary>
        private bool IsValidImageFile(IFormFile file)
        {
            var allowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
            var allowedMimeTypes = new HashSet<string> { "image/jpeg", "image/png", "image/gif", "image/bmp", "image/webp" };

            // Check MIME type
            if (!allowedMimeTypes.Contains(file.ContentType.ToLower()))
                return false;

            // Check file extension
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                return false;

            return true;
        }
        public async Task<List<(string imageUrl, string publicId)>> UploadImagesAsync(List<IFormFile> files)
        {
            var imageList = new List<(string imageUrl, string publicId)>();

            foreach (var file in files)
            {
                if (file == null || file.Length == 0) continue;

                //Restrict send only images
                if (!IsValidImageFile(file))
                    throw new Exception($"Invalid file: {file.FileName} (Only images are allowed)");
                await using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = $"posts/{Guid.NewGuid()}",
                    Transformation = new Transformation().Width(800).Height(500).Crop("fill")
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                imageList.Add((uploadResult.SecureUrl.ToString(), uploadResult.PublicId));
            }

            return imageList;
        }

    }
}