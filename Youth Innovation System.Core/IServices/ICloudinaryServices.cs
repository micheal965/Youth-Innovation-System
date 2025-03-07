using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth_Innovation_System.Core.IServices
{
	public interface ICloudinaryServices
	{
		Task<ImageUploadResult> UploadImageAsync(IFormFile file);
	}
}
