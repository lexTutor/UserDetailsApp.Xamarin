using UserDetailsApp.Core.DIContainer;
using UserDetailsApp.Core.ViewModels;
using Xamarin.Forms;

namespace UserDetailsApp.Core.Views
{
   public partial class ItemsPage : ContentPage
    {
       readonly ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = AppContainer.Resolve<ItemsViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}