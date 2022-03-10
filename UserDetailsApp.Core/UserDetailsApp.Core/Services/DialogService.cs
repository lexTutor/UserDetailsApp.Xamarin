using System.Threading.Tasks;
using Xamarin.Forms;

namespace UserDetailsApp.Core.Services
{
   public class DialogService : IDialogService
   {
      public async Task<bool> ShowDialogAsync(string message, string title, string okText, string cancelText)
      {
         return await Application.Current.MainPage.DisplayAlert(message, title, okText, cancelText);
      }
   }
}
