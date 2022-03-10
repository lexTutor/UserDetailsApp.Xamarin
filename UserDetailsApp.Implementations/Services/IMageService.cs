using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using UserDetailsApp.Contracts.Services;

namespace UserDetailsApp.Implementations.Services
{
   public class ImageService : IIMageService
   {
      private readonly Cloudinary cloudinary;

      public ImageService()
      {
         cloudinary = new Cloudinary(new Account("cloud9ne", "192122632777812", "dsdX67WClh58ql-tD1uO0LJaRuA"));
      }

      public async Task<ImageUploadResult> UploadImage(Stream image, string name, string extension)
      {
         //object to return
         var uploadResult = new ImageUploadResult();
         bool isFormatSupported = false;
         // validate the image size and extension type using settings from appsettings
         var listOfExtensions = new List<string> { "jpg", "jpeg", "png", "jfif" };
         for(int i = 0; i < listOfExtensions.Count; i++)
         {
            if(extension == listOfExtensions[i])
            {
               isFormatSupported = true;
               break;
            }
         }

         if(image == null || !isFormatSupported)
         {
            return null;
         }

         //fetch image as stream of data
         using(Stream imageStream = image)
         {
            string fileName = Guid.NewGuid().ToString() + "_" + name;
            //upload to cloudinary
            uploadResult = await cloudinary.UploadAsync(new ImageUploadParams()
            {
               File = new FileDescription(fileName, imageStream),
               Transformation = new Transformation().Crop("thumb").Gravity("face").Width(1000)
                                                    .Height(1000).Radius(40)
            });
         }
         return uploadResult;
      }
   }
}
