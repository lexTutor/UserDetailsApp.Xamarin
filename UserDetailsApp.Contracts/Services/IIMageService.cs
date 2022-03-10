using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;

namespace UserDetailsApp.Contracts.Services
{
   public interface IIMageService
   {
      Task<ImageUploadResult> UploadImage(Stream image, string name, string extension);
   }
}
