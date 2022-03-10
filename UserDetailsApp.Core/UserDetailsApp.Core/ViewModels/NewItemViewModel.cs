using System;
using System.Diagnostics;
using UserDetailsApp.Contracts.Services;
using UserDetailsApp.Models.Models;
using Xamarin.Forms;

namespace UserDetailsApp.Core.ViewModels
{
   [QueryProperty(nameof(UserId), nameof(UserId))]
   public class NewItemViewModel : BaseViewModel
   {
      #region Fields
      private string Id;
      private string sex;
      private string phoneNumber;
      private string email;
      private string address;
      private string firstName;
      private string lastName;

      private readonly IUserService _userService;
      #endregion

      #region Constructor
      public NewItemViewModel(IUserService userService)
      {
         SaveCommand = new Command(OnSave, ValidateSave);
         CancelCommand = new Command(OnCancel);
         this.PropertyChanged +=
             (_, __) => SaveCommand.ChangeCanExecute();
         _userService = userService;
      }
      #endregion

      #region Properties
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

      public string FirstName
      {
         get { return firstName; }
         set => SetProperty(ref firstName, value);
      }

      public string Address
      {
         get => address;
         set => SetProperty(ref address, value);
      }

      public string LastName
      {
         get => lastName;
         set => SetProperty(ref lastName, value);
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

      public Command SaveCommand { get; }
      public Command CancelCommand { get; }
      #endregion

      #region Methods
      private bool ValidateSave() => !string.IsNullOrWhiteSpace(firstName)
             && !string.IsNullOrWhiteSpace(lastName)
             && !string.IsNullOrWhiteSpace(email)
             && !string.IsNullOrWhiteSpace(sex)
             && !string.IsNullOrWhiteSpace(address)
             && !string.IsNullOrWhiteSpace(phoneNumber);

      private async void OnCancel()
      {
         // This will pop the current page off the navigation stack
         await Shell.Current.GoToAsync("..");
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
         FirstName = user.FirstName;
         PhoneNumber = user.PhoneNumber;
         Address = user.Address;
         Sex = user.Sex;
         Email = user.Email;
         LastName = user.LastName;
      }

      private async void OnSave()
      {
         UserModel user = new UserModel()
         {
            Address = Address,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Sex = Sex,
            PhoneNumber = PhoneNumber
         };

         if(Convert.ToInt32(Id) > 0)
         {
            user.Id = Convert.ToInt32(Id);
            await _userService.UpdateAsync(user);
         }
         else
         {
            await _userService.CreateAsync(user);
         }
         // This will pop the current page off the navigation stack
         await Shell.Current.GoToAsync("..");
      }
      #endregion
   }
}
