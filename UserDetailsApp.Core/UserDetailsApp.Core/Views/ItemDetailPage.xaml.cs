using UserDetailsApp.Core.DIContainer;
using UserDetailsApp.Core.ViewModels;
using Xamarin.Forms;

namespace UserDetailsApp.Core.Views
{
   public partial class ItemDetailPage : ContentPage
   {
      public ItemDetailPage()
      {
         InitializeComponent();
         BindingContext = AppContainer.Resolve<ItemDetailViewModel>();
      }
   }
}