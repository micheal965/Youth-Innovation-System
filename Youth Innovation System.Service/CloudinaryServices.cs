using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youth_Innovation_System.Core.IServices;

namespace Youth_Innovation_System.Service
{
	public class CloudinaryServices :ICloudinaryServices
	{
		private readonly Cloudinary _cloudinary;

		public CloudinaryServices(IConfiguration configuration)
		{
			var account = new Account(
				configuration["Cloudinary:CloudName"],
				configuration["Cloudinary:ApiKey"],
				configuration["Cloudinary:ApiSecret"]
			);
			_cloudinary = new Cloudinary(account);
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