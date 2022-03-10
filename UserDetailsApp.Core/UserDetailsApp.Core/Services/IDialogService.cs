using System.Threading.Tasks;

namespace UserDetailsApp.Core.Services
{
   public interface IDialogService
   {
      Task<bool> ShowDialogAsync(string message, string title, string okText, string cancelText);
   }
}