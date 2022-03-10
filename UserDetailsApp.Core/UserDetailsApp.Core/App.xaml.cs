using UserDetailsApp.Core.DIContainer;
using Xamarin.Forms;

namespace UserDetailsApp.Core
{
   public partial class App : Application
   {

      public App()
      {
         InitializeComponent();

         AppContainer.ConfigureServices();
         MainPage = new AppShell();
      }

      protected override void OnStart()
      {
      }

      protected override void OnSleep()
      {
      }

      protected override void OnResume()
      {
      }
   }
}
