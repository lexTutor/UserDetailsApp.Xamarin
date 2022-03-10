using System;
using System.Diagnostics;
using System.Windows.Input;
using CloudinaryDotNet.Actions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using UserDetailsApp.Contracts.Services;
using UserDetailsApp.Core.Services;
using UserDetailsApp.Core.Views;
using UserDetailsApp.Models.Models;
using Xamarin.Forms;
using static UserDetailsApp.Implementations.Utilities.Enums;

namespace UserDetailsApp.Core.ViewModels
{
   [QueryProperty(nameof(UserId), nameof(UserId))]
   public class ItemDetailViewModel : BaseViewModel
   {
      #region Fields & Constructor
      private string Id;
      private string sex;
      private string phoneNumber;
      private string email;
      private string picturePath;
      private string address;

      private readonly IUserService _userService;
      private readonly IIMageService _imageService;
      private readonly IDialogService _dialogService;

      public ItemDetailViewModel(IUserService userService, IIMageService iMageService, IDialogService dialogService)
      {
         _userService = userService;
         _imageService = iMageService;
         _dialogService = dialogService;
      }
#endregion

      #region Properties
      public ICommand TakePicture => new Command(OnCameraClicked);
      public ICommand EditProfile => new Command(OnEditButtonClicked);


      private string fullName;

      public string FullName
      {
         get { return fullName; }
         set => SetProperty(ref fullName, value);
      }

      public string Address
      {
         get => address;
         set => SetProperty(ref address, value);
      }

      public string PicturePath
      {
         get => string.IsNullOrWhiteSpace(picturePath) ? "biguser.png" : picturePath;
         set => SetProperty(ref picturePath, value);
      }

      public string Email
      {
         get => email;
         set => SetProperty(ref email, value);
      }

      public string Sex
      {
         get => sex;
         set => SetProperty(ref sex, value);
      }

      public string PhoneNumber
      {
         get => phoneNumber;
         set => SetProperty(ref phoneNumber, value);
      }

      public string UserId
      {
         get
         {
            return Id;
         }
         set
         {
            Id = value;
            LoadItemId(value);
         }
      }
#endregion

      #region Methods
      private async void OnEditButtonClicked(object obj)
      {
         await Shell.Current.GoToAsync($"{nameof(NewItemPage)}?{nameof(UserId)}={UserId}");
      }

      private async void OnCameraClicked(object obj)
      {
         await CrossMedia.Current.Initialize();
         if(!CrossMedia.Current.IsCameraAvailable && !CrossMedia.Current.IsCameraAvailable && !CrossMedia.Current.IsTakePhotoSupported)
         {
            return;
         }

         bool isGallery = await _dialogService.ShowDialogAsync("Upload", "Upload your picture", "Gallery", "Camera");
         MediaFile file = isGallery
            ? await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions())
            : await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
               SaveToAlbum = true,
               Name = FullName + "jpg"
            });

         if(file == null)
            return;
         string[] extension = file.Path.Split('.');
         ImageUploadResult result = await _imageService.UploadImage(file.GetStream(), FullName, extension[1]);
         if(result == null)
         {
            //display message to user
         }
         UserModel user = await _userService.GetAsync(Convert.ToInt32(Id));
         user.PicturePath = result.Url.ToString();
         user = await _userService.UpdateAsync(user);
         AssignProperties(user);
      }

      public async void LoadItemId(string itemId)
      {
         try
         {
            UserModel user = await _userService.GetAsync(Convert.ToInt32(itemId));
            AssignProperties(user);
         }
         catch(Exception)
         {
            Debug.WriteLine("Failed to Load Item");
         }
      }

      private void AssignProperties(UserModel user)
      {
         Id = user.Id.ToString();
         FullName = user.FullName;
         PhoneNumber = user.PhoneNumber;
         Address = user.Address;
         Sex = Enum.TryParse(user.Sex, out Gender gender) ? gender.ToString() : Gender.Invalid.ToString();
         Email = user.Email;
         PicturePath = user.PicturePath;
      }

      #endregion
   }
}
