using System;
using System.Collections.Generic;
using System.ComponentModel;
using UserDetailsApp.Core.DIContainer;
using UserDetailsApp.Core.Models;
using UserDetailsApp.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UserDetailsApp.Core.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = AppContainer.Resolve<NewItemViewModel>();
        }
    }
}