using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Linq;
using static Constants;

namespace EmployeeManagement.Extensions
{
    public static class IFormFileExtensions
    {
        public static bool IsSupported(this IFormFile formFile)
        {
            var fileExtension = Path.GetExtension(formFile.FileName).ToLower();

            return UploadsSettings.AcceptedFileTypes.Contains(fileExtension);
        }

        public static byte[] GetImageData(this IFormFile formFile)
        {
            const int targetWidth = 400;
            const int targetHeight = 0;
            
            using (var inputStream = formFile.OpenReadStream())
            using (var outputStream = new MemoryStream())
            {
                using var image = Image.Load(inputStream, out IImageFormat format);
                image.Mutate(i => i.Resize(targetWidth, targetHeight));
                image.Save(outputStream, format);

                return outputStream.ToArray();
            }            
        }              
    }
}
