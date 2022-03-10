using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using UserDetailsApp.Contracts.Services;
using UserDetailsApp.Core.Views;
using UserDetailsApp.Models.Models;
using Xamarin.Forms;

namespace UserDetailsApp.Core.ViewModels
{
   public class ItemsViewModel : BaseViewModel
   {
      #region Fields & Properties
      private UserModel _selectedItem;
      private readonly IUserService _userService;
      public ItemsViewModel(IUserService userService)
      {
         Title = "Users";
         _userService = userService;
         Users = new ObservableCollection<UserModel>();
         LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
         ItemTapped = new Command<UserModel>(OnItemSelected);
         AddItemCommand = new Command(OnAddItem);
      }

      public ObservableCollection<UserModel> Users { get; }
      public Command LoadItemsCommand { get; }
      public Command AddItemCommand { get; }
      public Command<UserModel> ItemTapped { get; }
      #endregion

      #region Methods
      async Task ExecuteLoadItemsCommand()
      {
         IsBusy = true;

         try
         {
            Users.Clear();
            IEnumerable<UserModel> items = await _userService.GetAllAsync();
            foreach(UserModel item in items)
            {
               if(string.IsNullOrWhiteSpace(item.PicturePath))
               {
                  item.PicturePath = "icons8user64.png";
               }
               Users.Add(item);
            }
         }
         catch(Exception ex)
         {
            Debug.WriteLine(ex);
         }
         finally
         {
            IsBusy = false;
         }
      }

      public void OnAppearing()
      {
         IsBusy = true;
         SelectedItem = null;
      }

      public UserModel SelectedItem
      {
         get => _selectedItem;
         set
         {
            SetProperty(ref _selectedItem, value);
            OnItemSelected(value);
         }
      }

      private async void OnAddItem(object obj)
      {
         await Shell.Current.GoToAsync(nameof(NewItemPage));
      }

      async void OnItemSelected(UserModel item)
      {
         if(item == null)
            return;

         // This will push the ItemDetailPage onto the navigation stack
         await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.UserId)}={item.Id}");
      }
      #endregion
   }
}